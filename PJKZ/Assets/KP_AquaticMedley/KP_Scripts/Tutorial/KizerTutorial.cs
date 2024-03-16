using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KizerTutorial : MonoBehaviour
{
    public Animator animTut;
    public CheckPointIndicators checkPoints;

    void OnEnabled()
    {
        animTut.SetBool("KizLeave?", false);
    }

    void Update()
    {
        if (checkPoints.correctCount >= 6)
        {
            animTut.SetBool("KizLeave?", false);
        }
    }
}
