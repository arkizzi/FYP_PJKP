using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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
    public CheckPointIndicators checkPoints;
    
    private int markerCounter = 1;
    private float lastBeatTime = 0f; 
    private bool chirpsAndBeatsRunning = false; 

    void Start()
    {
        BeatDetector.OnBeat += OnBeat;
    }

    void OnBeat()
    {
        float currentTime = Time.time;
        if (currentTime - lastBeatTime > 3f) 
        {
            markerCounter++;
            lastBeatTime = currentTime; 
            
            if (!chirpsAndBeatsRunning && markerCounter >= 3 && checkPoints.correctCount < 6 && kiz.animTut.GetBool("KizLeave?")) 
            {
                StartCoroutine(ChirpsAndBeats());
            }
        }
    }

    void Update()
    {
        //Debug.Log(markerCounter);
        if (textprompter.textDisplayed && textprompter.LineNoChecker == 1)
        {
            kiz.animTut.SetBool("KizLeave?", true);
            db.animBox.SetBool("DBLeave?", true);
            dbg.dbgTut.SetBool("LeaveDarkBG?", true);
            textprompter.textDisplayed = false;
            textprompter.enabled = false;
        }

        if (textprompter.LineNoChecker == 2)
        {
            StartCoroutine(SwitchScene("AquaticMedley"));
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

    IEnumerator SwitchScene(string sceneName)
    {
        yield return new WaitForSeconds(4.0f);
        SceneManager.LoadScene(sceneName);
    }

}
