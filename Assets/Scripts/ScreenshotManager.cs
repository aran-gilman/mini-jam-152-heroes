using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenshotManager : MonoBehaviour
{
    [SerializeField]
    private InputActionReference screenshotAction;

    private void TakeScreenshot(InputAction.CallbackContext context)
    {
        ScreenCapture.CaptureScreenshot($"screenshot-{Time.unscaledTime}.png");
    }

    private void OnEnable()
    {
        screenshotAction.action.actionMap.Enable();
        screenshotAction.action.performed += TakeScreenshot;
    }

    private void OnDisable()
    {
        screenshotAction.action.actionMap.Disable();
        screenshotAction.action.performed -= TakeScreenshot;
    }
}
