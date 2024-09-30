using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreen : MonoBehaviour
{
    #region REFERENCES
    private Image blackScreen;
    #endregion

    #region CONFIGURATION
    [SerializeField] private float fadeTime = 4f;
    #endregion

    #region LIFECYCLE
    private void Awake()
    {
        blackScreen = GetComponent<Image>();
    }


    private void Start()
    {
        FadeIn();
    }
    #endregion

    #region METHODS
    private void FadeIn()
    {
        StartCoroutine(FadeCoroutine(true, fadeTime));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeCoroutine(false, fadeTime));
    }

    private IEnumerator FadeCoroutine(bool isFadeIn, float fadingTime)
    {
        float initialAlphaValue = isFadeIn ? 1 : 0;
        float finalAlphaValue = isFadeIn ? 0 : 1;
        float timer = 0;

        while (timer < fadingTime)
        {
            float ratio = timer / fadingTime;
            Color tempColor = blackScreen.color;
            tempColor.a = Mathf.Lerp(initialAlphaValue, finalAlphaValue, ratio);
            blackScreen.color = tempColor;

            timer += Time.deltaTime;
            yield return null;
        }
    }
    #endregion
}