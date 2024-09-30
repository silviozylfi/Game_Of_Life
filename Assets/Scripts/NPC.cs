using UnityEngine;

public class NPC : MonoBehaviour
{
    #region REFERENCES
    private Animator animator;
    #endregion

    #region CONFIGURATION
    [SerializeField] string triggerString;
    #endregion

    #region LIFECYCLE
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        animator.SetTrigger(triggerString);
    }
    #endregion
}