using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteracts : MonoBehaviour
{
    public LayerMask targetLayer;
    public AudioSource popAudio;
    public AudioSource zapAudio;
    public AudioSource missAudio;
    public SimpleBeatDetection BeatDetector;
    public PlayerAccuracyIndicators accSpritePrompter;
    public bool successTap = false;
    public bool failTap = false;
    public bool tutCheck = false;
    public int fishSpawnCount = 0;

    private float time_after_beat = 0;
    public bool startTime = false;
    private float max_time_before_fail = 0.5f; //set this to how much longer after a beat should it be considered a fail
    private float max_time_before_great = 0.25f; //anytime between this and fail would be considered "Okay"
    private float max_time_before_best = 0.15f; // any button press before this is a "Perfect" anytime after this is "Great"
    private Coroutine fadeOutCoroutine;

    void Start()
    {
        BeatDetector.OnBeat += OnBeat; 
    }

    void Update()
    {
        if (fishSpawnCount >= 2) //trigger to check if it's the 2nd fish's turn
        {
            fishSpawnCount = 0;
        }

        if (startTime)
        {
            time_after_beat += Time.deltaTime;

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began|| Input.GetMouseButtonDown(0)) 
            {
                if (time_after_beat < max_time_before_best)
                {
                    PopTapSound();
                    accSpritePrompter.DisplayAccuracySprite(0);
                    Debug.Log("Perfect!");
                    fishSpawnCount = fishSpawnCount + 1;
                    successTap = true;
                    tutCheck = true;
                    startTime = false;
                }
                else if (time_after_beat < max_time_before_great)
                {
                    PopTapSound();
                    accSpritePrompter.DisplayAccuracySprite(1);
                    Debug.Log("Great!");
                    fishSpawnCount = fishSpawnCount + 1;
                    successTap = true;
                    tutCheck = true;
                    startTime = false;
                }
                else if (time_after_beat < max_time_before_fail)
                {
                    PopTapSound();
                    accSpritePrompter.DisplayAccuracySprite(2);
                    Debug.Log("Bad!");
                    fishSpawnCount = fishSpawnCount + 1;
                    successTap = true;
                    tutCheck = true;
                    startTime = false;
                }
                else
                {
                    MissTapSound();
                    accSpritePrompter.DisplayAccuracySprite(3);
                    Debug.Log("Miss");
                    fishSpawnCount = fishSpawnCount + 1;
                    //successTap = false;
                    startTime = false;
                }

                fadeOutCoroutine = StartCoroutine(ResetAccuracyIndicatorsAfterDelay());
            }
            else if (time_after_beat > 0.45f)
            {
                MissTapSound();
                accSpritePrompter.DisplayAccuracySprite(3);
                Debug.Log("Miss");
                fishSpawnCount = fishSpawnCount + 1;
                failTap = true;
                startTime = false;
                fadeOutCoroutine = StartCoroutine(ResetAccuracyIndicatorsAfterDelay());
            }
        }
    }

    void OnBeat()
    {
        time_after_beat = 0;
        startTime = true;
    }

    void PopTapSound()
    {
        if (IsLayerActiveInHierarchy(targetLayer))
        {
            zapAudio.Play();
        }
        else
        {
            popAudio.Play();
        }
    }

    bool IsLayerActiveInHierarchy(LayerMask layerMask)
    {
        // Get all GameObjects in the scene
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (((1 << obj.layer) & layerMask) != 0)
            {
                return true;
            }
        }
        return false;
    }

    void MissTapSound()
    {
        if (missAudio != null)
        {
            missAudio.Play();
        }
    }


    IEnumerator ResetAccuracyIndicatorsAfterDelay()
    {
        yield return new WaitForSeconds(1f);

        foreach (var spriteRenderer in accSpritePrompter.accuracySprites)
        {
            if (spriteRenderer.enabled)
            {
                StartCoroutine(accSpritePrompter.FadeOutSprite(spriteRenderer));
            }
        }
    }
}
