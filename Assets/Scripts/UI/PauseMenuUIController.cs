using UnityEngine;

namespace UI
{
    public class PauseMenuUIController : MonoBehaviour
    {
        private GameObject panel;
    
        private void Start()
        {
            panel = transform.GetChild(0).gameObject;
            panel.SetActive(false);

            StateManager.OnStateChanged += ShowPauseMenu;
        }

        private void ShowPauseMenu(StateManager.GameState gameState)
        {
            panel.SetActive(gameState == StateManager.GameState.Paused);
        }

        public void OnResumeClicked()
        {
            StateManager.TogglePause();
        }

        public void OnQuitClicked()
        {
            Application.Quit();
        }
    }
}