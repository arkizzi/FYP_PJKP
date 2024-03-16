using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkerBGTut : MonoBehaviour
{
    public Animator dbgTut;
    public CheckPointIndicators checkPoints;

    void OnEnabled()
    {
        dbgTut.SetBool("LeaveDarkBG?", false);
    }

    void Update()
    {
        if (checkPoints.correctCount >= 6)
        {
            StartCoroutine(AnimWithTImer());
        }
    }

    IEnumerator AnimWithTImer()
    {
        yield return new WaitForSeconds(2.00f);
        dbgTut.SetBool("LeaveDarkBG?", false);
    }
}
