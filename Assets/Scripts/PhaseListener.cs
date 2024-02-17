using UnityEngine;
using UnityEngine.Events;

public class PhaseListener : MonoBehaviour
{
    [SerializeField]
    private int targetPhase;

    [SerializeField]
    private UnityEvent onTargetPhaseEnter;

    [SerializeField]
    private UnityEvent onOtherPhaseEnter;

    private void HandlePhaseChange(int newPhase)
    {
        if (newPhase == targetPhase)
        {
            onTargetPhaseEnter.Invoke();
        }
        else
        {
            onOtherPhaseEnter.Invoke();
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
}
