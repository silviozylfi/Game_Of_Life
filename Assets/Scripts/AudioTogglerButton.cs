using System.Collections;
using UnityEngine;

public class AudioTogglerButton : SZ_Button
{
    #region CONFIGURATION
    [SerializeField] private AudioSource[] sources;
    [SerializeField] private Sprite onSprite;
    [SerializeField] private Sprite offSprite;
    [SerializeField] private float transitionTime = 1f;
    #endregion

    #region REFERENCES
    private ProjectInfoPanel projectInfoPanel;
    #endregion

    #region STATE
    private bool isAudioOn;
    private bool isTransitioning;
    #endregion

    #region LIFECYCLE
    protected override void Awake()
    {
        base.Awake();
        projectInfoPanel = FindObjectOfType<ProjectInfoPanel>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        projectInfoPanel.OnDisplayingInfo += EvaluateShowingLogic;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        projectInfoPanel.OnDisplayingInfo -= EvaluateShowingLogic;
    }

    protected override void Start()
    {
        base.Start();
        isAudioOn = true;
        isTransitioning = false;
    }
    #endregion

    #region METHODS
    protected override void ClickButton()
    {
        base.ClickButton();
        ToggleAudio();
    }

    private void ToggleAudio()
    {
        if (isTransitioning) return;
        isAudioOn = !isAudioOn;
        buttonImage.sprite = isAudioOn ? onSprite : offSprite;
        StartCoroutine(ToggleAudioCoroutine(isAudioOn));
    }

    private IEnumerator ToggleAudioCoroutine(bool isAudioOn)
    {
        isTransitioning = true;

        float initialValue = isAudioOn ? 0 : 1;
        float finalValue = isAudioOn ? 1 : 0;
        float timer = 0f;

        while (timer < transitionTime)
        {
            float ratio = timer / transitionTime;

            foreach (AudioSource source in sources)
            {
                source.volume = Mathf.Lerp(initialValue, finalValue, ratio);
            }

            timer += Time.deltaTime;
            yield return null;
        }

        isTransitioning = false;
    }

    protected override void EvaluateShowingLogic(bool val)
    {
        ShowButton(!val);
    }
    #endregion
}