using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeatCue : MonoBehaviour
{
    public SimpleBeatDetection BeatDetector;
    public ThoughtBox dialogueBox;
    public GameObject dilogObj;
    public AudioSource Markers;
    public AudioSource PenkieChirps;
    public AudioSource PopBeats;
    public GameObject ZapBadFish;
    public GameObject ZapGoodFish;
    public GameObject PopFish;
    public Animator angryPenkie;
    public Animator LoadingAnimator;
    
    public int markerCounter = 0;
    public bool isntAngry = false;
    private float lastBeatTime = 0f; 
    private bool chirpsAndBeatsRunning = false; 
    private int RoundsPerSes;

    void Start()
    {
        dilogObj.SetActive(false);
        Markers.Play();
        BeatDetector.OnBeat += OnBeat;
    }

    void Update()
    {
        Debug.Log(markerCounter);
    }

    void OnBeat()
    {
        float currentTime = Time.time;
        if (currentTime - lastBeatTime > 3f) 
        {
            markerCounter++;
            lastBeatTime = currentTime; 
            dialogueBox.LineChanger();
            RemoveOrAddFish();
            ThoughBoxProcesses();

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
                dilogObj.SetActive(false);
                break;
            case 8:
                ZapBadFish.SetActive(false);
                ZapGoodFish.SetActive(true);
                //dilogObj.SetActive(false);
                break;
            case 10:
                dilogObj.SetActive(false);
                break;
            case 12:
                ZapGoodFish.SetActive(false);
                PopFish.SetActive(true);
                break;
            case 14:
                dilogObj.SetActive(false);
                break;
        }
    }

    public void ThoughBoxProcesses()
    {
        switch (markerCounter)
        {
            case 1:
                dilogObj.SetActive(true);
                break;
            case 7:
                dilogObj.SetActive(true);
                break;
            case 11:
                dilogObj.SetActive(true);
                angryPenkie.SetBool("IsNotAngry", true);
                isntAngry = true;
                break;
            case 15:
                StartCoroutine(SwitchToTitle("TitleScreen"));
                break;
        }
    }

    IEnumerator SwitchToTitle(string sceneName)
    {
        yield return new WaitForSeconds(5f);
        LoadingAnimator.SetBool("LeavingScene?", true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator ChirpsAndBeats()
    {
        chirpsAndBeatsRunning = true;
        Chirps();
        StartCoroutine(PopTimer());
        StartCoroutine(disableAudio());
        yield return new WaitForSeconds(1.84f);
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
        yield return new WaitForSeconds(3.00f);
        PenkieChirps.enabled = false;
        PopBeats.enabled = false;
    }
}
