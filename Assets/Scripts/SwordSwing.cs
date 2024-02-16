using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SwordSwing : MonoBehaviour
{
    private Animator animator;
    private GameObject swordPosition;

    public void PerformAction(Vector3 position, float rotation)
    {
        swordPosition.transform.SetPositionAndRotation(
            position, Quaternion.Euler(0, 0, rotation));
        animator.SetTrigger("Attack");
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();

        RuntimeAnimationPosition animationPosition = animator.GetBehaviour<RuntimeAnimationPosition>();

        swordPosition = new GameObject("PlayerSwordSwingPosition");
        animationPosition.StateTransformMap.Add("SwordSwing", swordPosition.transform);
    }

    private void OnEnable()
    {
        swordPosition.SetActive(true);
    }

    private void OnDisable()
    {
        if (swordPosition != null)
        {
            swordPosition.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        Destroy(swordPosition);
    }
}
