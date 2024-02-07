using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public SimpleBeatDetection beatProcessor;
    public float tapThreshold = 0.4f;
    public float resetTime = 0.5f;

    private float lastTapTime;
    public int score;
    public List<bool> displayIndicator = new List<bool>();

    void Start()
    {
        beatProcessor.OnBeat += OnBeat;
        score = 0;

        //for visual indicator
        int typeOfAccuracy = 4;
        InitializeBooleanList(typeOfAccuracy);
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
                SetAccuracyIndicator(0);
                Debug.Log("Perfect!");
            }
            else if (timingDifference < 0.2f)
            {
                score += 100;
                SetAccuracyIndicator(1); 
                Debug.Log("Good!");
            }
            else if (timingDifference < 0.3f)
            {
                score += 50;
                SetAccuracyIndicator(2);
                Debug.Log("Bad!");
            }
            else
            {
                score += 0;
                SetAccuracyIndicator(3);
                Debug.Log("Miss!");
            }

            Debug.Log("Score: " + score);
            StartCoroutine(ResetAccuracyIndicatorsAfterDelay());
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

    void InitializeBooleanList(int typeOfAccuracy)
    {
        for (int i = 0; i < typeOfAccuracy; i++)
        {
            displayIndicator.Add(false);
        }
    }

    void ResetAccuracyIndicators()
    {
        for (int i = 0; i < displayIndicator.Count; i++)
        {
            displayIndicator[i] = false;
        }
    }

    void SetAccuracyIndicator(int index)
    {
        if (index >= 0 && index < displayIndicator.Count)
        {
            displayIndicator[index] = true;
        }
    }

    IEnumerator ResetAccuracyIndicatorsAfterDelay()
    {
        yield return new WaitForSeconds(resetTime);

        // Reset accuracy indicators after the specified delay
        ResetAccuracyIndicators();
    }
}