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
        Debug.Log($"Attacked at position {playerPointer.transform.position}");
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