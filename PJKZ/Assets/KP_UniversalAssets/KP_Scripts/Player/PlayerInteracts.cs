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

// private bool tapHandled = false;

// void Update()
// {
//     if (Input.GetKeyDown(KeyCode.Mouse0) && SceneManager.GetActiveScene().name == "AquaticMedley" && !tapHandled)
//     {
//         AMTapSound();
//         playerScore.HandleTap();
//         tapHandled = true;
//     }
//     else if (Input.GetKeyUp(KeyCode.Mouse0))
//     {
//         tapHandled = false;
//     }
// }

private bool tapHandled = false;

void Update()
{
    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && SceneManager.GetActiveScene().name == "AquaticMedley" && !tapHandled)
    {
        AMTapSound();
        //playerScore.HandleTap();
        //playerScoreTest.CalculateTapAccuracy();
        tapHandled = true;
    }
    else if (Input.touchCount == 0)
    {
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
