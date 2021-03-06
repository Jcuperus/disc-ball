using Helpers;
using Menu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuUIController : MonoBehaviour
    {
        [SerializeField] private Button startButton, customButton, settingsButton;
        [SerializeField] private TMP_Text startButtonLabel;
        [SerializeField] private GameObject customGamePanel, settingsPanel;

        private GameObject currentPanel;
        
        private const string LoadingText = "Loading...";

        private void Awake()
        {
            startButton.onClick.AddListener(OnStartClicked);
            customButton.onClick.AddListener(OnCustomGameClicked);
            settingsButton.onClick.AddListener(OnSettingsClicked);
        }

        private void OnStartClicked()
        {
            MenuManager.StartGame();
            startButtonLabel.text = LoadingText;
        }
        
        private void OnCustomGameClicked() => ToggleSidePanel(customGamePanel);
        
        private void OnSettingsClicked() => ToggleSidePanel(settingsPanel);

        private void ToggleSidePanel(GameObject panel)
        {
            if (currentPanel == panel)
            {
                currentPanel.SetActive(false);
                currentPanel = null;
            }
            else
            {
                if (currentPanel != null) currentPanel.SetActive(false);
                
                currentPanel = panel;
                currentPanel.SetActive(true);
            }
        }
    }
}

