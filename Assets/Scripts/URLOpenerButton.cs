using UnityEngine;

public class URLOpenerButton : SZ_Button
{
    #region REFERENCES
    private ProjectInfoPanel projectInfoPanel;
    #endregion

    #region CONFIGURATION 
    [SerializeField] string url = "https://www.instagram.com/silvio_zylfi_/";
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
    #endregion

    #region METHODS
    protected override void ClickButton()
    {
        base.ClickButton();
        OpenWebsite();
    }

    protected override void EvaluateShowingLogic(bool val)
    {
        ShowButton(!val);
    }

    private void OpenWebsite()
    {
        Application.OpenURL(url);
    }
    #endregion
}