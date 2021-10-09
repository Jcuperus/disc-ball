using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private GoalTrigger playerGoal, enemyGoal;
    
    protected override void Awake()
    {
        base.Awake();

        playerGoal.OnGoalScored += () => OnGoalScored(false);
        enemyGoal.OnGoalScored += () => OnGoalScored(true);
    }

    private void OnGoalScored(bool isPlayerGoal)
    {
        Debug.Log(isPlayerGoal ? "Player" : "Enemy");
    }
}
