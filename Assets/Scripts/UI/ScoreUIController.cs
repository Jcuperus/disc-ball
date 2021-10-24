using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreUIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text playerScoreLabel, enemyScoreLabel;

        private GameManager gameManager;
    
        private void Start()
        {
            gameManager = GameManager.Instance;
            gameManager.Score.OnScoreChanged += OnScoreChanged;
        }

        private void OnScoreChanged()
        {
            playerScoreLabel.text = gameManager.Score.PlayerScore.ToString();
            enemyScoreLabel.text = gameManager.Score.EnemyScore.ToString();
        }
    }
}