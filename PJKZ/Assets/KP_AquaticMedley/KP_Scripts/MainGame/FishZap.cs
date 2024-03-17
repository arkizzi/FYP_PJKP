using System.Collections;
using UnityEngine;

public class FishZap : MonoBehaviour
{
    public SimpleBeatDetection BeatDetector;
    public Animator FishAnimator;
    public bool isSecondFish;
    public PlayerInteracts sucessor;
    private Coroutine EntranceCoroutine;

    void OnEnable()
    {
        BeatDetector.OnBeat += OnBeat; 
    }

    void Update()
    {
        if (sucessor.successTap)
        {
            
            if (sucessor.fishSpawnCount == 2 && isSecondFish)
            {
                GrowAndFadeOutFish();
            }
            else if (sucessor.fishSpawnCount == 1 && !isSecondFish)
            {
                GrowAndFadeOutFish();
            }
        }
        
        if (sucessor.failTap)
        {
            if (sucessor.fishSpawnCount == 2 && isSecondFish)
            {
                StartCoroutine(FadeOutAfterDelay()); 
            }
            else if (sucessor.fishSpawnCount == 1 && !isSecondFish)
            {
                StartCoroutine(FadeOutAfterDelay()); 
            }
        }
    }

    void OnBeat()
    {
        if (isSecondFish)
        {
            StartCoroutine(SecondFishSpawn(DisplayFish)); 
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
    }

    IEnumerator SecondFishSpawn(System.Action method)
    {
        yield return new WaitForSeconds(0.375f);
        method();
    }

    IEnumerator FadeOutAfterDelay()
    {
        yield return new WaitForSeconds(1f); 
        FadeOutFish();
    }

    public void AnimateFishIn()
    {
        FishAnimator.SetBool("GoodFishSuccess", false);
        FishAnimator.SetBool("FishZapped", false);
        FishAnimator.SetBool("GoodFishAppears", true);
    }

    public void FadeOutFish()
    {
        FishAnimator.SetBool("FishZapped", true);
        FishAnimator.SetBool("GoodFishAppears", false);
        sucessor.failTap = false;
    }

    public void GrowAndFadeOutFish()
    {
        FishAnimator.SetBool("FishZapped", true);
        FishAnimator.SetBool("GoodFishAppears", false);
        sucessor.successTap = false;
    }
}
