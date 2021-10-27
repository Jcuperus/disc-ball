using UnityEngine;

namespace UI
{
    public class SetCounter : MonoBehaviour
    {
        [SerializeField] private RectTransform nodePrefab;

        public int SetCount
        {
            get => setCount;
            set
            {
                if (value == setCount) return;
                
                setCount = value;
                UpdateCounter();
            }
        }

        private int setCount;
        private int setAmount;
        
        private RectTransform rectTransform;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            setAmount = GameManager.Instance.gameConfiguration.gameSets;
        }

        private void UpdateCounter()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }

            float nodeHeight = rectTransform.rect.height / setAmount;
            
            for (int i = 0; i < SetCount; i++)
            {
                RectTransform rectTransformInstance = Instantiate(nodePrefab, transform, false);
                rectTransformInstance.sizeDelta = new Vector2(rectTransformInstance.rect.width, nodeHeight);
            }
        }
    }
}

