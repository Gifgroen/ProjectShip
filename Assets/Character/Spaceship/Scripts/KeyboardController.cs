using UnityEngine;
using UnityEngine.Serialization;

// ReSharper disable once CheckNamespace
namespace Character.Spaceship
{
    [RequireComponent(typeof(Rigidbody))]
    public class KeyboardController : MonoBehaviour
    {
        [SerializeField]
        private float rcsRotationForce = 32f;
        
        [SerializeField]
        private float rcsThrustForce = 16f;
     
        private Rigidbody _rigidBody;
    
        // Start is called before the first frame update
        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        private void Update()
        {
            ProcessInput();
        }

        private void ProcessInput()
        {
            _rigidBody.freezeRotation = true;
            ProcessThrustInput();
            ProcessRotationInput();
            _rigidBody.freezeRotation = false;
        }

        private void ProcessThrustInput()
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                return;
            }
            _rigidBody.AddRelativeForce(Vector3.up * rcsThrustForce);
        }

        private void ProcessRotationInput()
        {
            var frameRotation = rcsRotationForce * Time.deltaTime;
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(Vector3.forward * frameRotation);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(-Vector3.forward * frameRotation);
            }
        }
    }
}
