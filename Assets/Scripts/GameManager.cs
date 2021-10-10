using System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private GoalTrigger playerGoal, enemyGoal;

    public struct ScoreData
    {
        public int PlayerScore
        {
            get => playerScore;
            set
            {
                playerScore = value;
                OnScoreChanged.Invoke();
            }
        }
        
        public int EnemyScore
        {
            get => enemyScore;
            set
            {
                enemyScore = value;
                OnScoreChanged.Invoke();
            }
        }

        private int playerScore, enemyScore;
        
        public Action OnScoreChanged;
    }
    
    public ScoreData Score;
    
    protected override void Awake()
    {
        base.Awake();

        playerGoal.OnGoalScored += () => OnGoalScored(false);
        enemyGoal.OnGoalScored += () => OnGoalScored(true);
    }

    private void OnGoalScored(bool isPlayerGoal)
    {
        if (isPlayerGoal) Score.PlayerScore++;
        else Score.EnemyScore++;
    }
}
