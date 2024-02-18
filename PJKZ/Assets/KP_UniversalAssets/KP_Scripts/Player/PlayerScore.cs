using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public SimpleBeatDetection beatProcessor;
    public PlayerAccuracyIndicators playerAccuracyIndicators;
    public float tapThreshold = 0.4f;
    public float resetTime = 0.5f;

    private float lastTapTime;
    private Coroutine fadeOutCoroutine;
    public int score;

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
            float timingDifference = Mathf.Abs(Time.time - lastTapTime);

            if (timingDifference < 0.1f)
            {
                score += 500;
                playerAccuracyIndicators.DisplayAccuracySprite(0);
                Debug.Log("Perfect!");
            }
            else if (timingDifference < 0.2f)
            {
                score += 100;
                playerAccuracyIndicators.DisplayAccuracySprite(1);
                Debug.Log("Good!");
            }
            else if (timingDifference < 0.3f)
            {
                score += 50;
                playerAccuracyIndicators.DisplayAccuracySprite(2);
                Debug.Log("Bad!");
            }
            else
            {
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

    void OnBeat()
    {
        lastTapTime = Time.fixedTime;
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