using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkerBGTut : MonoBehaviour
{
    public Animator dbgTut;
    void OnEnabled()
    {
        dbgTut.SetBool("LeaveDarkBG?", false);
    }
}
