using UnityEngine;
using TMPro;
using System.Collections;

public class MapIndicator : MonoBehaviour
{
    [SerializeField] private string newText;
    [SerializeField] private TMP_Text textObject;
    [SerializeField] private float fadeInSpeed = 0.5f;
    [SerializeField] private float fadeOutSpeed = 0.5f;
    [SerializeField] private float visibilityDuration = 5f;
    [SerializeField] private float fadeDelay = 1f;
    [SerializeField] private float DelayAfterExit = 10f;

    private float fadeTimer;
    private bool isFadingIn;
    private bool isFadingOut;
    private bool isVisible;
    private bool CanVisible;

    void Start()
    {
        if (textObject != null)
        {
            textObject.alpha = 0f; // Set text transparency to 0 (invisible) by default
            textObject.text = "Default"; // Set default text
        }
    }

    void Update()
    {
        if (CanVisible)
        {
            if (isFadingIn)
            {
                FadeTextIn();
            }
            else if (isFadingOut)
            {
                FadeTextOut();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            fadeTimer = 0f;
            isFadingIn = true;
            isVisible = true;
            if (!string.IsNullOrEmpty(newText))
            {
                textObject.text = newText;
                CanVisible = true;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            fadeTimer += Time.deltaTime;
            if (fadeTimer >= visibilityDuration)
            {
                isFadingOut = true;
                isFadingIn = false;
                CanVisible = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            fadeTimer = 0f;
            isFadingOut = true;
            isFadingIn = false;

            CanVisible = false;

            StartCoroutine(Timer());
        }
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(DelayAfterExit);
        CanVisible = true;
    }

    void FadeTextIn()
    {
        textObject.alpha = Mathf.Lerp(textObject.alpha, 1f, fadeInSpeed * Time.deltaTime);
        if (textObject.alpha >= 0.99f)
        {
            isFadingIn = false;
            if (fadeTimer >= fadeDelay)
            {
                isFadingOut = true;
            }
        }
    }

    void FadeTextOut()
    {
        textObject.alpha = Mathf.Lerp(textObject.alpha, 0f, fadeOutSpeed * Time.deltaTime);
        if (textObject.alpha <= 0.01f)
        {
            textObject.alpha = 0f; // Ensure the text is completely invisible
            isFadingOut = false;
            isVisible = false;
        }
    }
}
