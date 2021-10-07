using System;
using UnityEngine;

public class HoldsDiscBehaviour : MonoBehaviour
{
    private static readonly int HoldsDiskTrigger = Animator.StringToHash("holdsDisk");

    private Animator _animator;
    private DiscBehaviour _disc;

    public bool hasDisc;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Disc"))
        {
            _disc = other.gameObject.GetComponent<DiscBehaviour>();
            _disc.SetFollow(gameObject);
            hasDisc = true;
            _animator.SetBool(HoldsDiskTrigger, hasDisc);
        }
    }

    public void FireDisc()
    {
        if (hasDisc)
        {
            _disc.LaunchDiscFromParent();
            hasDisc = false;
            _animator.SetBool(HoldsDiskTrigger, hasDisc);
        }
    }
}
