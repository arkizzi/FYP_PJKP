using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class TapIndicatorVisual : MonoBehaviour
{
    public PlayerInteracts beats;
    public Image image;
    public Animator indiTut;
    void Update()
    {
        if (beats.startTime)
        {
            if (!image.enabled)
            {
                image.enabled = true;
            }
            StartCoroutine(DisableAfterAWhile());
        }
    }

    void Start()
    {
        image.enabled = false;
    }

    IEnumerator DisableAfterAWhile()
    {
        indiTut.SetBool("LeaveBG?", false);
        yield return new WaitForSeconds(0.375f);
        indiTut.SetBool("LeaveBG?", true);
    }
}
