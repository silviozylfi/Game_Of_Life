using UnityEngine;

[CreateAssetMenu(fileName = "InfoData", menuName = "ScriptableObjects/InfoData", order = 1)]
public class InfoSO : ScriptableObject
{
    #region CONFIGURATION
    [TextArea(3, 15)] public string[] Lines;
    #endregion
}
