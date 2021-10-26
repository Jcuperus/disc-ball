using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class TextButton : MonoBehaviour
    {
        [SerializeField] private FontStyles selectedStyles;
        
        private TMP_Text label;
        
        private void Awake()
        {
            label = GetComponent<TMP_Text>();
        }

        public void Select()
        {
            label.fontStyle |= selectedStyles;
        }

        public void Deselect()
        {
            label.fontStyle &= ~selectedStyles;
        }
    }
}