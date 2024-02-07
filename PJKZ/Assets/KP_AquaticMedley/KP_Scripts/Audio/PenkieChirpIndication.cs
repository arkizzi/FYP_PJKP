using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenkieChirpIndication : MonoBehaviour
{
    public SimpleBeatDetection beatProcessor;
    public Animator animator;
    public bool toChirpOrNotToChirp = false;
    private bool beatDetected = false;

        void Start()
    {
        beatProcessor.OnBeat += OnBeat;
        beatDetected = false; 
    }

    void Update()
    {
        if (!beatDetected)
        {
            animator.SetBool("IsChirping", false);
        }
    }

    void OnBeat()
    {
        beatDetected = true; 
        animator.SetBool("IsChirping", true);
        beatDetected = false;
    }
}
