using UnityEngine;
using UnityEngine.Assertions;

public class Simulation : MonoBehaviour
{
    //
    // Settings
    // ---
    [Header("Sim Settings")]
    [SerializeField]
    int _gridPixelCount = 1024;
    public int GridPixelCount { get { return _gridPixelCount; } }

    [SerializeField]
    [Tooltip("20fps = 0.05, 30fps = 0.0333, 60fps = 0.0166")]
    float _updateInterval = 0.01f; // also called deltaTime in the paper, denoted "_DT" in shaders
    public float UpdateInterval { get { return _updateInterval; } }

    [SerializeField]
    float _gridPixelSize = 0.1f; // also called pipe-length (l) in the paper, denoted "_L" in shaders
    public float GridPixelSize { get { return _gridPixelSize; } }
    [SerializeField]
    float _pipeCrossSectionArea = 0.1f;
    [SerializeField]
    float _gravityConstant = 9.81f;

    [SerializeField]
    [Range(0.9f, 1f)]
    float _damping = 0.99f;

    [Header("Sand Settings")]
    [SerializeField]
    float _sandBlurPerSecond = 10.0f;
    [SerializeField]
    float _sandBlurAtStartSigma = 7.0f;

    [Header("Erosion Settings")]
    [SerializeField]
    float _sedimentCapacityConstant = 1;
    public float SedimentCapacityConstant { get { return _sedimentCapacityConstant; } }
    [SerializeField]
    float _dissolvingConstant = 0.01f;
    [SerializeField]
    float _depositionConstant = 0.01f;
    [SerializeField]
    float _erosionMinimumAngleThresshold = 0.001f;
    public float ErosionMinimumAngleThresshold { get { return _erosionMinimumAngleThresshold; } }

    [Header("Initializaton")]
    [SerializeField]
    Material _initialHeights;

    [Header("Performace")]
    [SerializeField]
    int _maxSimStepsPerFrame = 5;

    //
    // Schaders
    // ---
    [Header("Simulation Shaders")]
    [SerializeField]
    Shader _updateOutflowFluxShader;
    [SerializeField]
    Shader _updateHeightsShader;
    [SerializeField]
    Shader _updateVelocityFieldShader;
    [SerializeField]
    Shader _updateErosionDepositionShader;
    [SerializeField]
    Shader _updateSedimentTransportationShader;
    [SerializeField]
    Shader _blurSandShader;
    [SerializeField]
    Shader _addSourceShader;

    //
    // Materials
    // ---
    Material _updateOutflowFluxMaterial;
    Material _updateHeightsMaterial;
    Material _updateVelocityFieldMaterial;
    Material _updateErosionDepositionMaterial;
    Material _updateSedimentTransportationMaterial;
    Material _blurSandMaterial;
    Material _addSourceMaterial;

    //
    // Textures
    // ---
    BufferedRenderTexture _waterSandRockSediment; // R: water, G: sand, B: rock, A: sediment
    public RenderTexture CurrentWaterSandRockSediment { get { return _waterSandRockSediment.Texture; } }
    BufferedRenderTexture _outflowFluxRLBT; // outflowflux R: right, G: left, B: bottom, A: top
    public RenderTexture CurrentOutflowFluxRLBT { get { return _outflowFluxRLBT.Texture; } }
    BufferedRenderTexture _velocityXY; //velocity: R: x, G: y
    public RenderTexture CurrentVelocityXY { get { return _velocityXY.Texture; } }

    //
    // Other
    // ---
    float _nextUpdate;

    //
    // Events
    // ---
    public event System.Action OnBeforeSimFrame;
    public event System.Action OnSimStep;
    public event System.Action OnAfterSimFrame;

