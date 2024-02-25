using System.Collections;
using UnityEngine;

public class FishSpawn : MonoBehaviour
{
    public SpriteRenderer fishSprite;
    public PlayerScore playerScore;
    public PenkieChirpIndication penkieChirpIndication;
    public float growScale = 1.1f;
    public float animationDuration = 0.1f;
    public float fadeOutDuration = 0.15f;
    private Coroutine scaleCoroutine;

    void Start()
    {
        DisableFishSprite(); // Disable sprite on start
    }

    public void DisableFishSprite()
    {
        fishSprite.enabled = false;
    }

    public void DisplayFish()
    {
        DisableFishSprite(); // First, disable all sprites

        // Stop existing scaling coroutine before starting a new one
        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
        }

        fishSprite.enabled = true;
        StartCoroutine(AnimateFishIn(fishSprite));
    }

    public IEnumerator AnimateFishIn(SpriteRenderer spriteRenderer)
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
        playerScore.missedFish = false;
    }

    public IEnumerator GrowAndFadeOutSprite(SpriteRenderer spriteRenderer)
    {
        Color originalColor = spriteRenderer.color;
        Vector3 ogScale = spriteRenderer.transform.localScale;
        Vector3 targetScale = ogScale * growScale;

        float growElapsedTime = 0f;
        float fadeElapsedTime = 0f; // Added variable for fade animation

        while (growElapsedTime < animationDuration || fadeElapsedTime < fadeOutDuration)
        {
            // Calculate grow factor
            float growFactor = Mathf.Min(1f, growElapsedTime / animationDuration);

            // Update scale
            spriteRenderer.transform.localScale = Vector3.Lerp(ogScale, targetScale, growFactor);

            // Update fade
            float alpha = Mathf.Lerp(1f, 0f, fadeElapsedTime / fadeOutDuration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            // Increment timers
            growElapsedTime += Time.deltaTime;
            fadeElapsedTime += Time.deltaTime;

            yield return null;
        }

        spriteRenderer.color = originalColor; // Reset to original color
        spriteRenderer.transform.localScale = ogScale/growScale; // Reset to original scale
        spriteRenderer.enabled = false; // Disable the sprite after fading out
        playerScore.successFish = false;
    }


}
