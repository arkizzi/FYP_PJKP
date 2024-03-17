using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitching : MonoBehaviour
{
    public Animator LoadingAnimator;

    public void changeScene(string scene)
    {
        StartCoroutine(SwitchScene(scene));
    }

    IEnumerator SwitchScene(string sceneName)
    {
        LoadingAnimator.SetBool("LeavingScene?", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
