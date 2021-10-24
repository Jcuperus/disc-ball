using Helpers;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CountdownUIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text countdownLabel;

        private GameObject panel;
        
        private void Awake()
        {
            panel = transform.GetChild(0).gameObject;
            panel.SetActive(false);
            
            CountdownHelper.OnCountChanged += UpdateCounter;
        }

        private void UpdateCounter(int count)
        {
            if (count <= 0) panel.SetActive(false);
            else if (!panel.activeSelf) panel.SetActive(true);
            
            countdownLabel.text = count.ToString();
        }
    }
}

