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
        animationPosition.StateTransformMap.Add("Default", equipmentSlot.transform);
    }
}
