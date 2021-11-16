using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(MultiClipSource))]
    public class HoldsDiscBehaviour : MonoBehaviour
    {
        public bool HasDisc { get; private set; }
    
        private Animator animator;
        private MultiClipSource audioSource;
        private DiscBehaviour disc;
    
        private static readonly int HoldsDiskTrigger = Animator.StringToHash("holdsDisk");

        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
            audioSource = GetComponent<MultiClipSource>();
        }
    
        public void FireDisc()
        {
            if (!HasDisc) return;

            disc.LaunchDiscFromParent();
            audioSource.Play();
            SetDisc(null);
        }

        public void SetDisc(DiscBehaviour newDisc)
        {
            disc = newDisc;
            HasDisc = newDisc != null;
            animator.SetBool(HoldsDiskTrigger, HasDisc);
        }
    }
}