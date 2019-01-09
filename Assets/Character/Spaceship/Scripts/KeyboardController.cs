using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

// ReSharper disable once CheckNamespace
namespace Character.Spaceship
{
    [RequireComponent(typeof(Rigidbody))]
    public class KeyboardController : MonoBehaviour
    {
        public float waitTime = 1f;
        
        [SerializeField]
        private float rcsRotationForce = 32f;
        
        [SerializeField]
        private float rcsThrustForce = 16f;
     
        private Rigidbody _rigidBody;

        private enum State
        {
            Alive,
            Died,
            Transcended
        };

        private State state = State.Alive;
        
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

        private void OnCollisionEnter(Collision other)
        {
            if (state != State.Alive)
            {
                return;
            }
            switch (other.gameObject.tag)
            {
                case "Obstacle":
                    Debug.Log("You died!", other.gameObject);
                    state = State.Died;
                    StartCoroutine(LoadLevel("Level1"));
                    break;
                case "Destination":
                    state = State.Transcended;
                    Debug.Log("You won!", other.gameObject);
                    StartCoroutine(LoadLevel("Level2"));
                    break;
                default:
                    Debug.Log("You hit something else!", other.gameObject);
                    break;
            }
        }

        private IEnumerator LoadLevel(string levelName)
        {
            yield return new WaitForSeconds(waitTime);
            SceneManager.LoadScene(levelName);
        }

        private void ProcessInput()
        {
            if (state != State.Alive)
            {
                return;
            }
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
