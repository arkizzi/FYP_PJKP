using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenkieChirpIndication : MonoBehaviour
{
    public SimpleBeatDetection beatProcessor1;
    public SimpleBeatDetection beatProcessor2; 
    public FishSpawn fishSpawn;
    public PlayerScore playerScore;
    public Animator animator;
    private bool canTriggerDoubleChirping = true; //chokehold for the double chirp (to prevent it from being spammed)

    private Coroutine resetAnimationCoroutine1;
    private Coroutine resetAnimationCoroutine2;
    private Coroutine FishGoByeCoroutine;

    void Start()
    {
        beatProcessor1.OnBeat += OnBeat1; 
        beatProcessor2.OnBeat += OnBeat2; 
    }


    void OnBeat1()
    {
        fishSpawn.DisplayFish();
        animator.SetBool("IsChirping", true); //trigger animation for beat detection 1
        if (resetAnimationCoroutine1 != null)
            StopCoroutine(resetAnimationCoroutine1);
        resetAnimationCoroutine1 = StartCoroutine(ResetAnimation("IsChirping", 1));

        FishGoByeCoroutine = StartCoroutine(ResetFishAfterDelay());
    }

    void OnBeat2()
    {
        if (canTriggerDoubleChirping)
        {
            animator.SetBool("IsDoubleChriping", true);
            canTriggerDoubleChirping = false; //the chokehold
            if (resetAnimationCoroutine2 != null)
                StopCoroutine(resetAnimationCoroutine2);
            resetAnimationCoroutine2 = StartCoroutine(ResetAnimation("IsDoubleChriping", 2));
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
        
        if (fishSpawn.fishSprite.enabled)
        {
            if (playerScore.missedFish == true)
            {
                fishSpawn.FadeOutFish();
            }
            else if (playerScore.successFish == true)
            {
                fishSpawn.GrowAndFadeOutFish();
            }
        }
    }
}