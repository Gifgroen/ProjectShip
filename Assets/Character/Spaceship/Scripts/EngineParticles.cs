using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Character.Spaceship
{
    public class EngineParticles : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem leftEngineParticles;
    
        [SerializeField]
        private ParticleSystem rightEngineParticles;

        public void PlayThrustParticles()
        {
            if (!leftEngineParticles.isPlaying)
            {
                leftEngineParticles.Play();
            }
        
            if (!rightEngineParticles.isPlaying)
            {
                rightEngineParticles.Play();
            }
        }

        public void StopAllParticles()
        {
            leftEngineParticles.Stop();
            rightEngineParticles.Stop();
        }
    }
}
