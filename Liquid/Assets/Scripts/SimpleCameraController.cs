using UnityEngine;

public class SimpleCameraController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float _speed = 3.5f;
    public float _rotationSpeed = 0.5f;

    [Header("Look limitation")]
    public float _minLookAngle = 2f;
    public float _maxLookAngle = 2f;

    private InputMaster _input;

    float GetInputRotationDirection()
    {
        float rot = 0;
        if (Input.GetKey(KeyCode.Q))
        {
            rot -= 1;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rot += 1;
        }
        return rot;
    }

    void Awake()
    {
        _input = new InputMaster();
        _input.Player.Enable();
        _input.Mouse.Enable();
    }

    void Update()
    {
        // Translation
        Vector2 inputMovement = _input.Player.Movement.ReadValue<Vector2>();
        Vector3 dir3 = new Vector3(inputMovement.x, 0, inputMovement.y);
        Vector3 translation = dir3 * _speed * Time.deltaTime;
        transform.position = transform.position + Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0) * translation;

        // Rotation
        if (_input.Player.Look.ReadValue<float>() > 0.5f)
        {
            float mouseX = _input.Mouse.XDelta.ReadValue<float>();
            float mouseY = _input.Mouse.YDelta.ReadValue<float>();
            if (Application.platform == RuntimePlatform.WebGLPlayer) // TEMP: Fix for inverted Y on WebGL
            {
                mouseY = -mouseY;
            }
            Vector3 rot = transform.rotation.eulerAngles;
            Quaternion rotLeftRight = Quaternion.AngleAxis(mouseX * _rotationSpeed, Vector3.up);
            Quaternion rotUpDown = Quaternion.AngleAxis(-mouseY * _rotationSpeed, Vector3.right);
            transform.rotation = rotLeftRight * transform.rotation * rotUpDown; // order is very important
        }
    }
}