using System;

public static class StateManager
{
    public enum GameState
    {
        Running,
        RoundEnded,
        Paused
    }

    public static GameState State
    {
        get => state;
        set
        {
            PreviousState = state;
            state = value;
            OnStateChanged.Invoke(state);
        }
    }
    
    private static GameState state = GameState.Running;
    
    public static Action<GameState> OnStateChanged;

    public static GameState PreviousState { get; private set; }
}