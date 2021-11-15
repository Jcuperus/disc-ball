using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(TMP_Text), typeof(MultiClipSource))]
    public class TextButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Button button;
        [SerializeField] private FontStyles selectedStyles;
        
        private TMP_Text label;
        private MultiClipSource audioSource;
        
        private void Awake()
        {
            label = GetComponent<TMP_Text>();
            audioSource = GetComponent<MultiClipSource>();
            
            button.onClick.AddListener(audioSource.Play);
        }

        public void OnPointerEnter(PointerEventData eventData) => Select();
        public void OnPointerExit(PointerEventData eventData) => Deselect();

        private void Select()
        {
            label.fontStyle |= selectedStyles;
        }

        private void Deselect()
        {
            label.fontStyle &= ~selectedStyles;
        }
    }
}