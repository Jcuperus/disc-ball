using UnityEngine;

public class HoldsDiscBehaviour : MonoBehaviour
{
    public DiscBehaviour Disc
    {
        get => disc;
        set
        {
            disc = value;
            animator.SetBool(HoldsDiskTrigger, HasDisc);
        }
    }

    public bool HasDisc => Disc != null;
    
    private Animator animator;
    private DiscBehaviour disc;
    
    private static readonly int HoldsDiskTrigger = Animator.StringToHash("holdsDisk");

    public void FireDisc()
    {
        if (!HasDisc) return;

        Disc.LaunchDiscFromParent();
        animator.SetBool(HoldsDiskTrigger, HasDisc);
        Disc = null;
    }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Disc"))
    //     {
    //         disc = other.gameObject.GetComponent<DiscBehaviour>();
    //         disc.SetFollow(gameObject);
    //         hasDisc = true;
    //         animator.SetBool(HoldsDiskTrigger, hasDisc);
    //     }
    // }
}