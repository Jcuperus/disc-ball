using UnityEngine;

public class HoldsDiscBehaviour : MonoBehaviour
{
    private static readonly int HoldsDiskTrigger = Animator.StringToHash("holdsDisk");

    private Animator animator;
    private DiscBehaviour disc;

    public bool hasDisc;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Disc"))
        {
            disc = other.gameObject.GetComponent<DiscBehaviour>();
            disc.SetFollow(gameObject);
            hasDisc = true;
            animator.SetBool(HoldsDiskTrigger, hasDisc);
        }
    }

    public void FireDisc()
    {
        if (hasDisc)
        {
            disc.LaunchDiscFromParent();
            hasDisc = false;
            animator.SetBool(HoldsDiskTrigger, hasDisc);
        }
    }
}
