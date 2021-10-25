using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class TextButton : MonoBehaviour
    {
        private TMP_Text label;
        
        private void Awake()
        {
            label = GetComponent<TMP_Text>();
        }

        public void Select()
        {
            label.fontStyle |= FontStyles.Underline;
        }

        public void Deselect()
        {
            label.fontStyle &= ~FontStyles.Underline;
        }
    }
}