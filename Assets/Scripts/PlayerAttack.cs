using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private InputActionReference attackAAction;

    [SerializeField]
    private InputActionReference attackBAction;

    [SerializeField]
    private PlayerPointer playerPointer;

    [SerializeField]
    private PlayerEquipment equipment;

    [SerializeField]
    private float swordDistanceFromPlayer = 1.0f;

    private GameObject swordPosition;

    private void HandleAttackA(InputAction.CallbackContext ctx)
    {
        HandleAttack(equipment.SwordA);
    }

    private void HandleAttackB(InputAction.CallbackContext ctx)
    {
        HandleAttack(equipment.SwordB);
    }

    private void HandleAttack(GameObject sword)
    {
        Vector3 direction = (playerPointer.transform.position - transform.position).normalized;
        Vector3 swordPos = transform.position + direction * swordDistanceFromPlayer;  
        float swordAngle = Mathf.Rad2Deg * Mathf.Atan2(
                -direction.x, direction.y);

        swordPosition.transform.SetPositionAndRotation(
            swordPos, Quaternion.Euler(0, 0, swordAngle));
        sword.GetComponent<SwordSwing>().PerformAction(swordPosition.transform);
    }

    private void Awake()
    {
        swordPosition = new GameObject("PlayerSwordSwingPosition");
    }

    private void OnEnable()
    {
        attackAAction.action.actionMap.Enable();

        attackAAction.action.started += HandleAttackA;
        attackBAction.action.started += HandleAttackB;
    }

    private void OnDisable()
    {
        attackAAction.action.started -= HandleAttackA;
        attackBAction.action.started -= HandleAttackB;
    }
}