using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public SimpleBeatDetection beatProcessor;
    public PlayerAccuracyIndicators playerAccuracyIndicators;
    public float tapThreshold = 0.4f;
    public float resetTime = 0.5f;
    public int score;
    public bool missedFish = false;
    public bool successFish = false;
    
    private float lastTapTime;

    private Coroutine fadeOutCoroutine;
    private Coroutine missNoTapCoroutine;

    void Start()
    {
        beatProcessor.OnBeat += OnBeat;
        score = 0;
    }

    //function to handle taps from PlayerInteracts script
    public void HandleTap()
    {
        if (IsTapOnBeat())
        {
            //stop miss no tap
            if (missNoTapCoroutine != null)
            {
                StopCoroutine(missNoTapCoroutine);
            }

            float timingDifference = Mathf.Abs(Time.time - lastTapTime);

            if (timingDifference < 0.2f)
            {
                successFish = true;
                score += 500;
                playerAccuracyIndicators.DisplayAccuracySprite(0);
                Debug.Log("Perfect!");
            }
            else if (timingDifference < 0.3f)
            {
                successFish = true;
                score += 100;
                playerAccuracyIndicators.DisplayAccuracySprite(1);
                Debug.Log("Good!");
            }
            else if (timingDifference < 0.4f)
            {
                successFish = true;
                score += 50;
                playerAccuracyIndicators.DisplayAccuracySprite(2);
                Debug.Log("Bad!");
            }
            else
            {
                missedFish = true;
                score += 0;
                playerAccuracyIndicators.DisplayAccuracySprite(3);
                Debug.Log("Miss!");
            }

            Debug.Log("Score: " + score);
            
            // Stop existing fade-out coroutine before starting a new one
            if (fadeOutCoroutine != null)
            {
                StopCoroutine(fadeOutCoroutine);
            }

            // Start new coroutine to reset accuracy indicators after delay
            fadeOutCoroutine = StartCoroutine(ResetAccuracyIndicatorsAfterDelay());
        }
    }

    public void MissNoTap()
    {
        // Handle case where no tap is detected
        score += 0;
        playerAccuracyIndicators.DisplayAccuracySprite(3); // Display Miss indicator
        Debug.Log("Miss!");
        Debug.Log("Score: " + score);
        
        // Stop existing fade-out coroutine before starting a new one
        if (fadeOutCoroutine != null)
        {
            StopCoroutine(fadeOutCoroutine);
            }
        // Start new coroutine to reset accuracy indicators after delay
        fadeOutCoroutine = StartCoroutine(ResetAccuracyIndicatorsAfterDelay());
    }

    void OnBeat()
    {
        lastTapTime = Time.fixedTime;

        // Start the coroutine to trigger MissNoTap after 2 seconds
        if (missNoTapCoroutine != null)
        {
            StopCoroutine(missNoTapCoroutine);
        }
        missNoTapCoroutine = StartCoroutine(MissNoTapAfterDelay());
    }

    IEnumerator MissNoTapAfterDelay()
    {
        yield return new WaitForSeconds(0.6f);
        missedFish = true;
        MissNoTap();
    }

    bool IsTapOnBeat()
    {
        float timeSinceLastBeat = Time.fixedTime - lastTapTime;
        return timeSinceLastBeat < tapThreshold;
    }

    IEnumerator ResetAccuracyIndicatorsAfterDelay()
    {
        yield return new WaitForSeconds(resetTime);

        foreach (var spriteRenderer in playerAccuracyIndicators.accuracySprites)
        {
            if (spriteRenderer.enabled)
            {
                StartCoroutine(playerAccuracyIndicators.FadeOutSprite(spriteRenderer));
            }
        }
    }
}