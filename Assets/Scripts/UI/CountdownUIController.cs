using Helpers;
using UnityEngine;

namespace UI
{
    public class CountdownUIController : MonoBehaviour
    {
        [SerializeField] private FadeInLabel countdownLabel;

        private GameObject panel;
        private int count;

        private void Awake()
        {
            panel = transform.GetChild(0).gameObject;
            panel.SetActive(false);
            
            CountdownHelper.OnCountChanged += UpdateCounter;
            countdownLabel.OnFadeOutFinished += OnLabelFadeOut;
        }

        private void UpdateCounter(int newCount)
        {
            if (!panel.activeSelf) panel.SetActive(true);

            count = newCount;
            countdownLabel.FadeOut();
        }
        
        private void OnLabelFadeOut()
        {
            if (count <= 0) panel.SetActive(false);
            
            countdownLabel.FadeIn(count.ToString());
        }
    }
}

