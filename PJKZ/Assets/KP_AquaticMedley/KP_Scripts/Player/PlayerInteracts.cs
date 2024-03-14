using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteracts : MonoBehaviour
{
    public AudioSource audioSource;
    public SimpleBeatDetection BeatDetector;

    private float time_after_beat = 0;
    private bool startTime = false;
    private float max_time_before_fail = 1f; //set this to how much longer after a beat should it be considered a fail;
    private float max_time_before_great = 0.5f; //anytime between this and fail would be considered "Okay"
    private float max_time_before_best = 0.25f; // any button press before this is a "Perfect" anytime after this is "Great"

    void Start()
    {
        BeatDetector.OnBeat += OnBeat; 
    }

    void Update()
    {
        if (startTime)
        {
            time_after_beat += Time.deltaTime;

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) 
            {
                if (time_after_beat < max_time_before_best)
                {
                    Debug.Log("Perfect!");
                }
                else if (time_after_beat < max_time_before_great)
                {
                    Debug.Log("Great!");
                }
                else if (time_after_beat < max_time_before_fail)
                {
                    Debug.Log("Bad!");
                }
                else
                {
                    Debug.Log("Miss");
                    startTime = false;
                }
            }
        }
    }

    void OnBeat()
    {
        time_after_beat = 0;
        startTime = true;
    }

    void AMTapSound()
    {
        //check if the AudioSource is present
        if (audioSource != null)
        {
            //play the audio clip attached to the AudioSource
            audioSource.Play();
        }
    }
}
