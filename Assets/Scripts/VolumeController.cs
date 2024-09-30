using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeController : MonoBehaviour
{
    #region REFERENCES
    private ProjectInfoPanel projectInfoPanel;
    private ColorAdjustments colorAdjustments;
    private DepthOfField depthOfField;
    private Vignette vignette;
    #endregion

    #region CONFIGURATION
    [SerializeField] private float minFocusDistance = .1f;
    [SerializeField] private float maxFocusDistance = 3f;
    [SerializeField] private float minSaturation = -75f;
    [SerializeField] private float maxSaturation = 1f;
    [SerializeField] private float minVignetteSmoothness = .15f;
    [SerializeField] private float maxVignetteSmoothness = 1f;
    [SerializeField] private float transitionTime = 1f;
    #endregion

    #region STATE
    private bool introCompleted = false;
    #endregion

    #region LIFECYCLE
    private void Awake()
    {
        projectInfoPanel = FindObjectOfType<ProjectInfoPanel>();
        Volume volume = GetComponent<Volume>();
        volume.profile.TryGet<ColorAdjustments>(out colorAdjustments);
        volume.profile.TryGet<DepthOfField>(out depthOfField);
        volume.profile.TryGet<Vignette>(out vignette);
    }

    private void OnEnable()
    {
        projectInfoPanel.OnDisplayingInfo += BlurImage;
    }

    private void OnDisable()
    {
        projectInfoPanel.OnDisplayingInfo -= BlurImage;
    }
    #endregion

    #region METHODS
    public void BlurImage(bool val)
    {
        if (!introCompleted)
        {
            introCompleted = true;
            return;
        }

        StartCoroutine(BlurCoroutine(val));
    }

    private IEnumerator BlurCoroutine(bool val)
    {
        float initialFocusDistance = val ? maxFocusDistance : minFocusDistance;
        float finalFocusDistance = val ? minFocusDistance : maxFocusDistance;
        float initialSaturation = val ? maxSaturation : minSaturation;
        float finalSaturation = val ? minSaturation : maxSaturation;
        float initialVignetteSmoothness = val ? minVignetteSmoothness : maxVignetteSmoothness;
        float finalVignetteSmoothness = val ? maxVignetteSmoothness: minVignetteSmoothness;

        depthOfField.focusDistance.value = initialFocusDistance;
        colorAdjustments.saturation.value = initialSaturation;
        float timer = 0f;
        while (timer < transitionTime)
        {
            float ratio = timer / transitionTime;
            depthOfField.focusDistance.value = Mathf.Lerp(initialFocusDistance, finalFocusDistance, ratio);
            colorAdjustments.saturation.value = Mathf.Lerp(initialSaturation, finalSaturation, ratio);
            vignette.smoothness.value = Mathf.Lerp(initialVignetteSmoothness, finalVignetteSmoothness, ratio);
            timer += Time.deltaTime;
            yield return null;
        }
    }
    #endregion
}