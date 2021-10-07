using UnityEngine;

public class HoldsDiscBehaviour : MonoBehaviour
{
    public bool HasDisc { get; private set; }
    
    private Animator animator;
    private DiscBehaviour disc;
    
    private static readonly int HoldsDiskTrigger = Animator.StringToHash("holdsDisk");

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    
    public void FireDisc()
    {
        if (!HasDisc) return;

        disc.LaunchDiscFromParent();
        SetDisc(null);
    }

    public void SetDisc(DiscBehaviour newDisc)
    {
        disc = newDisc;
        HasDisc = newDisc != null;
        animator.SetBool(HoldsDiskTrigger, HasDisc);
    }
}