using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject PrevPanel;
    public AudioSource MainMedley;
    public Animator LoadingAnimator;

    public void Panel()
    {
        PausePanel.SetActive(true);
        PrevPanel.SetActive(false);
        AudioListener.pause = true;
        Time.timeScale = 0;
    }

    public void COntinue()
    {
        PausePanel.SetActive(false);
        PrevPanel.SetActive(true);
        AudioListener.pause = false;
        Time.timeScale = 1;
    }

    public void changeScene(string scene)
    {
        AudioListener.pause = false;
        MainMedley.Pause();
        Time.timeScale = 1;
        StartCoroutine(SwitchScene(scene));
    }

    IEnumerator SwitchScene(string sceneName)
    {
        LoadingAnimator.SetBool("LeavingScene?", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
