using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Character.Spaceship
{
    [RequireComponent(typeof(AudioSource))]
    public class EngineSound : MonoBehaviour
    {
        private AudioSource _audioSource;

        [SerializeField]
        private AudioClip thrustClip;

        [SerializeField]
        private Game.Data gameData;
        
        // Start is called before the first frame update
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            ProcessInput();
        }

        public void Stop()
        {
            _audioSource.Stop();
        }

        private void ProcessInput()
        {
            if (!gameData.IsAlive)
            {
                return;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                PlayThrust();
            }
            else
            {
                _audioSource.Stop();
            }
        }

        private void PlayThrust()
        {
            if (_audioSource.isPlaying)
            {
                return;
            }

            _audioSource.PlayOneShot(thrustClip);
        }
    }
}
