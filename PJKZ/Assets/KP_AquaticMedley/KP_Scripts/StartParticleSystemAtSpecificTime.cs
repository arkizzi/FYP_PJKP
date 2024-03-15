using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartParticleSystemAtSpecificTime : MonoBehaviour
{
    public float desiredPlaybackTime = 4f; // Time in seconds to set the playback

    private ParticleSystem particleSystem;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        SetPlaybackTime(desiredPlaybackTime);
    }

    void SetPlaybackTime(float timeInSeconds)
    {
        particleSystem.Simulate(timeInSeconds, true, false);
        particleSystem.Play(); // Start playing the Particle System
    }
}
