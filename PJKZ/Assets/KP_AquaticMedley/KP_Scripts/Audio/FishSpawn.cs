using System.Collections;
using UnityEngine;

public class FishSpawn : MonoBehaviour
{
    public PlayerScore playerScore;
    public Animator FishAnimator;
    private Coroutine EntranceCoroutine;

    void Start()
    {
        DisableFishSprite(); // Disable sprite on start
    }

    public void DisableFishSprite()
    {
        FishAnimator.SetBool("IsSingularBeatDetected?", false);
        FishAnimator.SetBool("SingularBeatMiss?", false);
        FishAnimator.SetBool("SingularBeatSuccess?", false);
        FishAnimator.SetBool("FishIdle?", false);
    }

    public void DisplayFish()
    {
        DisableFishSprite(); // First, disable all sprites

        // Stop existing scaling coroutine before starting a new one
        if (EntranceCoroutine != null)
        {
            StopCoroutine(EntranceCoroutine);
        }

        AnimateFishIn();
    }

    public void AnimateFishIn()
    {
        FishAnimator.SetBool("IsSingularBeatDetected?", true);
        FishAnimator.SetBool("FishIdle?", true);
    }

    public void FadeOutFish()
    {
        if(FishAnimator.GetBool("IsSingularBeatDetected?"))
        {
            FishAnimator.SetBool("IsSingularBeatDetected?", false);
        }

        FishAnimator.SetBool("SingularBeatMiss?", true);
        FishAnimator.SetBool("SingularBeatSuccess?", false);
        playerScore.missedFish = false;
    }

    public void GrowAndFadeOutFish()
    {
        if(FishAnimator.GetBool("IsSingularBeatDetected?"))
        {
            FishAnimator.SetBool("IsSingularBeatDetected?", false);
        }

        FishAnimator.SetBool("SingularBeatSuccess?", true);
        FishAnimator.SetBool("SingularBeatMiss?", false);
        playerScore.successFish = false;
    }
}
