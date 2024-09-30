using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class ProjectInfoPanel : MonoBehaviour
{
    #region REFERENCES
    [SerializeField] private TMP_Text textArea;
    #endregion

    #region CONFIGURATION
    [SerializeField] private float typingDelay = .1f;
    #endregion

    #region STATE
    private InfoSO currentInfoData; 
    private int currentLineIndex;
    private int typeCounter;
    #endregion

    #region EVENTS
    public Action OnCharacterTyped;
    public Action<bool> OnDisplayingInfo;
    public Action<int> OnDisplayingLine;
    #endregion

    #region LIFECYCLE
    private void Start()
    {
        textArea.text = "";
    }
    #endregion

    #region METHODS
    private void TypeCurrentLine()
    {
        StopAllCoroutines();
        StartCoroutine(TypeCoroutine());
    }

    private IEnumerator TypeCoroutine()
    {
        OnDisplayingLine?.Invoke(currentLineIndex);
        textArea.text = "";
        typeCounter = 0;

        foreach (char letter in CurrentInfoData.Lines[currentLineIndex].ToCharArray())
        {
            textArea.text += letter;
            typeCounter++;
            if (typeCounter == 8)
            {
                OnCharacterTyped?.Invoke();
                typeCounter = 0;
            }
            
            yield return new WaitForSeconds(typingDelay);
        }
    }

    public void DisplayNextLine()
    {
        currentLineIndex ++;
        TypeCurrentLine();
    }

    public void DisplayInfo(InfoSO info)
    {
        CurrentInfoData = info;
        currentLineIndex = 0;
        OnDisplayingInfo?.Invoke(true);
        TypeCurrentLine();
    }

    public void QuitDisplaying()
    {
        StopAllCoroutines();
        textArea.text = "";
        OnDisplayingLine?.Invoke(-1);
        OnDisplayingInfo?.Invoke(false);
    }
    #endregion

    #region PROPERTIES
    public InfoSO CurrentInfoData { get => currentInfoData; set => currentInfoData = value; }
    #endregion
}