using UnityEngine;

namespace UI
{
    public class ScoreUIController : MonoBehaviour
    {
        [SerializeField] private FadeInLabel playerScoreLabel, enemyScoreLabel;
        [SerializeField] private SetCounter playerSetCounter, enemySetCounter;

        private void Start()
        {
            ScoreManager.RedScore.OnDataChanged +=
                scoreData => UpdateScore(playerScoreLabel, playerSetCounter, scoreData);
            ScoreManager.BlueScore.OnDataChanged +=
                scoreData => UpdateScore(enemyScoreLabel, enemySetCounter, scoreData);
        }

        private void UpdateScore(FadeInLabel scoreLabel, SetCounter setCounter, ScoreManager.ScoreData score)
        {
            if (string.IsNullOrEmpty(scoreLabel.Text) || int.TryParse(scoreLabel.Text, out int oldScore) && oldScore != score.Points)
            {
                scoreLabel.FadeOut(() => scoreLabel.FadeIn(score.Points.ToString()));
            }

            if (setCounter.SetCount != score.Sets)
            {
                setCounter.SetCount = score.Sets;
            }
        }
    }
}