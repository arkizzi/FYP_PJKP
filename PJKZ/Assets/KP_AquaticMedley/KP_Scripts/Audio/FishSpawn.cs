using System.Collections;
using UnityEngine;

public class FishSpawn : MonoBehaviour
{
    public SimpleBeatDetection BeatDetector;
    public Animator FishAnimator;
    public bool isSecondFish;
    private Coroutine EntranceCoroutine;

    void Start()
    {
        BeatDetector.OnBeat += OnBeat; 
    }

    void OnBeat()
    {
        if (isSecondFish)
        {
            StartCoroutine(SecondFishSpawn()); // Start the coroutine
        }
        else
        {
            DisplayFish();
        }
    }
    public void DisplayFish()
    {
        // Stop existing scaling coroutine before starting a new one
        if (EntranceCoroutine != null)
        {
            StopCoroutine(EntranceCoroutine);
        }

        AnimateFishIn();
        //add a condition when the player taps 
        StartCoroutine(FadeOutAfterDelay());
    }

    IEnumerator SecondFishSpawn()
    {
        yield return new WaitForSeconds(0.106f);
        DisplayFish();
    }

    IEnumerator FadeOutAfterDelay()
    {
        yield return new WaitForSeconds(1.5f); // Wait for 1 second
        FadeOutFish(); // Call FadeOutFish after the delay
    }

    public void AnimateFishIn()
    {
        FishAnimator.SetBool("GoodFishSuccess", false);
        FishAnimator.SetBool("GoodFishMiss", false);
        FishAnimator.SetBool("GoodFishAppears", true);
    }

    public void FadeOutFish()
    {
        FishAnimator.SetBool("GoodFishMiss", true);
        FishAnimator.SetBool("GoodFishAppears", false);
    }

    public void GrowAndFadeOutFish()
    {
        FishAnimator.SetBool("GoodFishSuccess", true);
        FishAnimator.SetBool("GoodFishAppears", false);
    }
}
