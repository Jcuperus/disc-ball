using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class QuitButtonController : MonoBehaviour
    {
        private Button quitButton;

        private static bool CanQuit => Application.platform != RuntimePlatform.WebGLPlayer;
        
        private void Awake()
        {
            quitButton = GetComponent<Button>();
            quitButton.onClick.AddListener(OnQuitClicked);
            gameObject.SetActive(CanQuit);
        }

        private void OnQuitClicked()
        {
            if (!CanQuit) return;

            #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
            #else
            Application.Quit();
            #endif
        }
    }
}
