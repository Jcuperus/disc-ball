using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuUIController : MonoBehaviour
    {
        [SerializeField] private Button startButton, customButton, settingsButton, quitButton;
        [SerializeField] private TMP_Text startButtonLabel;
        [SerializeField] private GameObject customGamePanel;

        private const string LoadingText = "Loading...";

        private void Awake()
        {
            startButton.onClick.AddListener(OnStartClicked);
            customButton.onClick.AddListener(OnCustomGameClicked);
            settingsButton.onClick.AddListener(OnSettingsClicked);
            quitButton.onClick.AddListener(Application.Quit);
        }

        private void OnStartClicked()
        {
            GameConfigurationManager.Instance.GameConfig = GameConfigurationManager.Instance.DefaultConfig;
            MenuManager.StartGame();
            startButtonLabel.text = LoadingText;
        }

        private void OnSettingsClicked()
        {
            
        }

        private void OnCustomGameClicked()
        {
            customGamePanel.SetActive(!customGamePanel.activeSelf);
        }
    }
}

