using System.Collections;
using UnityEngine;
using System;

public class MatrixGenerator : MonoBehaviour
{
    #region REFERENCES
    [SerializeField] private Transform matrixTransform;
    [SerializeField] private Cell cellPrefab;
    #endregion

    #region CONFIGURATION
    private const int ROWS = 10;
    private const int COLUMNS = 10;

    [SerializeField] private float generationInterval = .1f;

    private Cell[,] matrix = new Cell[ROWS, COLUMNS];
    private bool[,] nextValues = new bool[ROWS, COLUMNS];
    #endregion

    #region STATE
    private bool isGenerating;
    #endregion

    #region EVENTS
    public Action<bool> OnGenerating;
    public Action<Cell> OnCellEvaluating;
    #endregion

    #region LIFECYCLE
    private void Start()
    {
        GenerateEmptyMatrix();
        isGenerating = false;
    }
    #endregion

    #region METHODS
    private void GenerateEmptyMatrix()
    {
        Vector3 matrixPosition = matrixTransform.position;
        for (int i = 0; i < ROWS; i++)
        {
            for (int j = 0; j < COLUMNS; j++)
            {
                Vector3 cellPosition = new Vector3(-i, 0, -j) + matrixPosition;
                Cell newCell = Instantiate<Cell>(cellPrefab, cellPosition, Quaternion.identity, matrixTransform);
                matrix[i, j] = newCell;
            }
        }
    }

    public void GenerateNewRandomMatrix()
    {
        if (isGenerating) return;
        ResetMatrix();
        StartCoroutine(GenerateNewRandomMatrixCoroutine());
    }

    private IEnumerator GenerateNewRandomMatrixCoroutine()
    {
        isGenerating = true;
        OnGenerating?.Invoke(true);

        for (int i = 0; i < ROWS; i++)
        {
            for (int j = 0; j < COLUMNS; j++)
            {
                OnCellEvaluating?.Invoke(matrix[i, j]);
                yield return new WaitForSeconds(generationInterval);
                matrix[i, j].SetAlive(UnityEngine.Random.Range(0, 2) == 1);
            }
        }

        CalculateNextValues();

        OnGenerating?.Invoke(false);
        isGenerating = false;
    }

    private void ResetMatrix()
    {
        for (int i = 0; i < ROWS; i++)
        {
            for (int j = 0; j < COLUMNS; j++)
            {
                matrix[i, j].SetAlive(false);
            }
        }
    }

    private void CalculateNextValues()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                int liveNeighbors = CountLiveNeighbors(x, y);
                if (matrix[x, y].IsAlive) nextValues[x, y] = liveNeighbors == 2 || liveNeighbors == 3;
                else nextValues[x, y] = liveNeighbors == 3;
                matrix[x, y].LiveNeighbors = liveNeighbors;
                matrix[x, y].WillLive = nextValues[x, y];
            }
        }
    }

    public void GenerateNextStep()
    {
        for (int i = 0; i < ROWS; i++)
        {
            for (int j = 0; j < COLUMNS; j++)
            {
                matrix[i, j].SetAlive(nextValues[i, j]);
            }
        }
        CalculateNextValues();
    }

    int CountLiveNeighbors(int x, int y)
    {
        int liveNeighbors = 0;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue;

                int nx = (x + i + 10) % 10;
                int ny = (y + j + 10) % 10;

                if (matrix[nx, ny].IsAlive)
                {
                    liveNeighbors++;
                }
            }
        }

        return liveNeighbors;
    }
    #endregion 
}