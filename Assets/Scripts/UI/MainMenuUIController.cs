using TMPro;
using UnityEngine;

namespace UI
{
    public class MainMenuUIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text startButtonLabel;
        [SerializeField] private GameObject customGamePanel;

        private const string LoadingText = "Loading...";
        
        public void OnStartClicked()
        {
            GameConfigurationManager.Instance.GameConfig = GameConfigurationManager.Instance.DefaultConfig;
            MenuManager.StartGame();
            startButtonLabel.text = LoadingText;
        }

        public void OnCustomGameClicked()
        {
            customGamePanel.SetActive(!customGamePanel.activeSelf);
        }

        public void OnQuitClicked() => Application.Quit();
    }
}

