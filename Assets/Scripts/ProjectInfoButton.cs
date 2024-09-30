using UnityEngine;

public class ProjectInfoButton : SZ_Button
{
    #region REFERENCES
    private ProjectInfoPanel projectInfoPanel;
    #endregion

    #region CONFIGURATION
    [SerializeField] private InfoSO projectInfoData;
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
        Invoke(nameof(DisplayInfo), 5f);
    }
    #endregion

    #region METHODS
    protected override void ClickButton()
    {
        base.ClickButton();
        DisplayInfo();
    }

    private void DisplayInfo()
    {
        projectInfoPanel.DisplayInfo(projectInfoData);
    }

    protected override void EvaluateShowingLogic(bool val)
    {
        ShowButton(!val);
    }
    #endregion
}