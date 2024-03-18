using System.Collections;
using UnityEngine;

public class FishSpawn : MonoBehaviour
{
    public SimpleBeatDetection BeatDetector;
    public BubbleSpawn bubble;
    public Animator FishAnimator;
    public bool isSecondFish;
    public bool isZapped;
    public PlayerInteracts sucessor;
    private Coroutine EntranceCoroutine;

    void Start()
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
                bubble.BubblePop();
            }
            else if (sucessor.fishSpawnCount == 1 && !isSecondFish)
            {
                GrowAndFadeOutFish();
                bubble.BubblePop();
            }
        }
        
        if (sucessor.failTap)
        {
            if (sucessor.fishSpawnCount == 2 && isSecondFish)
            {
                StartCoroutine(FadeOutAfterDelay()); 
                bubble.StartCoroutine(bubble.DissapearAfterDelay()); 
            }
            else if (sucessor.fishSpawnCount == 1 && !isSecondFish)
            {
                StartCoroutine(FadeOutAfterDelay()); 
                StartCoroutine(bubble.DissapearAfterDelay()); 
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
        FishAnimator.SetBool("FishZapped", false);
        FishAnimator.SetBool("GoodFishSuccess", false);
        FishAnimator.SetBool("GoodFishMiss", false);
        FishAnimator.SetBool("GoodFishAppears", true);
    }

    public void FadeOutFish()
    {
        FishAnimator.SetBool("GoodFishMiss", true);
        FishAnimator.SetBool("GoodFishAppears", false);
        sucessor.failTap = false;
    }

    public void GrowAndFadeOutFish()
    {
        if (isZapped)
        {
            FishAnimator.SetBool("FishZapped", true);
            FishAnimator.SetBool("GoodFishAppears", false);
        }
        else
        {
            FishAnimator.SetBool("GoodFishSuccess", true);
            FishAnimator.SetBool("GoodFishAppears", false);
        }
        sucessor.successTap = false;
    }
}
