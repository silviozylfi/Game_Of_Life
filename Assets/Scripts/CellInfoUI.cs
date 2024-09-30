using UnityEngine;
using TMPro;

public class CellInfoUI : MonoBehaviour
{
    #region REFERENCES
    [SerializeField] GameObject UIContainer;
    [SerializeField] private TMP_Text currentState;
    [SerializeField] private TMP_Text liveNeighborsField;
    [SerializeField] private TMP_Text nextState;
    #endregion

    #region CONFIGURATION
    [SerializeField] private Color aliveColor;
    [SerializeField] private Color deadColor;
    #endregion

    #region LIFECYCLE
    private void OnEnable()
    {
        CellGraphics.OnHover += UpdateUI;
    }

    private void OnDisable()
    {
        ClearUI();
    }

    private void Start()
    {
        UpdateUI(null, Vector3.zero);
    }
    #endregion

    #region METHODS
    private void UpdateUI(Cell cell, Vector3 spawnPosition)
    {
        if (cell == null)
        {
            UIContainer.SetActive(false);
            return;
        }

        bool isAlive = cell.IsAlive;
        bool willLive = cell.WillLive;
        int liveNeighbors = cell.LiveNeighbors;

        currentState.color = isAlive ? aliveColor : deadColor;
        currentState.text = isAlive ? "Alive" : "Dead";

        liveNeighborsField.text = liveNeighbors.ToString();

        nextState.color = willLive ? aliveColor : deadColor;
        nextState.text = willLive ? "Alive" : "Dead";

        transform.position = spawnPosition;
        UIContainer.SetActive(true);
    }

    private void ClearUI()
    {
        currentState.text = "";
        liveNeighborsField.text = "";
        nextState.text = "";
    }
    #endregion
}