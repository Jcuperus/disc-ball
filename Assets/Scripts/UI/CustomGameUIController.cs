using Menu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CustomGameUIController : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown setDropdown, gameDropdown;
        [SerializeField] private Button startButton;

        private GameConfigurationData customGameConfig;
        
        private void Awake()
        {
            startButton.onClick.AddListener(OnStartClicked);
            customGameConfig = GameConfigurationManager.Instance.GameConfig;
            
            InitializeDropdownOptions();
        }
        
        private void InitializeDropdownOptions()
        {
            for (int i = 0; i < setDropdown.options.Count; i++)
            {
                if (int.TryParse(setDropdown.options[i].text, out int optionValue) && optionValue == customGameConfig.setPoints)
                {
                    setDropdown.value = i;
                }
            }
            
            for (int i = 0; i < gameDropdown.options.Count; i++)
            {
                if (int.TryParse(gameDropdown.options[i].text, out int optionValue) && optionValue == customGameConfig.gameSets)
                {
                    gameDropdown.value = i;
                }
            }
        }

        private void OnStartClicked()
        {
            string set = setDropdown.options[setDropdown.value].text;
            string game = gameDropdown.options[gameDropdown.value].text;
            
            if (int.TryParse(set, out int setPoints) && int.TryParse(game, out int gameSets))
            {
                customGameConfig.setPoints = setPoints;
                customGameConfig.gameSets = gameSets;

                MenuManager.StartGame(customGameConfig);
            }
        }
    }
}