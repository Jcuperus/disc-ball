using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class DiscController : MonoBehaviour
{
    public event Action<RaycastHit> OnCollide;
    
    [SerializeField] private LayerMask collisionMask;

    private new SphereCollider collider;
    
    private void Awake()
    {
        collider = GetComponent<SphereCollider>();
    }
    
    public void Move(Vector3 velocity)
    {
        CheckCollisions(ref velocity);
        
        transform.Translate(velocity);
    }

    private void CheckCollisions(ref Vector3 velocity)
    {
        Vector3 direction = velocity.normalized;
        Vector3 rayOrigin = transform.position + direction * collider.radius;
        
        if (Physics.Raycast(rayOrigin, direction, out RaycastHit hitInfo, velocity.magnitude, collisionMask.value))
        {
            if (hitInfo.distance > collider.radius)
            {
                velocity = direction * hitInfo.distance;
            }
            else
            {
                velocity = Vector3.zero;
            }

            OnCollide?.Invoke(hitInfo);
        }
    }
}