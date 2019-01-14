using UnityEngine;

namespace VFX.Explosion
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] 
        private AudioSource source;

        [SerializeField]
        private AudioClip clip;
    
        [SerializeField]
        private ParticleSystem system;
    
        public void Explode(Vector3 effectPosition)
        {
            transform.position = effectPosition;
            source.PlayOneShot(clip);
            system.Play();
        }
    }
}
