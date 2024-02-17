using UnityEngine;

public class PlayerProgressController : MonoBehaviour
{
    public void AdvancePhase()
    {
        PlayerProgress.CurrentPhase += 1;
    }

    public void SetPhase(int newPhase)
    {
        PlayerProgress.CurrentPhase = newPhase;
    }
}
