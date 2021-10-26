using UnityEngine;

namespace UI
{
    public class ScoreUIController : MonoBehaviour
    {
        [SerializeField] private FadeInLabel playerScoreLabel, enemyScoreLabel;
        [SerializeField] private SetCounter playerSetCounter, enemySetCounter;

        private void Start()
        {
            playerScoreLabel.OnFadeOutFinished += UpdatePlayerScore;
            enemyScoreLabel.OnFadeOutFinished += UpdateEnemyScore;

            ScoreManager.OnPlayerScoreChanged += () => playerScoreLabel.FadeOut();
            ScoreManager.OnEnemyScoreChanged += () => enemyScoreLabel.FadeOut();

            ScoreManager.OnPlayerSetsChanged += () => playerSetCounter.SetCount = ScoreManager.PlayerSets;
            ScoreManager.OnEnemySetsChanged += () => enemySetCounter.SetCount = ScoreManager.EnemySets;
        }
        
        private void UpdatePlayerScore() => playerScoreLabel.FadeIn(ScoreManager.PlayerScore.ToString());
        private void UpdateEnemyScore() => enemyScoreLabel.FadeIn(ScoreManager.EnemyScore.ToString());
    }
}