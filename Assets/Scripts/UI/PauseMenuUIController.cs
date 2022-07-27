using Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class PauseMenuUIController : MonoBehaviour
    {
        [SerializeField] private Button resumeButton, returnButton;
        private GameObject panel;

        private const string MenuSceneName = "MainMenuScene";
        
        private void Awake()
        {
            resumeButton.onClick.AddListener(OnResumeClicked);
            returnButton.onClick.AddListener(OnReturnClicked);
        }

        private void Start()
        {
            panel = transform.GetChild(0).gameObject;
            panel.SetActive(false);

            StateManager.OnStateChanged += ShowPauseMenu;
        }

        private void OnDisable()
        {
            StateManager.OnStateChanged -= ShowPauseMenu;
        }

        private void ShowPauseMenu(StateManager.GameState gameState)
        {
            panel.SetActive(gameState == StateManager.GameState.Paused);
        }

        private void OnResumeClicked()
        {
            StateManager.TogglePause();
        }

        private void OnReturnClicked()
        {
            StateManager.TogglePause();
            StateManager.State = StateManager.GameState.GameEnded;
            
            SceneManager.LoadScene(MenuSceneName);
        }
    }
}