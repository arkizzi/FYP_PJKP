using UnityEngine;
using System.Collections.Generic;

public class NewPlayerScoreTest : MonoBehaviour
{
    public AudioSource audioSource;
    public BeatPreCalculator beatDetection;

    void Start()
    {
        if (beatDetection == null)
        {
            Debug.LogError("BeatDetection component not assigned.");
            return;
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not assigned.");
            return;
        }

        // Analyze the audio clip for beats
        beatDetection.AnalyzeAudioClip();

        // Retrieve the list of beat timestamps
        List<float> beatTimestamps = beatDetection.GetBeatTimestamps();

        // Print the timestamps
        foreach (float timestamp in beatTimestamps)
        {
            Debug.Log("Beat timestamp: " + timestamp.ToString("F3") + " seconds");
        }
    }
}