    //
    // Code
    // ---
    void Start()
    {
        // Some assurances
        Assert.IsFalse(_initialHeights == null, "Missing initial heights material."); //IsNotNull doesn't work for some reason

        // Create materials
        _updateOutflowFluxMaterial = new Material(_updateOutflowFluxShader);
        _updateHeightsMaterial = new Material(_updateHeightsShader);
        _updateVelocityFieldMaterial = new Material(_updateVelocityFieldShader);
        _updateErosionDepositionMaterial = new Material(_updateErosionDepositionShader);
        _updateSedimentTransportationMaterial = new Material(_updateSedimentTransportationShader);
        _blurSandMaterial = new Material(_blurSandShader);

        _addSourceMaterial = new Material(_addSourceShader);

        // Create textures
        _gridPixelCount = Mathf.ClosestPowerOfTwo(_gridPixelCount);
        var format = RenderTextureFormat.ARGBFloat;
        var readWrite = RenderTextureReadWrite.Linear;
        Assert.IsTrue(SystemInfo.SupportsRenderTextureFormat(format), "Rendertexture format not supported: " + format);
        _waterSandRockSediment = new BufferedRenderTexture(_gridPixelCount, _gridPixelCount, 0, format, readWrite, _initialHeights);
        _outflowFluxRLBT = new BufferedRenderTexture(_gridPixelCount, _gridPixelCount, 0, format, readWrite, Texture2D.blackTexture);
        _velocityXY = new BufferedRenderTexture(_gridPixelCount, _gridPixelCount, 0, format, readWrite, Texture2D.blackTexture);

        // Blur sand a bit
        BlurSandOnce();

        // Start first simulation step
        _nextUpdate = Time.time;
    }

