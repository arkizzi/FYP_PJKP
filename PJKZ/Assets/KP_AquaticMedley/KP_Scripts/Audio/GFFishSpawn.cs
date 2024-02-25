using System.Collections;
using UnityEngine;

public class GFFishSpawn : MonoBehaviour
{
    public PlayerScore playerScore;
    public Animator GFFishAnimator;
    private Coroutine GFEntranceCoroutine;

    void Start()
    {
        DisableFishSprite(); // Disable sprite on start
    }

    public void DisableFishSprite()
    {
        GFFishAnimator.SetBool("IsSingularBeatDetected?", false);
        GFFishAnimator.SetBool("SingularBeatMiss?", false);
        GFFishAnimator.SetBool("SingularBeatSuccess?", false);
        GFFishAnimator.SetBool("FishIdle?", false);
    }

    public void DisplayFish()
    {
        DisableFishSprite(); // First, disable all sprites

        // Stop existing scaling coroutine before starting a new one
        if (GFEntranceCoroutine != null)
        {
            StopCoroutine(GFEntranceCoroutine);
        }

        AnimateFishIn();
    }

    public void AnimateFishIn()
    {
        GFFishAnimator.SetBool("IsSingularBeatDetected?", true);
        GFFishAnimator.SetBool("FishIdle?", true);
    }

    public void FadeOutFish()
    {
        if(GFFishAnimator.GetBool("IsSingularBeatDetected?"))
        {
            GFFishAnimator.SetBool("IsSingularBeatDetected?", false);
        }

        GFFishAnimator.SetBool("SingularBeatMiss?", true);
        GFFishAnimator.SetBool("SingularBeatSuccess?", false);
        playerScore.missedFish = false;
    }

    public void GrowAndFadeOutFish()
    {
        if(GFFishAnimator.GetBool("IsSingularBeatDetected?"))
        {
            GFFishAnimator.SetBool("IsSingularBeatDetected?", false);
        }

        GFFishAnimator.SetBool("SingularBeatSuccess?", true);
        GFFishAnimator.SetBool("SingularBeatMiss?", false);
        playerScore.successFish = false;
    }
}
