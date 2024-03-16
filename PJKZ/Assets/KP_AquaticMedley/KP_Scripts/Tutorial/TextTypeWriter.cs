using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTypeWriter : MonoBehaviour
{
    public bool textDisplayed;
    public Text text;
    public CheckPointIndicators checkPoints;
    private string Lines;
    public int LineNoChecker;

    void OnEnable()
    {
        LineChanger();
        StartCoroutine(TypeWriterProcess(Lines));
    }

    void LineChanger()
    {
        Lines = "Tap on beat AFTER you hear Penkie's chirps!";
        LineNoChecker = 1;

        if (checkPoints.correctCount >= 6)
        {
            Lines = "Good Job!";
            LineNoChecker = 2;
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
        yield return new WaitForSeconds(1.0f);
        textDisplayed = true;
    }
}
