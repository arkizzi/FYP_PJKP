using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerScoreTest : MonoBehaviour
{
   public BeatPreCalculator beatPreCalculator; 
   public PlayerAccuracyIndicators playerAccuracyIndicators;
    public float timeWindow = 0.5f; // Time window in seconds around each beat timestamp
    public float maxTimeDifference = 0.2f; // Maximum time difference to consider a tap within the window
    public PlayerInteracts playerInteracts;
    public int score;
    public bool missedFish = false;
    public bool successFish = false;
    private bool playerTapped = false;
    public float resetTime = 0.5f;
    HashSet<float> handledTimestamps = new HashSet<float>();
    private Coroutine fadeOutCoroutine;
    private List<float> predefinedBeatTimestamps = new List<float> { 0.00f, 13.931f, 16.073f, 17.360f, 21.002f, 21.217f, 23.144f, 23.359f, 
                                                                    24.217f, 27.646f, 30.003f, 30.219f, 31.073f, 34.503f, 36.860f, 
                                                                    37.075f, 37.932f, 41.576f, 41.788f, 43.716f, 43.933f, 44.788f, 
                                                                    48.431f, 48.647f, 50.574f, 50.791f, 51.646f};

    public void Start()
    {
        score = 0;
    }

    //change the way the accuracy is determined, its bad atm

    public void CheckAccuracy()
    {
        missedFish = false;
        successFish = false;
        playerTapped = true;

        // Get the current time in the game
        float currentTime = Time.time;
        // Debug.Log(currentTime);

        // Iterate through the list of beat timestamps
        foreach (float beatTime in predefinedBeatTimestamps)
        {
            // Calculate the start and end of the time window around the beat timestamp
            float windowStart = beatTime;
            float windowEnd = beatTime + timeWindow;

            // Check if the current time falls within the time window
            if (currentTime >= windowStart && currentTime <= windowEnd)
            {
                // Calculate the timing difference
                float timingDifference = Mathf.Abs(beatTime - currentTime);
                Debug.Log(timingDifference);

                // Check if the timing difference is within the maximum allowed difference
                if (timingDifference <= maxTimeDifference)
                {
                    // Timing is accurate, handle accuracy level
                    HandleAccuracy(timingDifference);
                    return; // Exit the loop as we found a match
                }
            }
        }
    }

public void MissAccuracy()
{
    missedFish = false;
    successFish = false;
    // Get the current time in the game
    float currentTime = Time.time;
    //Debug.Log(currentTime);

    // Iterate through the list of beat timestamps
    foreach (float beatTime in predefinedBeatTimestamps)
    {
        if (!playerTapped)
        {
            // Calculate the start and end of the time window around the beat timestamp
            float windowStart = beatTime - timeWindow;
            float windowEnd = beatTime + timeWindow;
            //Debug.Log(windowEnd);

            // Check if the current time falls within the time window and the beat timestamp has not been handled yet
            if (!(currentTime <= windowEnd) && !handledTimestamps.Contains(beatTime))
            {
                HandleMissedTap();
                handledTimestamps.Add(beatTime); // Add the timestamp to the set to indicate it has been handled
            }
        }
    }
}



    void HandleAccuracy(float timingDifference)
    {
        // Evaluate the accuracy based on the timing difference
        if (timingDifference <= 0.2f)
        {
            score += 500;
            playerAccuracyIndicators.DisplayAccuracySprite(0);
            successFish = true;
            Debug.Log("Perfect!");
        }
        else if (timingDifference <= 0.6f)
        {
            score += 100;
            playerAccuracyIndicators.DisplayAccuracySprite(1);
            successFish = true;
            Debug.Log("Good!");
        }
        else if (timingDifference <= 0.8f)
        {
            score += 50;
            playerAccuracyIndicators.DisplayAccuracySprite(2);
            successFish = true;
            Debug.Log("Bad!");
        }
        else
        {
            score += 0;
            playerAccuracyIndicators.DisplayAccuracySprite(3);
            missedFish = true;
            Debug.Log("Miss!");
        }

        // Stop existing fade-out coroutine before starting a new one
        if (fadeOutCoroutine != null)
        {
            StopCoroutine(fadeOutCoroutine);
        }

        // Start new coroutine to reset accuracy indicators after delay
        fadeOutCoroutine = StartCoroutine(ResetAccuracyIndicatorsAfterDelay());
        
        //playerTapped = false;
    }

    void HandleMissedTap()
    {
        score += 0;
        playerAccuracyIndicators.DisplayAccuracySprite(3);
        missedFish = true;
        Debug.Log("Missed Tap!");

        // Stop existing fade-out coroutine before starting a new one
        if (fadeOutCoroutine != null)
        {
            StopCoroutine(fadeOutCoroutine);
        }

        // Start new coroutine to reset accuracy indicators after delay
        fadeOutCoroutine = StartCoroutine(ResetAccuracyIndicatorsAfterDelay());
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
