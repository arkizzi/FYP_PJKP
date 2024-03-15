using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTutorial : MonoBehaviour
{
    public SimpleBeatDetection BeatDetector;
    public TextTypeWriter textprompter;
    public KizerTutorial kiz;
    public DialogueBox db;
    public DarkerBGTut dbg;
    public AudioSource PenkieChirps;
    public AudioSource PopBeats;
    private int markerCounter = 0;
    private bool beatDetect = false;
    private float lastBeatTime = 0f; 
    private bool chirpsAndBeatsRunning = false; 

    void Start()
    {
        BeatDetector.OnBeat += OnBeat;
    }

    void OnBeat()
    {
        float currentTime = Time.time;
        if (currentTime - lastBeatTime > 3.5f) 
        {
            markerCounter++;
            lastBeatTime = currentTime; 
            if (!chirpsAndBeatsRunning && markerCounter >= 2) 
            {
                StartCoroutine(ChirpsAndBeats());
            }
        }
    }

    void Update()
    {
        Debug.Log(chirpsAndBeatsRunning);
        if (textprompter.textDisplayed)
        {
            kiz.animTut.SetBool("KizLeave?", true);
            db.animBox.SetBool("DBLeave?", true);
            dbg.dbgTut.SetBool("LeaveDarkBG?", true);
        }
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
        yield return new WaitForSeconds(1.00f);
        PenkieChirps.enabled = false;
        PopBeats.enabled = false;
    }
}
