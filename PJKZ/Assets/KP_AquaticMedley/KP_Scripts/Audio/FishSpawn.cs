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
    private Coroutine fadeOutCoroutine;

    void Start()
    {
        DisableFishSprite(); // Disable sprite on start
    }

void Update()
{
    if (penkieChirpIndication.GoodFishSpawn == true)
    {
        fishSprite.enabled = true;
        if (fadeOutCoroutine == null)
        {
            scaleCoroutine = StartCoroutine(AnimateFishIn(fishSprite));
        }
    }
    if  (playerScore.missedFish == true)
    {
        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
        }
        fadeOutCoroutine = StartCoroutine(FadeOutSprite(fishSprite));
    }
}


    public void DisableFishSprite()
    {
        fishSprite.enabled = false;
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
        Vector3 originalScale = spriteRenderer.transform.localScale;
        Vector3 targetScale = originalScale * growScale;

        float growElapsedTime = 0f;
        while (growElapsedTime < animationDuration)
        {
            spriteRenderer.transform.localScale = Vector3.Lerp(originalScale, targetScale, growElapsedTime / animationDuration);
            growElapsedTime += Time.deltaTime;
            yield return null;
        }

        float fadeElapsedTime = 0f;
        while (fadeElapsedTime < fadeOutDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, fadeElapsedTime / fadeOutDuration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            fadeElapsedTime += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = originalColor; // Reset to original color
        spriteRenderer.enabled = false; // Disable the sprite after fading out
    }

}
