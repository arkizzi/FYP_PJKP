using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThoughtBox : MonoBehaviour
{
    private bool displayedTextFin = false;
    public Text text;
    public BeatCue bq;
    private string Lines;
    private int LineNoChecker = 0;
    public Animator animator;
    public bool disabler = false;

    void Start()
    {
        //LineNoChecker = 1;
    }

    void OnEnable()
    {
        //LineNoChecker ++;
        LineChanger();
        StartCoroutine(AddIntoScene());
    }

    void Update()
    {
        //Debug.Log(LineNoChecker);
        //LineChanger();
    }

    public void LineChanger()
    {
        switch (bq.markerCounter)
        {
            case 1:
                Lines = "Penkie is ANGRY! Penkie is going to HURT ALL fish!";
                StartCoroutine(TypeWriterProcess(Lines));
                break;
            case 2:
                Lines = "Penkie HATES ALL fish! Penkie HATES EVERYTHING!!";
                StartCoroutine(TypeWriterProcess(Lines));
                StartCoroutine(RemoveFromScene());
                break;
            case 7:
                Lines = "WHY do I hurt them when I'm angry?";
                StartCoroutine(TypeWriterProcess(Lines));
                break;
            case 8:
                Lines = "Is it WRONG for me to hurt them?";
                StartCoroutine(TypeWriterProcess(Lines));
                StartCoroutine(RemoveFromScene());
                break;
            case 11:
                Lines = "Penkie shouldn't hurt the Fish...";
                StartCoroutine(TypeWriterProcess(Lines));
                break;
            case 12:
                Lines = "Penkie will BE NICE to them now!";
                StartCoroutine(TypeWriterProcess(Lines));
                StartCoroutine(RemoveFromScene());
                break;
        }
    }

    IEnumerator TypeWriterProcess(string scent)
    {
        text.text = ""; //null

        foreach (char letter in scent.ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(0.04f);
        }
        yield return new WaitForSeconds(1f);
        displayedTextFin = true;
    }

        IEnumerator AddIntoScene()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("IsEntering?", true);
    }

    IEnumerator RemoveFromScene()
    {
        if (bq.markerCounter == 2)
        {
            yield return new WaitForSeconds(5f);
        }
        else
        {
            yield return new WaitForSeconds(3f);
        }
        animator.SetBool("IsLeaving?", true);
        disabler = true;
    }
}
