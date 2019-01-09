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

        private ThrustSound _soundManager;
        private ParticleManager _particleManager;

        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _soundManager = GetComponent<ThrustSound>();
            _particleManager = GetComponent<ParticleManager>();
        }

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
                    state = State.Died;
                    _soundManager.Stop();
                    _soundManager.PlayExplosion();
                    StartCoroutine(LoadLevel("Level1"));
                    break;
                case "Destination":
                    state = State.Transcended;
                    _soundManager.Stop();
                    _soundManager.PlaySuccess();
                    StartCoroutine(LoadLevel("Level2"));
                    break;
                default:
                    /* NOOP */
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
            _particleManager.PlayThrustParticles();
            ProcessThrustInput();
            ProcessRotationInput();
            _rigidBody.freezeRotation = false;
        }

        private void ProcessThrustInput()
        {
            if (!Input.GetKey(KeyCode.Space))
            {            
                _particleManager.StopAllParticles();
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
