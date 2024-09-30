using UnityEngine;
using UnityEngine.UI;
using System;

public class CellGraphics : MonoBehaviour
{
    #region REFERENCES
    private Cell cell;
    private MatrixGenerator matrixGenerator;
    private ProjectInfoPanel projectInfoPanel;
    private BoxCollider col;
    [SerializeField] private Image squareImage;
    [SerializeField] GameObject plant;
    [SerializeField] ParticleSystem puff;
    [SerializeField] Transform uiPanelSpawnPoint;
    #endregion

    #region CONFIGURATION
    [SerializeField] Color aliveColor;
    [SerializeField] Color deadColor;
    [SerializeField] Color highlightedColor;
    [SerializeField] Color generatedColor;
    #endregion

    #region EVENTS
    public static Action<Cell, Vector3> OnHover;
    #endregion

    #region LIFECYCLE
    private void Awake()
    {
        cell = GetComponent<Cell>();
        matrixGenerator = FindObjectOfType<MatrixGenerator>();
        projectInfoPanel = FindObjectOfType<ProjectInfoPanel>();
        col = GetComponent<BoxCollider>();
    }

    private void OnEnable()
    {
        cell.OnGenerated += UpdateUI;
        Cell.OnBirth += PlayPuff;
        matrixGenerator.OnGenerating += DisableCollider;
        matrixGenerator.OnCellEvaluating += GeneratedCellColor;
        projectInfoPanel.OnDisplayingInfo += DisableCollider;
    }

    private void OnDisable()
    {
        cell.OnGenerated -= UpdateUI;
        Cell.OnBirth -= PlayPuff;
        matrixGenerator.OnGenerating -= DisableCollider;
        matrixGenerator.OnCellEvaluating -= GeneratedCellColor;
        projectInfoPanel.OnDisplayingInfo -= DisableCollider;
    }

    private void Start()
    {
        DisableCollider(true);
    }
    #endregion

    #region METHODS
    private void OnMouseEnter()
    {
        HighlightCell(true);
        OnHover?.Invoke(cell, uiPanelSpawnPoint.position);
    }

    private void OnMouseExit()
    {
        HighlightCell(false);
        OnHover?.Invoke(null, Vector3.zero);
    }

    private void UpdateUI(bool isAlive)
    {
        plant.SetActive(isAlive);
        squareImage.color = isAlive ? aliveColor : deadColor;
    }

    private void HighlightCell(bool isHiglighted)
    {
        if (isHiglighted) squareImage.color = highlightedColor;
        else UpdateUI(cell.IsAlive);
    }

    private void GeneratedCellColor(Cell currentlyGeneratedCell)
    {
        if (currentlyGeneratedCell == this.cell)
        {
            squareImage.color = generatedColor;
        }
    }

    private void DisableCollider(bool val)
    {
        col.enabled = !val;
    }

    private void PlayPuff(Cell bornCell)
    {
        if (bornCell == this.cell) puff.Play();
    }
    #endregion
}