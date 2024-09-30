using UnityEngine;

public class QuitAppButton : SZ_Button
{
    #region REFERENCES
    private ProjectInfoPanel projectInfoPanel;
    private BlackScreen blackScreen;
    #endregion

    #region LIFECYCLE
    protected override void Awake()
    {
        base.Awake();
        projectInfoPanel = FindObjectOfType<ProjectInfoPanel>();
        blackScreen = FindObjectOfType<BlackScreen>();
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
        blackScreen.FadeOut();
        Invoke(nameof(QuitApp), 4.5f);
    }

    protected override void EvaluateShowingLogic(bool val)
    {
        ShowButton(!val);
    }

    private void QuitApp()
    {
        Application.Quit();
    }
    #endregion
}