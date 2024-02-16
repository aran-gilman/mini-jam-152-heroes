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
    }
}
