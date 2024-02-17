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
    private Vector2 currentInput;

    private void HandleMove(InputAction.CallbackContext ctx)
    {
        currentInput = ctx.ReadValue<Vector2>();
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

    private void Update()
    {
        rb.velocity = currentInput * moveSpeed;
    }
}
