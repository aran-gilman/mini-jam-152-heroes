using UnityEngine;

public class SwapSword : MonoBehaviour
{
    private GameObject swordRoot;

    private void Awake()
    {
        swordRoot = GetComponentInParent<Animator>().gameObject;
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
        }
    }
}
