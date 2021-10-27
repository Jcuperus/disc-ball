using System;
using UnityEngine;

public static class StateManager
{
    public enum GameState
    {
        Running,
        RoundEnded,
        GameEnded,
        Paused
    }

    public static GameState State
    {
        get => state;
        set
        {
            previousState = state;
            state = value;
            OnStateChanged.Invoke(state);
        }
    }
    
    private static GameState state = GameState.Running, previousState = GameState.Running;
    
    public static Action<GameState> OnStateChanged;
    
    public static void TogglePause()
    {
        if (State != GameState.Paused)
        {
            State = GameState.Paused;
            Time.timeScale = 0f;
        }
        else
        {
            State = previousState;
            Time.timeScale = 1f;
        }
    }
}