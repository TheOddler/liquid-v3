using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField]
    Simulation _simulation = null;

    [Header("Settings")]
    [SerializeField]
    float _radius = 5.0f;
    public float Radius { get => _radius; set => _radius = value; }
    [SerializeField]
    float _dropRadius = 2.0f;
    public float DropRadius { get => _dropRadius; set => _dropRadius = value; }
    [SerializeField]
    float _amountPerSecond = 1.0f;
    public float AmountPerSecond { get => _amountPerSecond; set => _amountPerSecond = value; }

    private InputMaster _input;

    void Awake()
    {
        _input = new InputMaster();
        _input.Player.Enable();
        _input.Mouse.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        // Move cloud
        Vector2 mousePos = new Vector2(_input.Mouse.XPosition.ReadValue<float>(), _input.Mouse.YPosition.ReadValue<float>());
        if (Application.platform == RuntimePlatform.WebGLPlayer) // TEMP: Fix for inverted Y on WebGL
        {
            mousePos.y = Screen.height - mousePos.y;
        }
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Vector3 castPosition = ray.origin + ray.direction * (ray.origin.y - transform.position.y) / -ray.direction.y;
        transform.position = castPosition;
        float simSize = _simulation.transform.lossyScale.x; //assume it's equally scaled
        Vector3 simPos = _simulation.transform.position;

        if (Mathf.Abs(castPosition.x - simPos.x) < simSize / 2f && Mathf.Abs(castPosition.z - simPos.z) < simSize / 2f) // inside simulation
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }

            // Rain
            if (_input.Player.AddWater.ReadValue<float>() > 0.5f)
            {
                Vector2 rand = Random.insideUnitCircle * Radius;
                Vector2 posInSim = new Vector2(
                    simSize / 2f + (transform.position.x + rand.x - simPos.x),
                    simSize / 2f - (transform.position.z + rand.y - simPos.z)
                    ) / simSize;
                _simulation.AddWaterSandRockSediment(posInSim, DropRadius, new Vector4(AmountPerSecond * Time.deltaTime, 0, 0, 0));
            }
        }
        else
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
