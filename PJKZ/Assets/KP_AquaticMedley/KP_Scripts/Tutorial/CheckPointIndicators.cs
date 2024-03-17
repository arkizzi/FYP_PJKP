using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointIndicators : MonoBehaviour
{
    public int correctCount;
    public PlayerInteracts pi;
    public List<Animator> animatorsList = new List<Animator>();

    void Update()
    {
        //Debug.Log("correctCount: " + correctCount);
        
        if (pi.tutCheck)
        {

            pi.tutCheck = false; 
            correctCount++;

            if (correctCount == 2)
            {
                animatorsList[0].SetBool("Success?", true);
            }
            else if (correctCount == 4)
            {
                animatorsList[1].SetBool("Success?", true);
            }
            else if (correctCount == 6)
            {
                animatorsList[2].SetBool("Success?", true);
            }
        }
    }

}
