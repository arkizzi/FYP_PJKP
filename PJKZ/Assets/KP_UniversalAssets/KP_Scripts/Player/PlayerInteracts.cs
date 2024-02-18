using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteracts : MonoBehaviour
{
    public AudioSource audioSource;
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

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && SceneManager.GetActiveScene().name == "AquaticMedley")
        {
            //handle initial press
            AMTapSound();
        }

        if (Input.GetMouseButtonUp(0) && SceneManager.GetActiveScene().name == "AquaticMedley")
        {
            //handle release
            playerScore.HandleTap();
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
