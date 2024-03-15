using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    public Animator animBox;
    void OnEnabled()
    {
        animBox.SetBool("DBLeave?", false);
    }
}
