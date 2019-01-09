using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
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
