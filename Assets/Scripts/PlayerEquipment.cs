using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [SerializeField]
    private GameObject swordAPrefab;

    [SerializeField]
    private GameObject swordBPrefab;

    [SerializeField]
    private GameObject slotA;

    [SerializeField]
    private GameObject slotB;

    public GameObject SwordA { get; private set; }
    public GameObject SwordB { get; private set; }

    public void SwapEquipment(GameObject oldSword, GameObject newSword)
    {
        if (oldSword == SwordA)
        {
            SwordA = newSword;
            SetSlot(SwordA, slotA);
        }
        else if (oldSword == SwordB)
        {
            SwordB = newSword;
            SetSlot(SwordB, slotB);
        }
    }

    private void Awake()
    {
        SwordA = Instantiate(swordAPrefab, slotA.transform);
        SwordB = Instantiate(swordBPrefab, slotB.transform);

        SetDefaultSwordLocation(SwordA, slotA);
        SetDefaultSwordLocation(SwordB, slotB);
    }

    private void SetDefaultSwordLocation(GameObject sword, GameObject equipmentSlot)
    {
        Animator animator = sword.GetComponent<Animator>();
        RuntimeAnimationPosition animationPosition = animator.GetBehaviour<RuntimeAnimationPosition>();
        animationPosition.StateTransformMap["Default"] = equipmentSlot.transform;
    }

    private void SetSlot(GameObject sword, GameObject slot)
    {
        sword.transform.parent = slot.transform;
        sword.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        SetDefaultSwordLocation(sword, slot);
    }
}
