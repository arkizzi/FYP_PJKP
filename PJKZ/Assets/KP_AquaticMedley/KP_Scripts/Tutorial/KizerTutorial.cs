using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KizerTutorial : MonoBehaviour
{
    public Animator animTut;
    void OnEnabled()
    {
        animTut.SetBool("KizLeave?", false);
    }
}
