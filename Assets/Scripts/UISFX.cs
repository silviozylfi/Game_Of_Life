using UnityEngine;

public class UISFX : MonoBehaviour
{
    #region REFERENCES
    private AudioSource audioSource;
    private ProjectInfoPanel projectInfoPanel;
    #endregion

    #region CONFIGURATION
    [SerializeField] private AudioClip clickClip;
    [SerializeField] private AudioClip typingClip;
    [SerializeField] private AudioClip cellBirth;
    [SerializeField] private AudioClip cellHover;
    #endregion

    #region LIFECYCLE
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        projectInfoPanel = FindObjectOfType<ProjectInfoPanel>();
    }

    private void OnEnable()
    {
        SZ_Button.OnButtonClicked += PlayClickClip;
        Cell.OnBirth += PlayBirthClip;
        CellGraphics.OnHover += PlayCellHoverClip;
        projectInfoPanel.OnCharacterTyped += PlayTypingClip;
    }

    private void OnDisable()
    {
        SZ_Button.OnButtonClicked -= PlayClickClip;
        Cell.OnBirth -= PlayBirthClip;
        CellGraphics.OnHover -= PlayCellHoverClip;
        projectInfoPanel.OnCharacterTyped -= PlayTypingClip;
    }
    #endregion

    #region METHODS
    private void PlayClickClip()
    {
        audioSource.PlayOneShot(clickClip);
    }

    private void PlayTypingClip()
    {
        audioSource.PlayOneShot(typingClip);
    }

    private void PlayBirthClip(Cell _)
    {
        if (audioSource.isPlaying) return;
        audioSource.PlayOneShot(cellBirth);
    }

    private void PlayCellHoverClip(Cell cell, Vector3 __)
    {
        if (!cell) return;
        audioSource.PlayOneShot(cellHover);
    }
    #endregion
}