using System.Collections;
using Game;
using UnityEngine;
using UnityEngine.SceneManagement;
using VFX.Explosion;

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
     
        private Rigidbody rigidBody;

        [SerializeField]
        private Data gameData;

        private EngineSound engineSound;
        private EngineParticles engineParticles;

        [SerializeField]
        private Explosion explosionPrefab;
        
        [SerializeField]
        private Explosion successPrefab;

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            engineSound = GetComponent<EngineSound>();
            engineParticles = GetComponent<EngineParticles>();
        }

        private void Awake()
        {
            gameData.Init();
        }

        private void Update()
        {
            ProcessInput();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!gameData.IsAlive)
            {
                return;
            }           
            switch (other.gameObject.tag)
            {
                case "Obstacle":
                    gameData.Kill();
                    engineSound.Stop();
                    var explosion = Instantiate(explosionPrefab);
                    explosion.Explode(transform.position);
                    StartCoroutine(LoadLevel("Level1"));
                    break;
                case "Destination":
                    gameData.SetTranscended();
                    engineSound.Stop();
                    var succesExplosion = Instantiate(successPrefab);
                    succesExplosion.Explode(transform.position);
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
            if (!gameData.IsAlive)
            {
                return;
            }
            rigidBody.freezeRotation = true;
            engineParticles.PlayThrustParticles();
            ProcessThrustInput();
            ProcessRotationInput();
            rigidBody.freezeRotation = false;
        }

        private void ProcessThrustInput()
        {
            if (!Input.GetKey(KeyCode.Space))
            {            
                engineParticles.StopAllParticles();
                return;
            }
            
            rigidBody.AddRelativeForce(Vector3.up * rcsThrustForce);
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
