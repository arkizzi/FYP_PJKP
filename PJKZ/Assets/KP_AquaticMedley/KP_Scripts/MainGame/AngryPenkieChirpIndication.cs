using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryPenkieChirpIndication : MonoBehaviour
{
    public SimpleBeatDetection BeatDetector;
    public Animator animator;
    public BeatCue bq;
    private Coroutine Anim1Coroutine;

    void Start()
    {
        BeatDetector.OnBeat += OnBeat; 
    }

    void OnBeat()
    {
        if (bq.isntAngry)
        {
            animator.SetBool("IsDoubleChriping", true); //trigger animation for beat detection 1
            if (Anim1Coroutine != null)
                StopCoroutine(Anim1Coroutine);

            Anim1Coroutine = StartCoroutine(ResetAnimation("IsDoubleChriping"));
        }
        else
        {
            animator.SetBool("AngryAttacking", true); //trigger animation for beat detection 1
            if (Anim1Coroutine != null)
                StopCoroutine(Anim1Coroutine);

            Anim1Coroutine = StartCoroutine(ResetAnimation("AngryAttacking"));
        }

    }


    IEnumerator ResetAnimation(string parameterName)
    {
        yield return new WaitForSeconds(0.125f);
        animator.SetBool(parameterName, false);
        yield break; 

    }

}