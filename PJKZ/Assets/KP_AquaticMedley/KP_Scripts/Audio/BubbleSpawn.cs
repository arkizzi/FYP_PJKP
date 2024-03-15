using System.Collections;
using UnityEngine;

public class BubbleSpawn : MonoBehaviour
{
    public SimpleBeatDetection BeatDetector;
    public Animator bubbleAnim;
    public bool isSecondBubble;
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
            if (sucessor.fishSpawnCount == 2 && isSecondBubble)
            {
                BubblePop();
            }
            else if (sucessor.fishSpawnCount == 1 && !isSecondBubble)
            {
                BubblePop();
            }
        }
        
        if (sucessor.failTap)
        {
            if (sucessor.fishSpawnCount == 2 && isSecondBubble)
            {
                StartCoroutine(DissapearAfterDelay()); 
            }
            else if (sucessor.fishSpawnCount == 1 && !isSecondBubble)
            {
                StartCoroutine(DissapearAfterDelay()); 
            }
        }
    }

    void OnBeat()
    {
        if (isSecondBubble)
        {
            StartCoroutine(SecondBubbleSpawn(DisplayBubble)); // Start the coroutine
        }
        else
        {
            DisplayBubble();
        }
    }
    public void DisplayBubble()
    {
        // Stop existing scaling coroutine before starting a new one
        if (EntranceCoroutine != null)
        {
            StopCoroutine(EntranceCoroutine);
        }

        SpawnBubble();
    }

    IEnumerator SecondBubbleSpawn(System.Action method)
    {
        yield return new WaitForSeconds(0.375f);
        method();
    }

    IEnumerator DissapearAfterDelay()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second
        FadeOutBubble(); // Call FadeOutFish after the delay
    }

    public void SpawnBubble()
    {
        bubbleAnim.SetBool("BubbleFades", false);
        bubbleAnim.SetBool("BubblePops", false);
        bubbleAnim.SetBool("BubbleAppears", true);
    }

    public void FadeOutBubble()
    {
        bubbleAnim.SetBool("BubbleFades", true);
        bubbleAnim.SetBool("BubbleAppears", false);
    }

    public void BubblePop()
    {
        bubbleAnim.SetBool("BubblePops", true);
        bubbleAnim.SetBool("BubbleAppears", false);
    }
}
