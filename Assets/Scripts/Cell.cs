using UnityEngine;
using System;

public class Cell : MonoBehaviour
{
    #region STATE
    private bool isAlive;
    private int liveNeighbors;
    private bool willLive;
    #endregion

    #region EVENTS
    public Action<bool> OnGenerated;
    public static Action<Cell> OnBirth;
    #endregion

    #region LIFECYCLE
    private void Start()
    {
        SetAlive(false);
    }
    #endregion

    #region METHODS
    public void SetAlive(bool isAlive)
    {
        bool previousVal = this.isAlive;
        this.isAlive = isAlive;
        OnGenerated?.Invoke(isAlive);

        if (!previousVal && isAlive)
        {
            OnBirth?.Invoke(this);
        }
    }
    #endregion

    #region PROPERTIES
    public bool IsAlive { get { return isAlive; } }
    public int LiveNeighbors { get => liveNeighbors; set => liveNeighbors = value; }
    public bool WillLive { get => willLive; set => willLive = value; }
    #endregion
}