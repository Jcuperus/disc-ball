using Helpers;
using UnityEngine;

namespace MovementControllers
{
    [RequireComponent(typeof(SphereCollider))]
    public class SimpleMovementController : MonoBehaviour
    {
        [SerializeField] private LayerMask collisionMask;
        
        public struct CollisionData
        {
            public RaycastHit RaycastHit;
            public bool HasCollision;
        }
    
        public CollisionData CollisionInfo;
        
        protected float Radius;

        protected virtual void Awake()
        {
            Radius = GetComponent<SphereCollider>().radius;
        }
        
        public void Move(Vector3 velocity)
        {
            CheckCollisions(ref velocity);
            
            transform.Translate(velocity);
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
            float rayLength = velocity.magnitude;

            RaycastHit[] raycastHits =
                Physics.SphereCastAll(transform.position, Radius, direction, rayLength, collisionMask.value);
            
            if (raycastHits.Length > 0)
            {
                foreach (RaycastHit hitInfo in raycastHits)
                {
                    if (hitInfo.distance >= rayLength) continue;

                    rayLength = hitInfo.distance;
                    
                    if (rayLength > Radius)
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
            else
            {
                CollisionInfo.HasCollision = false;
            }
        }
    }
}