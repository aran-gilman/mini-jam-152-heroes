using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SwordSwing : MonoBehaviour
{
    private Animator animator;
    private RuntimeAnimationPosition animationPosition;

    public void PerformAction(Transform position)
    {
        animationPosition.StateTransformMap["SwordSwing"] = position;
        animator.SetTrigger("Attack");
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animationPosition = animator.GetBehaviour<RuntimeAnimationPosition>();
    }
}
