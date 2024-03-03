using UnityEngine;
using System;
using System.Collections.Generic;

public class BeatPreCalculator : MonoBehaviour
{
    public AudioSource audioSource;

    private List<float> beatTimestamps = new List<float>();
    private float lastBeatTime;
    public float beatCooldown = 1.0f;

    public List<float> GetBeatTimestamps()
    {
        return beatTimestamps;
    }

    public void AnalyzeAudioClip()
    {
        if (audioSource == null || audioSource.clip == null)
        {
            Debug.LogError("Audio source or audio clip is null.");
            return;
        }

        float[] samples = new float[audioSource.clip.samples * audioSource.clip.channels];
        audioSource.clip.GetData(samples, 0);

        int bufferSize = 1024; // Adjust according to your needs
        int numBuffers = samples.Length / bufferSize;

        float[] instantEnergies = new float[numBuffers];

        // Compute instant energies for each buffer
        for (int i = 0; i < numBuffers; i++)
        {
            float instantEnergy = 0;

            for (int j = 0; j < bufferSize; j++)
            {
                int index = i * bufferSize + j;
                instantEnergy += samples[index] * samples[index];
            }

            instantEnergies[i] = instantEnergy;
        }

        float localAverageEnergy = CalculateLocalAverageEnergy(instantEnergies);
        float variance = CalculateVariance(instantEnergies, localAverageEnergy);
        double constantC = (-0.0025714 * variance) + 1.5142857;

        // Detect beats
        for (int i = 0; i < numBuffers; i++)
        {
            if (instantEnergies[i] > constantC * localAverageEnergy)
            {
                float currentTime = i * bufferSize / (float)audioSource.clip.frequency;

                // Check if enough time has passed since the last beat
                if (currentTime - lastBeatTime >= beatCooldown)
                {
                    beatTimestamps.Add(currentTime);
                    lastBeatTime = currentTime;
                }
            }
        }
    }

    private float CalculateLocalAverageEnergy(float[] instantEnergies)
    {
        float sum = 0;
        foreach (float energy in instantEnergies)
        {
            sum += energy;
        }
        return sum / instantEnergies.Length;
    }

    private float CalculateVariance(float[] instantEnergies, float localAverageEnergy)
    {
        float sum = 0;
        foreach (float energy in instantEnergies)
        {
            sum += Mathf.Pow(energy - localAverageEnergy, 2);
        }
        return sum / instantEnergies.Length;
    }
}
