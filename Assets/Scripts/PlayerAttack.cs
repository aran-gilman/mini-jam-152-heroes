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

    private void HandleAttackA(InputAction.CallbackContext ctx)
    {
        HandleAttack();
    }

    private void HandleAttackB(InputAction.CallbackContext ctx)
    {
        HandleAttack();
    }

    private void HandleAttack()
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