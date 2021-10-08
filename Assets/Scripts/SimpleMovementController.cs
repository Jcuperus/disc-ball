using Helpers;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SimpleMovementController : MonoBehaviour
{
    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private float castYOffset;

    public struct CollisionData
    {
        public RaycastHit RaycastHit;
        public bool HasCollision;
    }
    
    public CollisionData CollisionInfo;
    
    private float radius;

    private void Awake()
    {
        radius = GetComponent<Collider>().bounds.extents.x;
    }
    
    public void Move(Vector3 velocity, Space space = Space.Self)
    {
        CheckCollisions(ref velocity);
        
        transform.Translate(velocity, space);
    }

    public void DisableMask(LayerMask mask, float delay)
    {
        int previousMask = collisionMask.value;
        collisionMask.value -= mask.value;
        
        this.DelayedAction(() =>
        {
            collisionMask.value = previousMask;
        }, delay);
    }

    private void CheckCollisions(ref Vector3 velocity)
    {
        Vector3 direction = velocity.normalized;
        Vector3 raycastOrigin = transform.position + Vector3.up * castYOffset;
        float rayLength = velocity.magnitude;

        RaycastHit[] raycastHits =
            Physics.SphereCastAll(raycastOrigin, radius, direction, rayLength, collisionMask.value);

        CollisionInfo.HasCollision = false;

        if (raycastHits.Length > 0)
        {
            foreach (RaycastHit hitInfo in raycastHits)
            {
                if (hitInfo.collider.isTrigger || hitInfo.distance >= rayLength) continue;
                
                rayLength = hitInfo.distance;
                
                if (rayLength > radius)
                {
                    velocity = direction * rayLength;
                }
                else
                {
                    velocity = Vector3.zero;
                }

                CollisionInfo.RaycastHit = hitInfo;
                CollisionInfo.HasCollision = true;
            }
        }
    }
}