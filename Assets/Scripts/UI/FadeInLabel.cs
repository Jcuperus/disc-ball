using System;
using Helpers;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(Animator), typeof(TMP_Text))]
    public class FadeInLabel : MonoBehaviour
    {
        public string Text => label.text;
        
        private Animator animator;
        private TMP_Text label;

        private Coroutine currentCoroutine;
        
        private static readonly int FadeInTrigger = Animator.StringToHash("FadeIn");
        private static readonly int FadeOutTrigger = Animator.StringToHash("FadeOut");
        
        public void FadeIn(string newText)
        {
            label.text = newText;
            animator.SetTrigger(FadeInTrigger);
        }

        public void FadeOut()
        {
            animator.SetTrigger(FadeOutTrigger);
        }
        
        public void FadeOut(Action callback)
        {
            FadeOut();
            currentCoroutine = this.DelayedAction(callback, GetCurrentAnimationDuration());
        }
        
        private void OnEnable()
        {
            animator = GetComponent<Animator>();
            label = GetComponent<TMP_Text>();
        }

        private void OnDisable()
        {
            if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        }

        private float GetCurrentAnimationDuration()
        {
            return animator.GetCurrentAnimatorStateInfo(0).length;
        }
    }
}