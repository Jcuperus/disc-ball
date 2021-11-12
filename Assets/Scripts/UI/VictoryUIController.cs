using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class VictoryUIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text victoryLabel;
        [SerializeField] private string winMessage, loseMessage;

        private GameObject panel;
        
        public void Restart()
        {
            panel.SetActive(false);
            GameManager.Instance.StartGame();
        }

        public void Quit()
        {
            Application.Quit();
        }
        
        private void Start()
        {
            panel = transform.GetChild(0).gameObject;
            panel.SetActive(false);
            
            GameManager.Instance.OnGameEnd += OnGameEnd;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnGameEnd -= OnGameEnd;
        }

        private void OnGameEnd(bool redWins)
        {
            victoryLabel.text = redWins ? winMessage : loseMessage;
            panel.SetActive(true);
        }
    }
}