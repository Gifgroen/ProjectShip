using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

// ReSharper disable once CheckNamespace
namespace Character.Spaceship
{
    [RequireComponent(typeof(AudioSource))]
    public class ThrustSound : MonoBehaviour
    {
        private AudioSource _audioSource;

        [SerializeField]
        private AudioClip thrustClip;
        [SerializeField]
        private AudioClip explosionClip;
        [SerializeField]
        private AudioClip destinationClip;

        private bool dead = false;
        
        // Start is called before the first frame update
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            ProcessInput();
        }
        
        public void PlayThrust()
        {
            if (_audioSource.isPlaying)
            {
                return;
            }

            _audioSource.PlayOneShot(thrustClip);
        }

        public void Stop()
        {
            _audioSource.Stop();
        }

        public void PlayExplosion()
        {
            dead = true;
            _audioSource.PlayOneShot(explosionClip);
        }

        public void PlaySuccess()
        {
            dead = true;
            _audioSource.PlayOneShot(destinationClip);
        }

        private void ProcessInput()
        {
            if (dead)
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
    }
}
