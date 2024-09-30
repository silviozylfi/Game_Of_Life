public class NewMatrixButton : SZ_Button
{
    #region REFERENCES
    private MatrixGenerator matrixGenerator;
    private ProjectInfoPanel projectInfoPanel;
    #endregion

    #region LIFECYCLE
    protected override void Awake()
    {
        base.Awake();
        matrixGenerator = FindObjectOfType<MatrixGenerator>();
        projectInfoPanel = FindObjectOfType<ProjectInfoPanel>();
    }
    
    protected override void OnEnable()
    {
        base.OnEnable();
        matrixGenerator.OnGenerating += EvaluateEnablingLogic;
        projectInfoPanel.OnDisplayingInfo += EvaluateShowingLogic;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        matrixGenerator.OnGenerating -= EvaluateEnablingLogic;
        projectInfoPanel.OnDisplayingInfo -= EvaluateShowingLogic;
    }
    #endregion

    #region METHODS
    protected override void ClickButton()
    {
        base.ClickButton();
        matrixGenerator.GenerateNewRandomMatrix();
    }

    protected override void EvaluateEnablingLogic(bool val)
    {
        EnableButtonInteractable(!val);
    }

    protected override void EvaluateShowingLogic(bool val)
    {
        ShowButton(!val);
    }
    #endregion
}