using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPointer : MonoBehaviour
{
    [SerializeField]
    private InputActionReference pointerPosition;

    private Camera mainCamera;

    private void HandlePoint(InputAction.CallbackContext ctx)
    {
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
        worldPos.z = 0;
        transform.position = worldPos;
    }

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        pointerPosition.action.actionMap.Enable();
        pointerPosition.action.performed += HandlePoint;
    }

    private void OnDisable()
    {
        pointerPosition.action.performed -= HandlePoint;
    }
}
