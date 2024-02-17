public class PlayerProgress
{
    private static PlayerProgress instance;
    public static PlayerProgress Instance
    {
        get
        {
            instance ??= new PlayerProgress();
            return instance;
        }
    }

    private int currentPhase = 0;
    public int CurrentPhase
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
    public event PhaseEventHandler OnPhaseChange;

    private PlayerProgress() { }
}
