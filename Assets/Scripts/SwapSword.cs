using UnityEngine;
using UnityEngine.Events;

public class SwapSword : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onSwap;

    private Animator animator;
    private GameObject swordRoot;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        swordRoot = animator.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectable"))
        {
            PlayerEquipment playerEquipment = GetComponentInParent<PlayerEquipment>();
            if (playerEquipment != null && collision.transform.childCount > 0)
            {
                GameObject newSword = collision.transform.GetChild(0).gameObject;
                playerEquipment.SwapEquipment(swordRoot, newSword);
            }
            swordRoot.transform.parent = collision.transform;
            swordRoot.transform.SetLocalPositionAndRotation(
                Vector3.zero, Quaternion.identity);
            onSwap.Invoke();
        }
    }
}
