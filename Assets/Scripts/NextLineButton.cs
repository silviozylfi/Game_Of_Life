public class NextLineButton : SZ_Button
{
    #region REFERENCES
    private ProjectInfoPanel projectInfoPanel;
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
        projectInfoPanel.OnDisplayingLine += EvaluateCurrentLine;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        projectInfoPanel.OnDisplayingLine -= EvaluateCurrentLine;
    }
    #endregion

    #region METHODS
    private void EvaluateCurrentLine(int i)
    {
        ShowButton(i >= 0 && i != projectInfoPanel.CurrentInfoData.Lines.Length - 1);
    }

    protected override void ClickButton()
    {
        base.ClickButton();
        projectInfoPanel.DisplayNextLine();
    }
    #endregion
}