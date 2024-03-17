using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatCue : MonoBehaviour
{
    public SimpleBeatDetection BeatDetector;
    public ThoughtBox dialogueBox;
    public AudioSource Markers;
    public AudioSource PenkieChirps;
    public AudioSource PopBeats;
    public GameObject ZapBadFish;
    public GameObject ZapGoodFish;
    public GameObject PopFish;
    
    private int markerCounter = 0;
    private float lastBeatTime = 0f; 
    private bool chirpsAndBeatsRunning = false; 
    private int RoundsPerSes;

    void Start()
    {
        Markers.Play();
        BeatDetector.OnBeat += OnBeat;
    }

    void Update()
    {
        Debug.Log(markerCounter);
        // Debug.Log(RoundsPerSes);
        // if (RoundsPerSes == 2)
        // {
        //     RoundsPerSes = 0;
        //     StartCoroutine(BringBackDialogueBox());
        // }
    }

    void OnBeat()
    {
        float currentTime = Time.time;
        if (currentTime - lastBeatTime > 3f) 
        {
            dialogueBox.gameObject.SetActive(false);
            markerCounter++;
            lastBeatTime = currentTime; 
            RemoveOrAddFish();

            if (markerCounter == 5 || markerCounter == 6 || markerCounter == 9 || markerCounter == 10 || markerCounter == 13 || markerCounter == 14)
            {
                StartCoroutine(ChirpsAndBeats());
            } 
        }
    }

    public void RemoveOrAddFish()
    {
        switch (markerCounter)
        {
            case 4:
                ZapBadFish.SetActive(true);
                break;
            case 8:
                //Destroy(ZapBadFish);
                ZapBadFish.SetActive(false);
                ZapGoodFish.SetActive(true);
                break;
            case 12:
                //Destroy(ZapGoodFish);
                ZapGoodFish.SetActive(false);
                PopFish.SetActive(true);
                break;
        }
    }

    IEnumerator BringBackDialogueBox()
    {
        yield return new WaitForSeconds(2f);
        dialogueBox.gameObject.SetActive(true);
    }

    IEnumerator ChirpsAndBeats()
    {
        chirpsAndBeatsRunning = true;
        Chirps();
        StartCoroutine(PopTimer());
        StartCoroutine(disableAudio());
        yield return new WaitForSeconds(1.84f);
        RoundsPerSes ++;
        chirpsAndBeatsRunning = false; 
    }

    void Chirps()
    {
        PenkieChirps.enabled = true;
    }

    IEnumerator PopTimer()
    {
        yield return new WaitForSeconds(1.84f);
        PopBeats.enabled = true;
    }

    IEnumerator disableAudio()
    {
        yield return new WaitForSeconds(1.00f);
        PenkieChirps.enabled = false;
        PopBeats.enabled = false;
    }
}
