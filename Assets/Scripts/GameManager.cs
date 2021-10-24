using System;
using System.Collections;
using Helpers;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private GoalTrigger playerGoal, enemyGoal;
    [SerializeField] private DiscBehaviour discPrefab;
    [SerializeField] private Vector3 discSpawnPosition;
    [SerializeField, Min(0)] private int newRoundCountdownAmount = 3;
    [SerializeField] private float roundEndDelay = 0.1f;
    
    [NonSerialized] public DiscBehaviour DiscInstance;
    
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

    private enum GameState
    {
        Running,
        RoundEnded
    }

    private GameState state = GameState.Running;
    
    protected override void Awake()
    {
        base.Awake();

        playerGoal.OnGoalScored += () => OnGoalScored(false);
        enemyGoal.OnGoalScored += () => OnGoalScored(true);
        
        DiscInstance = Instantiate(discPrefab);
        DiscInstance.gameObject.SetActive(false);
        StartRound();
    }

    private void OnGoalScored(bool isPlayerGoal)
    {
        if (state != GameState.Running) return;
        
        if (isPlayerGoal) Score.PlayerScore++;
        else Score.EnemyScore++;

        EndRound();
    }

    private void StartRound()
    {
        StartCoroutine(StartRoundCoroutine());
    }

    private IEnumerator StartRoundCoroutine()
    {
        yield return CountdownHelper.CountDownCoroutine(newRoundCountdownAmount);
        
        state = GameState.Running;
        
        DiscInstance.transform.position = discSpawnPosition;
        Vector3 initialDiscVelocity = Random.Range(0, 2) > 0 ? Vector3.left : Vector3.right;
        DiscInstance.velocity = initialDiscVelocity;
        DiscInstance.gameObject.SetActive(true);
    }

    private void EndRound()
    {
        state = GameState.RoundEnded;
        
        this.DelayedAction(() =>
        {
            DiscInstance.gameObject.SetActive(false);
            StartRound();
        }, roundEndDelay);
    }
}
