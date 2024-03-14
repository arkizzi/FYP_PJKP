using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenkieChirpIndication : MonoBehaviour
{
    public SimpleBeatDetection BeatDetector;
    public Animator animator;
    private Coroutine Anim1Coroutine;

    void Start()
    {
        BeatDetector.OnBeat += OnBeat; 
    }

    void OnBeat()
    {
        animator.SetBool("IsDoubleChriping", true); //trigger animation for beat detection 1
        if (Anim1Coroutine != null)
            StopCoroutine(Anim1Coroutine);

        Anim1Coroutine = StartCoroutine(ResetAnimation("IsDoubleChriping"));
    }


    IEnumerator ResetAnimation(string parameterName)
    {
        yield return new WaitForSeconds(0.125f);
        animator.SetBool(parameterName, false);
        yield break; 

    }

}