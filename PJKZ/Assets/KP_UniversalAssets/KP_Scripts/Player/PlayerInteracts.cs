using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteracts : MonoBehaviour
{
    public AudioSource audioSource;
    public NewPlayerScoreTest playerScoreTest;
    private PlayerScore playerScore;

    void Start()
    {
        playerScore = FindObjectOfType<PlayerScore>();

        //check if AudioSource is attached
        if (audioSource == null)
        {
            Debug.LogError("No AudioSouce.");
        }
    }

    private bool tapHandled = false;

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && SceneManager.GetActiveScene().name == "AquaticMedley" && !tapHandled)
        {
            AMTapSound();
            playerScoreTest.CheckAccuracy();
            tapHandled = true;
        }
        else if (Input.touchCount == 0)
        {
            playerScoreTest.MissAccuracy();
            tapHandled = false;
        }
    }


    void AMTapSound()
    {
        //check if the AudioSource is present
        if (audioSource != null)
        {
            //play the audio clip attached to the AudioSource
            audioSource.Play();
        }
    }
}
