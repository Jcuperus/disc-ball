using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class HorizontalCharacterController : MonoBehaviour
{
    public struct CollisionData
    {
        public RaycastHit RaycastHit;
        public bool HasCollision;
    }

    public CollisionData CollisionInfo;
    
    [SerializeField] private LayerMask collisionMask;

    private float radius;
    
    private void Awake()
    {
        radius = GetComponent<SphereCollider>().radius;
    }
    
    public void Move(Vector3 velocity)
    {
        CheckCollisions(ref velocity);
        
        transform.Translate(velocity);
    }

    private void CheckCollisions(ref Vector3 velocity)
    {
        Vector3 direction = velocity.normalized;
        Vector3 rayOrigin = transform.position + direction * radius;
        
        if (Physics.Raycast(rayOrigin, direction, out RaycastHit hitInfo, velocity.magnitude, collisionMask.value))
        {
            if (hitInfo.distance > radius)
            {
                velocity = direction * hitInfo.distance;
            }
            else
            {
                velocity = Vector3.zero;
            }

            CollisionInfo.RaycastHit = hitInfo;
            CollisionInfo.HasCollision = true;
        }
        else
        {
            CollisionInfo.HasCollision = false;
        }
    }
}