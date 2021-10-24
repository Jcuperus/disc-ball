using System;
using Helpers;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(Animator), typeof(TMP_Text))]
    public class FadeInLabel : MonoBehaviour
    {
        public Action OnFadeOutFinished;
        
        private Animator animator;
        private TMP_Text label;
        
        private static readonly int FadeInTrigger = Animator.StringToHash("FadeIn");
        private static readonly int FadeOutTrigger = Animator.StringToHash("FadeOut");
        
        private void Awake()
        {
            animator = GetComponent<Animator>();
            label = GetComponent<TMP_Text>();
        }

        public void FadeIn(string newText)
        {
            label.text = newText;
            animator.SetTrigger(FadeInTrigger);
        }

        public void FadeOut()
        {
            animator.SetTrigger(FadeOutTrigger);
            this.DelayedAction(OnFadeOutFinished, GetCurrentAnimationDuration());
        }

        private float GetCurrentAnimationDuration()
        {
            return animator.GetCurrentAnimatorStateInfo(0).length;
        }
    }
}