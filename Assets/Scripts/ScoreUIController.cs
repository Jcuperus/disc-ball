using TMPro;
using UnityEngine;

public class ScoreUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text playerScoreLabel, enemyScoreLabel;

    private void Start()
    {
        GameManager.Instance.OnScoreChanged += OnScoreChanged;
    }

    private void OnScoreChanged(int playerScore, int enemyScore)
    {
        playerScoreLabel.text = playerScore.ToString();
        enemyScoreLabel.text = enemyScore.ToString();
    }
}
