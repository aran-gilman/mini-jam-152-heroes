using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SwordSwing : MonoBehaviour
{
    private Animator animator;
    private RuntimeAnimationPosition animationPosition;

    public void PerformAction(Vector3 position, float rotation)
    {
        animationPosition.Position = position;
        animationPosition.Rotation = Quaternion.Euler(0, 0, rotation);
        animator.SetTrigger("Attack");
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animationPosition = animator.GetBehaviour<RuntimeAnimationPosition>();
    }
}
