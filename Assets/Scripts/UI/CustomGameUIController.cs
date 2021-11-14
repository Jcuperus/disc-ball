using Gameplay;
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

        private void Awake()
        {
            startButton.onClick.AddListener(OnStartClicked);
        }

        private void OnStartClicked()
        {
            string set = setDropdown.options[setDropdown.value].text;
            string game = gameDropdown.options[gameDropdown.value].text;
            
            if (int.TryParse(set, out int setPoints) && int.TryParse(game, out int gameSets))
            {
                var customGameConfig = ScriptableObject.CreateInstance<GameConfigurationData>();
                
                customGameConfig.setPoints = setPoints;
                customGameConfig.gameSets = gameSets;

                MenuManager.StartGame(customGameConfig);
            }
        }
    }
}