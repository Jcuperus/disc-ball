using System;
using Gameplay;
using UnityEngine;

namespace UI
{
    public class ScoreUIController : MonoBehaviour
    {
        [SerializeField] private FadeInLabel playerScoreLabel, enemyScoreLabel;
        [SerializeField] private SetCounter playerSetCounter, enemySetCounter;

        private void Start()
        {
            ScoreManager.RedScore.OnDataChanged += UpdateRedScore;
            ScoreManager.BlueScore.OnDataChanged += UpdateBlueScore;
        }

        private void OnDisable()
        {
            ScoreManager.RedScore.OnDataChanged -= UpdateRedScore;
            ScoreManager.BlueScore.OnDataChanged -= UpdateBlueScore;
        }

        private void UpdateRedScore(ScoreManager.ScoreData scoreData) =>
            UpdateScore(playerScoreLabel, playerSetCounter, scoreData);

        private void UpdateBlueScore(ScoreManager.ScoreData scoreData) =>
            UpdateScore(enemyScoreLabel, enemySetCounter, scoreData);

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