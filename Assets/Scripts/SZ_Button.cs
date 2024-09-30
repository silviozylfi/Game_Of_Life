using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class SZ_Button : MonoBehaviour
{
    #region REFERENCES
    private Button button;
    protected Image buttonImage;
    #endregion

    #region EVENTS
    public static Action OnButtonClicked;
    #endregion

    #region LIFECYCLE
    protected virtual void Awake()
    {
        button = GetComponentInChildren<Button>();
        buttonImage = button.image;
    }

    protected virtual void OnEnable()
    {
        button.onClick.AddListener(ClickButton);
    }

    protected virtual void OnDisable()
    {
        button.onClick.RemoveListener(ClickButton);
    }

    protected virtual void Start()
    {
        ShowButton(false);
    }
    #endregion

    #region METHODS
    protected void ShowButton(bool val)
    {
        buttonImage.enabled = val;
        EnableButtonInteractable(val);
    }

    protected void EnableButtonInteractable(bool val)
    {
        button.interactable = val;
    }

    protected virtual void ClickButton()
    {
        OnButtonClicked?.Invoke();
    }

    protected virtual void EvaluateShowingLogic(bool val) { }
    protected virtual void EvaluateEnablingLogic(bool val) { }
    #endregion
}