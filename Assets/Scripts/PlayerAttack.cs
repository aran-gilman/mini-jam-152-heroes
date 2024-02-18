using System.Collections;
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

    [SerializeField]
    private float attackCooldown = 0.5f;

    [SerializeField]
    private GameObject swordIndicatorPrefab;

    private GameObject swordPosition;

    private bool isOnCooldown = false;

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
        if (isOnCooldown)
        {
            return;
        }

        StartCoroutine(RunAttackCooldown());

        Vector3 direction = (playerPointer.transform.position - transform.position).normalized;
        Vector3 swordPos = transform.position + direction * swordDistanceFromPlayer;  
        float swordAngle = Mathf.Rad2Deg * Mathf.Atan2(
                -direction.x, direction.y);

        swordPosition.transform.SetPositionAndRotation(
            swordPos, Quaternion.Euler(0, 0, swordAngle));
        sword.GetComponent<SwordSwing>().PerformAction(swordPosition.transform);
    }

    private IEnumerator RunAttackCooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        isOnCooldown = false;
    }

    private void Awake()
    {
        swordPosition = Instantiate(swordIndicatorPrefab);
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