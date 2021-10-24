using UnityEngine;

namespace UI
{
    public class ScoreUIController : MonoBehaviour
    {
        [SerializeField] private FadeInLabel playerScoreLabel, enemyScoreLabel;

        private GameManager gameManager;
        
        private void Start()
        {
            gameManager = GameManager.Instance;

            playerScoreLabel.OnFadeOutFinished += UpdatePlayerScore;
            enemyScoreLabel.OnFadeOutFinished += UpdateEnemyScore;
            
            gameManager.Score.OnPlayerScoreChanged += () => playerScoreLabel.FadeOut();
            gameManager.Score.OnEnemyScoreChanged += () => enemyScoreLabel.FadeOut();
        }
        
        private void UpdatePlayerScore() => playerScoreLabel.FadeIn(gameManager.Score.PlayerScore.ToString());
        private void UpdateEnemyScore() => enemyScoreLabel.FadeIn(gameManager.Score.EnemyScore.ToString());
    }
}