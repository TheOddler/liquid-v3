using UnityEngine;

public class WaterAdder : MonoBehaviour
{
    [SerializeField]
    Simulation _simulation = null;

    [Header("Settings")]
    [SerializeField]
    float _radius = 2.0f;
    [SerializeField]
    [Tooltip("Amount per second")]
    float _amount = 1.0f;

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
        Vector2 mousePos = new Vector2(_input.Mouse.XPosition.ReadValue<float>(), _input.Mouse.YPosition.ReadValue<float>());
        if (Application.platform == RuntimePlatform.WebGLPlayer) // TEMP: Fix for inverted Y on WebGL
        {
            mousePos.y = Screen.height - mousePos.y;
        }
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Vector3 castPosition = ray.origin + ray.direction * (ray.origin.y - transform.position.y) / -ray.direction.y;
        transform.position = castPosition;

        if (_input.Player.AddWater.ReadValue<float>() > 0.5f)
        {
            float simSize = _simulation.transform.lossyScale.x; //assume it's equally scaled
            Vector2 posInSim = new Vector2(
                simSize / 2f + (castPosition.x - _simulation.transform.position.x),
                simSize / 2f - (castPosition.z - _simulation.transform.position.z)
                ) / simSize;
            _simulation.AddWaterSandRockSediment(posInSim, _radius, new Vector4(_amount * Time.deltaTime, 0, 0, 0));
        }
    }
}
