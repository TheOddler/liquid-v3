using UnityEngine;

public class Raining : MonoBehaviour
{
    [SerializeField]
    Simulation _simulation = null;

    [Header("Settings")]
    [SerializeField]
    float _radius = 5.0f;
    [SerializeField]
    float _dropRadius = 2.0f;
    [SerializeField]
    float _amountPerSecond = 1.0f;

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

        // Rain
        if (_input.Player.AddWater.ReadValue<float>() > 0.5f)
        {
            float simSize = _simulation.transform.lossyScale.x; //assume it's equally scaled
            Vector2 rand = Random.insideUnitCircle * _radius;
            Vector2 posInSim = new Vector2(
                simSize / 2f + (transform.position.x + rand.x - _simulation.transform.position.x),
                simSize / 2f - (transform.position.z + rand.y - _simulation.transform.position.z)
                ) / simSize;
            _simulation.AddWaterSandRockSediment(posInSim, _dropRadius, new Vector4(_amountPerSecond * Time.deltaTime, 0, 0, 0));
        }
    }
}
