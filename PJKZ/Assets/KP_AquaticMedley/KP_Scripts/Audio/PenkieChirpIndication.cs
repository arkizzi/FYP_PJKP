using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenkieChirpIndication : MonoBehaviour
{
    public SimpleBeatDetection beatProcessor1;
    public SimpleBeatDetection beatProcessor2; 
    public GFFishSpawn gfFishSpawn;
    public BFFishSpawn bfFishASpawn;
    public BFFishSpawn bfFishBSpawn;
    public PlayerScore playerScore;
    public Animator animator;
    private bool canTriggerDoubleChirping = true; //chokehold for the double chirp (to prevent it from being spammed)
    private Coroutine Anim1Coroutine;
    private Coroutine Anim2Coroutine;
    private Coroutine GoodFishGoByeCoroutine;
    private Coroutine BadFishAGoByeCoroutine;
    private Coroutine BadFishBGoByeCoroutine;

    void Start()
    {
        beatProcessor1.OnBeat += OnBeat1; 
        beatProcessor2.OnBeat += OnBeat2; 
    }

    void OnBeat1()
    {
        gfFishSpawn.DisplayFish();
        animator.SetBool("IsChirping", true); //trigger animation for beat detection 1
        if (Anim1Coroutine != null)
            StopCoroutine(Anim1Coroutine);
        Anim1Coroutine = StartCoroutine(ResetAnimation("IsChirping", 1));

        GoodFishGoByeCoroutine = StartCoroutine(ResetFishAfterDelay());
    }

    void OnBeat2()
    {
        if (canTriggerDoubleChirping)
        {
            bfFishASpawn.DisplayFish();
            bfFishBSpawn.DisplayFish();

            animator.SetBool("IsDoubleChriping", true);
            canTriggerDoubleChirping = false; //the chokehold
            if (Anim2Coroutine != null)
                StopCoroutine(Anim2Coroutine);
            Anim2Coroutine = StartCoroutine(ResetAnimation("IsDoubleChriping", 2));

            BadFishAGoByeCoroutine = StartCoroutine(ResetBadFishAAfterDelay());
            BadFishBGoByeCoroutine = StartCoroutine(ResetBadFishABfterDelay());
        }
    }

    IEnumerator ResetAnimation(string parameterName, int processorNumber)
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool(parameterName, false);
        if (processorNumber == 1)
            yield break; 
        else if (processorNumber == 2)
            yield return new WaitForSeconds(1f); //coolddown to minimize spam
        canTriggerDoubleChirping = true;
    }

    IEnumerator ResetFishAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        
        if (gfFishSpawn.GFFishAnimator.GetBool("FishIdle?"))
        {
            if (playerScore.missedFish == true)
            {
                gfFishSpawn.FadeOutFish();
            }
            else if (playerScore.successFish == true)
            {
                gfFishSpawn.GrowAndFadeOutFish();
            }
        }
    }

    IEnumerator ResetBadFishAAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        
        if (bfFishASpawn.BFFishAnimator.GetBool("FishIdle?"))
        {
            if (playerScore.missedFish == true)
            {
                bfFishASpawn.FadeOutFish();
            }
            else if (playerScore.successFish == true)
            {
                bfFishASpawn.GrowAndFadeOutFish();
            }
        }
    }

    IEnumerator ResetBadFishABfterDelay()
    {
        yield return new WaitForSeconds(1f + bfFishBSpawn.animationDelay);
        
        if (bfFishBSpawn.BFFishAnimator.GetBool("FishIdle?"))
        {
            if (playerScore.missedFish == true)
            {
                bfFishBSpawn.FadeOutFish();
            }
            else if (playerScore.successFish == true)
            {
                bfFishBSpawn.GrowAndFadeOutFish();
            }
        }
    }
}