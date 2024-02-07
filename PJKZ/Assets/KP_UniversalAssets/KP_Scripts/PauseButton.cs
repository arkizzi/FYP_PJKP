using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PauseButton : MonoBehaviour
{
    public UnityEvent gamePaused;
    public UnityEvent gameResumed;
    private bool isGamePaused = false;

    void Start()
    {
        Button pauseButton = GameObject.Find("PauseButton").GetComponent<Button>();

        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(TogglePause);
        }
        else
        {
            Debug.LogError("PauseButton not found or not assigned in the Inspector.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;

        AudioSource[] audios = FindObjectsOfType<AudioSource>();

        if (isGamePaused)
        {
            // Pause the audio before setting Time.timeScale to 0
            foreach (AudioSource a in audios)
            {
                a.Pause();
            }

            Time.timeScale = 0f;
            gamePaused.Invoke();
            // Dump the pause menu here
        }
        else
        {
            // Unpause the audio before setting Time.timeScale to 1
            foreach (AudioSource a in audios)
            {
                a.UnPause();
            }

            Time.timeScale = 1f;
            gameResumed.Invoke();
            // Remove/hide the pause menu here
        }
    }

}
