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
        Ray toPointer = new Ray(
            transform.position, playerPointer.transform.position);

        Vector3 swordPos = toPointer.GetPoint(swordDistanceFromPlayer);
        Quaternion swordRotation = Quaternion.Euler(0, 0, 
            Mathf.Rad2Deg * Mathf.Atan2(
                -playerPointer.transform.position.x, playerPointer.transform.position.y));
        sword.transform.SetPositionAndRotation(swordPos, swordRotation);

        sword.GetComponent<Animator>().SetTrigger("Attack");
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