    void Update()
    {
        int iter = 0;
        if (OnBeforeSimFrame != null) OnBeforeSimFrame();
        while (Time.time >= _nextUpdate && iter < _maxSimStepsPerFrame)
        {
            UpdateSimulation();
            _nextUpdate += _updateInterval;
            iter++;
            if (OnSimStep != null) OnSimStep();
        }
        if (OnAfterSimFrame != null) OnAfterSimFrame();

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 zeroPos = ray.origin + ray.direction * (ray.origin.y - 0.5f) / -ray.direction.y;
            Debug.Log(ray.origin + " -> " + ray.direction + " = " + zeroPos);
            AddWaterSandRockSediment(new Vector2(5f + zeroPos.x, 5f - zeroPos.z) / 10f, 2f, new Vector4(1f * Time.deltaTime, 0, 0, 0));
        }
    }

    void UpdateSimulation()
    {
        // Do all steps
        UpdateFluxStep();
        UpdateHeightsStep();
        UpdateVelocityXY();
        UpdateErosionDeposition();
        UpdateSedimentTransportation();
    }

    void UpdateFluxStep()
    {
        // Set values
        _updateOutflowFluxMaterial.SetTexture("_WaterSandRockSedimentTex", _waterSandRockSediment.Texture);
        _updateOutflowFluxMaterial.SetFloat("_DT", _updateInterval);
        _updateOutflowFluxMaterial.SetFloat("_L", _gridPixelSize);
        _updateOutflowFluxMaterial.SetFloat("_A", _pipeCrossSectionArea);
        _updateOutflowFluxMaterial.SetFloat("_G", _gravityConstant);

        // Do the step
        Graphics.Blit(_outflowFluxRLBT.Texture, _outflowFluxRLBT.Buffer, _updateOutflowFluxMaterial);

        // Finalize
        _outflowFluxRLBT.Swap();
    }

    void UpdateHeightsStep()
    {
        // Set values
        _updateHeightsMaterial.SetTexture("_OutflowFluxRLBT", _outflowFluxRLBT.Texture);
        _updateHeightsMaterial.SetFloat("_DT", _updateInterval);
        _updateHeightsMaterial.SetFloat("_L", _gridPixelSize);
        _updateHeightsMaterial.SetFloat("_SandBlurPerSecond", _sandBlurPerSecond);
        _updateHeightsMaterial.SetFloat("_Damping", _damping);

        // Do the step
        Graphics.Blit(_waterSandRockSediment.Texture, _waterSandRockSediment.Buffer, _updateHeightsMaterial);

        // Finalize
        _waterSandRockSediment.Swap();
    }

    void UpdateVelocityXY()
    {
        // Set values
        _updateVelocityFieldMaterial.SetTexture("_OutflowFluxRLBT", _outflowFluxRLBT.Texture);
        _updateVelocityFieldMaterial.SetTexture("_WaterSandRockSedimentTex", _waterSandRockSediment.Texture);
        _updateVelocityFieldMaterial.SetTexture("_PreviousWaterSandRockSedimentTex", _waterSandRockSediment.Buffer);
        _updateVelocityFieldMaterial.SetFloat("_L", _gridPixelSize);

        // Do the step
        Graphics.Blit(_velocityXY.Texture, _velocityXY.Buffer, _updateVelocityFieldMaterial);

        // Finalize
        _velocityXY.Swap();
    }

    void UpdateErosionDeposition()
    {
        // Set values
        _updateErosionDepositionMaterial.SetTexture("_VelocityXY", _velocityXY.Texture);

        _updateErosionDepositionMaterial.SetFloat("_DT", _updateInterval);
        _updateErosionDepositionMaterial.SetFloat("_L", _gridPixelSize);

        _updateErosionDepositionMaterial.SetFloat("_Kc", _sedimentCapacityConstant);
        _updateErosionDepositionMaterial.SetFloat("_Ks", _dissolvingConstant);
        _updateErosionDepositionMaterial.SetFloat("_Kd", _depositionConstant);
        _updateErosionDepositionMaterial.SetFloat("_ErosionMinimumAngleThresshold", _erosionMinimumAngleThresshold);

        // Do the step
        Graphics.Blit(_waterSandRockSediment.Texture, _waterSandRockSediment.Buffer, _updateErosionDepositionMaterial);

        // Finalize
        _waterSandRockSediment.Swap();
    }

    void UpdateSedimentTransportation()
    {
        _updateSedimentTransportationMaterial.SetTexture("_VelocityXY", _velocityXY.Texture);
        _updateSedimentTransportationMaterial.SetFloat("_DT", _updateInterval);
        _updateSedimentTransportationMaterial.SetFloat("_L", _gridPixelSize);

        // Do the step
        Graphics.Blit(_waterSandRockSediment.Texture, _waterSandRockSediment.Buffer, _updateSedimentTransportationMaterial);

        // Finalize
        _waterSandRockSediment.Swap();
    }

    void BlurSandOnce()
    {
        // Set values
        _blurSandMaterial.SetFloat("_Sigma", _sandBlurAtStartSigma);

        // Do the step
        Graphics.Blit(_waterSandRockSediment.Texture, _waterSandRockSediment.Buffer, _blurSandMaterial);

        // Finalize
        _waterSandRockSediment.Swap();
    }

    public void AddWaterSandRockSediment(Vector2 mid, float radius, Vector4 amount)
    {
        var tmpActiveRT = RenderTexture.active;
        RenderTexture.active = CurrentWaterSandRockSediment;

        var brushSize = new Vector2(radius * 2, radius * 2);
        mid *= GridPixelCount;
        mid -= brushSize / 2;
        Rect screenRect = new Rect(mid, brushSize);

        Texture tex = Texture2D.whiteTexture;

        amount /= GridPixelSize * GridPixelSize;
        amount = Vector4.Scale(amount, new Vector4(1f / 8f, 1f / 8f, 1f / 8f, 1f / 8f));
        _addSourceMaterial.SetVector("_Scale", amount);

        GL.PushMatrix();
        GL.LoadPixelMatrix(0, GridPixelCount, GridPixelCount, 0);
        Graphics.DrawTexture(screenRect, tex, _addSourceMaterial);
        GL.PopMatrix();

        RenderTexture.active = tmpActiveRT;
    }
}
