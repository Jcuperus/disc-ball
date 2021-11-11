using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenuUIController : MonoBehaviour
    {
        [SerializeField] private SceneAsset gameScene;
        [SerializeField] private TMP_Text startButtonLabel;

        private const string LoadingText = "Loading...";
        
        public void StartGame()
        {
            SceneManager.LoadSceneAsync(gameScene.name);
            startButtonLabel.text = LoadingText;
        }

        public void Quit() => Application.Quit();
    }
}

