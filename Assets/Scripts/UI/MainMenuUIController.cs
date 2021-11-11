using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenuUIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text startButtonLabel;
        [SerializeField] private DiscBehaviour discPrefab;

        private const string GameSceneName = "DiskBallScene";
        private const string LoadingText = "Loading...";
        
        public void StartGame()
        {
            SceneManager.LoadSceneAsync(GameSceneName);
            startButtonLabel.text = LoadingText;
        }

        public void Quit() => Application.Quit();

        private void Awake()
        {
            if (!Camera.main) return;
            
            Camera mainCamera = Camera.main;
            Vector3 cameraPosition = mainCamera.transform.position;

            float cameraDistance = Vector3.Distance(cameraPosition, Vector3.zero);
            float verticalLength = GetCameraOffsetAtDistance(cameraDistance, mainCamera.fieldOfView);
            float horizontalFOV = Camera.VerticalToHorizontalFieldOfView(mainCamera.fieldOfView, mainCamera.aspect);
            float horizontalLength = GetCameraOffsetAtDistance(cameraDistance, horizontalFOV);

            Vector3 rightColliderPosition = Vector3.right * horizontalLength + Vector3.right * 0.5f;
            Vector3 horizontalColliderSize = new Vector3(1f, 2f, verticalLength * 2);
            
            CreateMenuCollider(rightColliderPosition, horizontalColliderSize);
            CreateMenuCollider(-rightColliderPosition, horizontalColliderSize);

            Vector3 forwardColliderPosition = Vector3.forward * verticalLength + Vector3.forward * 0.5f;
            Vector3 verticalColliderSize = new Vector3(horizontalLength * 2, 2f, 1f);
            
            CreateMenuCollider(forwardColliderPosition, verticalColliderSize);
            CreateMenuCollider(-forwardColliderPosition, verticalColliderSize);
        }

        private void Start()
        {
            DiscBehaviour disc = Instantiate(discPrefab);
            disc.velocity = new Vector3(1, 0, 1).normalized;
        }

        private float GetCameraOffsetAtDistance(float distance, float fov)
        {
            float adjacentAngle = fov / 2 * Mathf.Deg2Rad;
            float oppositeAngle = Mathf.PI - adjacentAngle - Mathf.PI / 2;
            float adjacentLength = distance / Mathf.Sin(oppositeAngle) * Mathf.Sin(adjacentAngle);
            
            return adjacentLength;
        }

        private void CreateMenuCollider(Vector3 center, Vector3 size)
        {
            var colliderObject = new GameObject("Menu Collider")
            {
                layer = LayerMask.NameToLayer("Obstacle")
            };
            var menuCollider = colliderObject.AddComponent<BoxCollider>();

            menuCollider.center = center;
            menuCollider.size = size;
        }
    }
}

