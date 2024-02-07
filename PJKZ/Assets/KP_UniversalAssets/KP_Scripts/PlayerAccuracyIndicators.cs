using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAccuracyIndicators : MonoBehaviour
{
    public List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    public PlayerScore playerScore;

    //pretty effects
    public float fadeSpeed = 0.5f;
    private List<float> currentAlphas;
    private List<float> targetAlphas;

    void Start()
    {
        ResetVisibilityAll();
        InitializeAlphas();
    }

    void Update()
    {
        DisplaySprites();
    }

    void DisplaySprites()
    {
        if (playerScore != null)
        {
            List<bool> displayIndicator = playerScore.displayIndicator;

            for (int i = 0; i < spriteRenderers.Count; i++)
            {
                if (i < displayIndicator.Count && i < currentAlphas.Count && i < targetAlphas.Count && spriteRenderers[i] != null)
                {
                    targetAlphas[i] = displayIndicator[i] ? 1.0f : 0.0f;

                    if (displayIndicator[i])
                    {
                        currentAlphas[i] = Mathf.MoveTowards(currentAlphas[i], targetAlphas[i], fadeSpeed * Time.deltaTime);
                    }
                    else
                    {
                        currentAlphas[i] = Mathf.MoveTowards(currentAlphas[i], targetAlphas[i], fadeSpeed * Time.deltaTime);
                    }

                    Color spriteColour = spriteRenderers[i].color;
                    spriteColour.a = currentAlphas[i];
                    spriteRenderers[i].color = spriteColour;
                }
            }
        }
    }

    void ResetVisibilityAll()
    {
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            if (spriteRenderer != null)
            {
                Color spriteColour = spriteRenderer.color;
                spriteColour.a = 0.0f;
                spriteRenderer.color = spriteColour;
            }
        }
    }

    void InitializeAlphas()
    {
        currentAlphas = new List<float>();
        targetAlphas = new List<float>();

        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            currentAlphas.Add(0.0f);
            targetAlphas.Add(0.0f);
        }
    }

}
