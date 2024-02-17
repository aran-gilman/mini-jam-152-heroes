using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class SwordSwing : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onSwing;

    private Animator animator;
    private RuntimeAnimationPosition animationPosition;

    public void PerformAction(Transform position)
    {
        onSwing.Invoke();
        animationPosition.StateTransformMap["SwordSwing"] = position;
        animator.SetTrigger("Attack");
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animationPosition = animator.GetBehaviour<RuntimeAnimationPosition>();
    }
}
