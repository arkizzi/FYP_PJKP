using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAccuracyIndicators : MonoBehaviour
{
    public List<SpriteRenderer> accuracySprites;
    public float growScale = 1.1f;
    public float animationDuration = 0.15f;
    public float fadeOutDuration = 0.15f;
    private Coroutine scaleCoroutine;

    void Start()
    {
        //disable all sprites
        DisableAllSprites();
    }

    //method that disables all sprites
    public void DisableAllSprites()
    {
        foreach (var spriteRenderer in accuracySprites)
        {
            spriteRenderer.enabled = false;
        }
    }

    //activate the corresponding sprite based on the accuracy
    public void DisplayAccuracySprite(int accuracyIndex)
    {
        DisableAllSprites(); // First, disable all sprites

        //activate sprite based on the accuracy index
        if (accuracyIndex >= 0 && accuracyIndex < accuracySprites.Count)
        {
            // Stop existing scaling coroutine before starting a new one
            if (scaleCoroutine != null)
            {
                StopCoroutine(scaleCoroutine);
            }

            accuracySprites[accuracyIndex].enabled = true;
            StartCoroutine(AnimateAccuracySprite(accuracySprites[accuracyIndex]));
        }
        else
        {
            Debug.LogError("Invalid accuracy index.");
        }
    }

    IEnumerator AnimateAccuracySprite(SpriteRenderer spriteRenderer)
    {
        Vector3 originalScale = spriteRenderer.transform.localScale;
        Vector3 targetScale = originalScale * growScale;

        float elapsedTime = 0f;
        while (elapsedTime < animationDuration)
        {
            spriteRenderer.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.transform.localScale = originalScale; // Reset to original scale
    }

    public IEnumerator FadeOutSprite(SpriteRenderer spriteRenderer)
    {
        Color originalColor = spriteRenderer.color;

        float elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = originalColor; // Reset to original color
        spriteRenderer.enabled = false; // Disable the sprite after fading out
    }
}
