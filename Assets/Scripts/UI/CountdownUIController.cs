using Helpers;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(MultiClipSource))]
    public class CountdownUIController : MonoBehaviour
    {
        [SerializeField] private FadeInLabel countdownLabel;

        private GameObject panel;
        private MultiClipSource audioSource;
        private int count;

        private void OnEnable()
        {
            audioSource = GetComponent<MultiClipSource>();
            
            panel = transform.GetChild(0).gameObject;
            panel.SetActive(false);
            
            CountdownHelper.OnCountChanged += UpdateCounter;
        }

        private void OnDisable()
        {
            CountdownHelper.OnCountChanged -= UpdateCounter;
        }

        private void UpdateCounter(int newCount)
        {
            if (!panel.activeSelf) panel.SetActive(true);
            
            count = newCount;
            countdownLabel.FadeOut(OnLabelFadeOut);
        }
        
        private void OnLabelFadeOut()
        {
            if (count <= 0) panel.SetActive(false);
            
            audioSource.Play();
            countdownLabel.FadeIn(count.ToString());
        }
    }
}

