using System;

public static class ScoreManager
{
    public static int PlayerScore
    {
        get => playerScore;
        set
        {
            playerScore = value;
            OnPlayerScoreChanged.Invoke();
        }
    }

    private static int playerScore;

    public static int EnemyScore
    {
        get => enemyScore;
        set
        {
            enemyScore = value;
            OnEnemyScoreChanged.Invoke();
        }
    }

    private static int enemyScore;

    public static int PlayerSets
    {
        get => playerSets;
        set
        {
            playerSets = value;
            OnPlayerSetsChanged.Invoke();
        }
    }

    private static int playerSets;
    
    public static int EnemySets
    {
        get => enemySets;
        set
        {
            enemySets = value;
            OnEnemySetsChanged.Invoke();
        }
    }

    private static int enemySets;

    public static Action OnPlayerScoreChanged, OnEnemyScoreChanged, OnPlayerSetsChanged, OnEnemySetsChanged;
}