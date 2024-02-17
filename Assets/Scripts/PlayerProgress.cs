public class PlayerProgress
{
    private static int currentPhase = 0;
    public static int CurrentPhase
    {
        get => currentPhase;
        set
        {
            if (currentPhase == value)
            {
                return;
            }

            currentPhase = value;
            OnPhaseChange?.Invoke(currentPhase);
        }
    }

    public delegate void PhaseEventHandler(int newPhase);
    public static event PhaseEventHandler OnPhaseChange;

    private PlayerProgress() { }
}
