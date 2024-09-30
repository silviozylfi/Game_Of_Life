using UnityEngine;
using TMPro;

public class DateUI : MonoBehaviour
{
    #region REFERENCES
    private TMP_Text textField;
    private ProjectInfoPanel projectInfoPanel;
    #endregion

    #region CONFIGURATION
    [SerializeField] string textToDisplay = "WARSAW, SEPTEMBER 2024";
    #endregion

    #region LIFECYCLE
    private void Awake()
    {
        textField = GetComponentInChildren<TMP_Text>();
        projectInfoPanel = FindObjectOfType<ProjectInfoPanel>();
    }

    private void OnEnable()
    {
        projectInfoPanel.OnDisplayingInfo += HideText;
    }

    private void OnDisable()
    {
        projectInfoPanel.OnDisplayingInfo -= HideText;
    }

    private void Start()
    {
        HideText(true);
    }
    #endregion

    #region METHODS
    private void HideText(bool hide)
    {
        textField.text = hide ? "" : textToDisplay;
    }
    #endregion
}