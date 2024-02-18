using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PhaseListener : MonoBehaviour
{
    [Tooltip("Set to -1 to target all phases.")]
    [SerializeField]
    private int targetPhase;

    [SerializeField]
    private bool delayToNextFrame = false;

    [SerializeField]
    private UnityEvent onTargetPhaseEnter;

    [SerializeField]
    private UnityEvent onOtherPhaseEnter;

    private void HandlePhaseChange(int newPhase)
    {
        if (delayToNextFrame)
        {
            StartCoroutine(DelayedInvokeEvent(newPhase));
        }
        else
        {
            InvokeEvent(newPhase);
        }
    }

    private void OnEnable()
    {
        HandlePhaseChange(PlayerProgress.CurrentPhase);
        PlayerProgress.OnPhaseChange += HandlePhaseChange;
    }

    private void OnDisable()
    {
        PlayerProgress.OnPhaseChange -= HandlePhaseChange;
    }

    private IEnumerator DelayedInvokeEvent(int newPhase)
    {
        yield return null;
        InvokeEvent(newPhase);
    }

    private void InvokeEvent(int newPhase)
    {
        if (targetPhase < 0 || newPhase == targetPhase)
        {
            onTargetPhaseEnter.Invoke();
        }
        else
        {
            onOtherPhaseEnter.Invoke();
        }
    }
}
