using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFFishSpawn : MonoBehaviour
{
    public PlayerScore playerScore;
    public Animator BFFishAnimator;
    private Coroutine BFEntranceCoroutine;
    public bool isFishB;
    public float animationDelay = 0.15f; // Delay for animations on FishB

    void Start()
    {
        DisableFishSprite(); // Disable sprite on start
    }

    public void DisableFishSprite()
    {
        BFFishAnimator.SetBool("IsSingularBeatDetected?", false);
        BFFishAnimator.SetBool("SingularBeatMiss?", false);
        BFFishAnimator.SetBool("SingularBeatSuccess?", false);
        BFFishAnimator.SetBool("FishIdle?", false);
    }

    public void DisplayFish()
    {
        DisableFishSprite(); // First, disable all sprites

        // Stop existing scaling coroutine before starting a new one
        if (BFEntranceCoroutine != null)
        {
            StopCoroutine(BFEntranceCoroutine);
        }

        StartCoroutine(AnimateFishInWithDelay());
    }

    IEnumerator AnimateFishInWithDelay()
    {
        yield return new WaitForSeconds(isFishB ? animationDelay : 0f); // Add delay if it's FishB

        AnimateFishIn();
    }

    public void AnimateFishIn()
    {
        BFFishAnimator.SetBool("IsSingularBeatDetected?", true);
        BFFishAnimator.SetBool("FishIdle?", true);
    }

    public void FadeOutFish()
    {
        if(BFFishAnimator.GetBool("IsSingularBeatDetected?"))
        {
            BFFishAnimator.SetBool("IsSingularBeatDetected?", false);
        }

        BFFishAnimator.SetBool("SingularBeatMiss?", true);
        BFFishAnimator.SetBool("SingularBeatSuccess?", false);
        playerScore.missedFish = false;
    }

    public void GrowAndFadeOutFish()
    {
        if(BFFishAnimator.GetBool("IsSingularBeatDetected?"))
        {
            BFFishAnimator.SetBool("IsSingularBeatDetected?", false);
        }

        BFFishAnimator.SetBool("SingularBeatSuccess?", true);
        BFFishAnimator.SetBool("SingularBeatMiss?", false);
        playerScore.successFish = false;
    }
}
