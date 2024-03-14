using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptCredits : MonoBehaviour
{
    public Canvas currentCanvas; // First canvas object
    public Canvas nextCanvas; // Second canvas object

    // Function to toggle the enabled state of the canvases
    public void ToggleCanvasesOnClick()
    {
        // Toggle the state of the canvases
        currentCanvas.enabled = false;
        nextCanvas.enabled = true;
    }
}
