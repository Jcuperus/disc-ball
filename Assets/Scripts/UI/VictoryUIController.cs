using Gameplay;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class VictoryUIController : MonoBehaviour
    {
        [SerializeField] private Button restartButton, returnButton;
        [SerializeField] private TMP_Text victoryLabel;
        [SerializeField] private string winMessage, loseMessage;

        private GameObject panel;

        private const string MenuSceneName = "MainMenuScene";
        
        private void Start()
        {
            panel = transform.GetChild(0).gameObject;
            panel.SetActive(false);
            
            restartButton.onClick.AddListener(Restart);
            returnButton.onClick.AddListener(Return);
            
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

        private void Restart()
        {
            panel.SetActive(false);
            GameManager.Instance.StartGame();
        }

        private void Return()
        {
            SceneManager.LoadScene(MenuSceneName);
        }
    }
}