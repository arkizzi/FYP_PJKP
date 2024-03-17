using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThoughtBox : MonoBehaviour
{
    private bool displayedTextFin = false;
    public Text text;
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
        animator.SetBool("IsLeaving?", false);
        disabler = false;
        //LineNoChecker ++;
        LineChanger();
    }

    void Update()
    {
        Debug.Log(LineNoChecker);
        if (displayedTextFin)
        {
            LineNoChecker ++;
            displayedTextFin = false;
            LineChanger();
        }
    }

    void LineChanger()
    {
        switch (LineNoChecker)
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
            case 3:
                Lines = "WHY do i hurt the fish? Did they hurt me?";
                StartCoroutine(TypeWriterProcess(Lines));
                break;
            case 4:
                Lines = "Is it BAD to hurt the fish when I'm angry?";
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
        yield return new WaitForSeconds(5.0f);
        displayedTextFin = true;
    }

    IEnumerator RemoveFromScene()
    {
        yield return new WaitForSeconds(5f);
        animator.SetBool("IsLeaving?", true);
        disabler = true;
    }
}
