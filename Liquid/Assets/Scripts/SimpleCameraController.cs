using UnityEngine;

namespace UnityTemplateProjects
{
    public class SimpleCameraController : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float _speed = 3.5f;
        public float _rotationSpeed = 30f;
        public float _runBoost = 5f;

        Vector3 GetInputTranslationDirection()
        {
            Vector3 direction = new Vector3();
            if (Input.GetKey(KeyCode.W))
            {
                direction += Vector3.forward;
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction += Vector3.back;
            }
            if (Input.GetKey(KeyCode.A))
            {
                direction += Vector3.left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction += Vector3.right;
            }
            return direction;
        }

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

        void Update()
        {
            // Rotation
            Vector3 rot = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(rot.x, rot.y + GetInputRotationDirection() * _rotationSpeed * Time.deltaTime, rot.z);

            // Translation
            var translation = GetInputTranslationDirection() * _speed * Time.deltaTime;

            // Speed up movement when shift key held
            if (Input.GetKey(KeyCode.LeftShift))
            {
                translation *= _runBoost;
            }

            transform.position = transform.position + Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0) * translation;
        }
    }

}