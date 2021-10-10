using System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private GoalTrigger playerGoal, enemyGoal;

    public Action<int, int> OnScoreChanged;
    
    private int playerScore, enemyScore;
    
    
    
    protected override void Awake()
    {
        base.Awake();

        playerGoal.OnGoalScored += () => OnGoalScored(false);
        enemyGoal.OnGoalScored += () => OnGoalScored(true);
    }

    private void OnGoalScored(bool isPlayerGoal)
    {
        if (isPlayerGoal) playerScore++;
        else enemyScore++;
        
        OnScoreChanged.Invoke(playerScore, enemyScore);
    }
}
