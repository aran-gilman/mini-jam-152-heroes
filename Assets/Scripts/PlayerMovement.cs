using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private InputActionReference moveAction;

    [SerializeField]
    private float moveSpeed;

    private Rigidbody2D rb;

    private void HandleMove(InputAction.CallbackContext ctx)
    {
        Vector2 val = ctx.ReadValue<Vector2>();
        rb.velocity = val * moveSpeed;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        moveAction.action.actionMap.Enable();
        moveAction.action.performed += HandleMove;
        moveAction.action.canceled += HandleMove;
    }

    private void OnDisable()
    {
        moveAction.action.performed -= HandleMove;
        moveAction.action.canceled -= HandleMove;
    }
}
