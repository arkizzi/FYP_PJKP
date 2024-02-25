using System.Collections;
using UnityEngine;

public class FishSpawn : MonoBehaviour
{
    public SpriteRenderer fishSprite;
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
    }

    public void DisplayFish()
    {
        DisableFishSprite(); // First, disable all sprites

        // Stop existing scaling coroutine before starting a new one
        if (EntranceCoroutine != null)
        {
            StopCoroutine(EntranceCoroutine);
        }

        fishSprite.enabled = true;
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
        playerScore.missedFish = false;
    }

    public void GrowAndFadeOutFish()
    {
        if(FishAnimator.GetBool("IsSingularBeatDetected?"))
        {
            FishAnimator.SetBool("IsSingularBeatDetected?", false);
        }

        FishAnimator.SetBool("SingularBeatSuccess?", true);
        playerScore.missedFish = false;
    }
}
