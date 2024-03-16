using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    public Animator animBox;
    public CheckPointIndicators checkPoints;
    public TextTypeWriter textprompter;


    void OnEnabled()
    {
        animBox.SetBool("DBLeave?", false);
    }

    void Update()
    {
        if (checkPoints.correctCount >= 6)
        {
            textprompter.enabled = true;
            StartCoroutine(AnimWithTImer());
        }
    }

    IEnumerator AnimWithTImer()
    {
        yield return new WaitForSeconds(2.00f);
        animBox.SetBool("DBLeave?", false);
    }
}
