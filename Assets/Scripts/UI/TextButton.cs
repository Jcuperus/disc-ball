using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class TextButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private FontStyles selectedStyles;
        
        private TMP_Text label;
        
        private void Awake()
        {
            label = GetComponent<TMP_Text>();
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