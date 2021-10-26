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
    [SerializeField, Min(1)] private int setPoints = 5;
    
    [NonSerialized] public DiscBehaviour DiscInstance;

    public static void TogglePause()
    {
        if (StateManager.State != StateManager.GameState.Paused)
        {
            StateManager.State = StateManager.GameState.Paused;
            Time.timeScale = 0f;
        }
        else
        {
            StateManager.State = StateManager.PreviousState;
            Time.timeScale = 1f;
        }
    }
    
    protected override void Awake()
    {
        base.Awake();

        playerGoal.OnGoalScored += () => OnGoalScored(false);
        enemyGoal.OnGoalScored += () => OnGoalScored(true);
        
        DiscInstance = Instantiate(discPrefab);
        DiscInstance.gameObject.SetActive(false);
        StartRound();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) TogglePause();
    }

    private void OnGoalScored(bool isPlayerGoal)
    {
        if (StateManager.State != StateManager.GameState.Running) return;

        if (isPlayerGoal) ScoreManager.PlayerScore++;
        else ScoreManager.EnemyScore++;

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
        StateManager.State = StateManager.GameState.RoundEnded;
        
        this.DelayedAction(() =>
        {
            DiscInstance.gameObject.SetActive(false);
            StartRound();
        }, roundEndDelay);
    }

    private void CheckSetEnded()
    {
        bool setHasEnded = false;
        
        if (ScoreManager.PlayerScore >= setPoints)
        {
            ScoreManager.PlayerSets++;
            setHasEnded = true;
        }

        if (ScoreManager.EnemyScore >= setPoints)
        {
            ScoreManager.EnemySets++;
            setHasEnded = true;
        }

        if (setHasEnded)
        {
            ScoreManager.PlayerScore = ScoreManager.EnemyScore = 0;
        }
    }
}
