using System.Collections;
using Helpers;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CountdownUIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text countdownLabel;
        [SerializeField] private Animator labelAnimator;

        private GameObject panel;

        private static readonly int FadeInTrigger = Animator.StringToHash("FadeIn");
        private static readonly int FadeOutTrigger = Animator.StringToHash("FadeOut");


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

            StartCoroutine(LabelFadeCoroutine(count));
        }

        private IEnumerator LabelFadeCoroutine(int newCount)
        {
            labelAnimator.SetTrigger(FadeOutTrigger);

            yield return new WaitForSeconds(labelAnimator.GetCurrentAnimatorStateInfo(0).length);
            
            countdownLabel.text = newCount.ToString();
            labelAnimator.SetTrigger(FadeInTrigger);
        }
    }
}

