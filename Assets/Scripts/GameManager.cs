using System;
using System.Collections;
using Helpers;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoSingleton<GameManager>
{
    [NonSerialized] public DiscBehaviour DiscInstance;
    public Action<bool> OnGameEnd;
    
    [SerializeField] private GoalTrigger playerGoal, enemyGoal;
    [SerializeField] private DiscBehaviour discPrefab;
    [SerializeField] private Vector3 discSpawnPosition;
    [SerializeField, Min(0)] private int newRoundCountdownAmount = 3;
    [SerializeField] private float roundEndDelay = 0.1f;
    private GameConfigurationData gameConfiguration;

    public void StartGame()
    {
        ScoreManager.Reset();
        StateManager.State = StateManager.GameState.Running;
        StartRound();
    }
    
    protected override void Awake()
    {
        base.Awake();

        gameConfiguration = GameConfigurationManager.Instance.GameConfig;

        playerGoal.OnGoalScored += () => OnGoalScored(false);
        enemyGoal.OnGoalScored += () => OnGoalScored(true);
        
        DiscInstance = Instantiate(discPrefab);
        DiscInstance.gameObject.SetActive(false);
        
        StartGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) StateManager.TogglePause();
    }

    private void OnGoalScored(bool isPlayerGoal)
    {
        if (StateManager.State != StateManager.GameState.Running) return;

        ScoreManager.ScoreData scorerData = isPlayerGoal ? ScoreManager.RedScore : ScoreManager.BlueScore;
        scorerData.Points++;

        CheckSetEnded();
        EndRound();
    }

    private void StartRound()
    {
        StartCoroutine(StartRoundCoroutine());
    }

    private IEnumerator StartRoundCoroutine()
    {
        yield return CountdownHelper.CountDownCoroutine(newRoundCountdownAmount);
        
        StateManager.State = StateManager.GameState.Running;
        
        DiscInstance.transform.position = discSpawnPosition;
        Vector3 initialDiscVelocity = Random.Range(0, 2) > 0 ? Vector3.left : Vector3.right;
        DiscInstance.velocity = initialDiscVelocity;
        DiscInstance.gameObject.SetActive(true);
    }

    private void EndRound()
    {
        DiscInstance.gameObject.SetActive(false);

        if (StateManager.State == StateManager.GameState.GameEnded) return;
        
        StateManager.State = StateManager.GameState.RoundEnded;
        
        this.DelayedAction(StartRound, roundEndDelay);
    }

    private void CheckSetEnded()
    {
        bool setHasEnded = false;

        foreach (ScoreManager.ScoreData scoreData in new[] { ScoreManager.RedScore, ScoreManager.BlueScore })
        {
            if (scoreData.Points >= gameConfiguration.setPoints)
            {
                scoreData.Sets++;
                setHasEnded = true;
            }
        }

        if (setHasEnded)
        {
            CheckVictory();
            ScoreManager.ResetPoints();
        }
    }

    private void CheckVictory()
    {
        if (ScoreManager.RedScore.Sets >= gameConfiguration.gameSets)
        {
            EndGame(true);
        }
        else if (ScoreManager.BlueScore.Sets >= gameConfiguration.gameSets)
        {
            EndGame(false);
        }
    }

    private void EndGame(bool redWins)
    {
        StateManager.State = StateManager.GameState.GameEnded;
        OnGameEnd?.Invoke(redWins);
    }
}
