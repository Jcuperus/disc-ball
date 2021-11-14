using System.Collections.Generic;
using Helpers;
using UnityEngine;

namespace Movement
{
    public class SimpleMovementController : MonoBehaviour
    {
        [SerializeField] private LayerMask collisionMask;
        [SerializeField] private float radius;
        [SerializeField] private float castYOffset;
        [SerializeField] private int rayAmount = 7;

        private const float ArcLength = Mathf.PI;

        public struct CollisionInfo
        {
            public RaycastHit RaycastHit;
            public bool HasCollision;
        }
    
        private CollisionInfo collisionInfo;

        private void OnDisable()
        {
            collisionInfo.HasCollision = false;
        }

        private void OnDrawGizmos()
        {
            const int circleResolution = 16;

            var points = new Vector3[circleResolution];
            Vector3 position = transform.position + Vector3.up * castYOffset;
            for (int i = 0; i < points.Length; i++)
            {
                float t = (float)i / circleResolution;
                points[i] = position + MathHelper.AngleToDirection(t * MathHelper.Tau) * radius;
            }
        
            for (int i = 0; i < points.Length; i++)
            {
                int nextIndex = (i + 1) % points.Length;
                Gizmos.DrawLine(points[i], points[nextIndex]);
            }
        }

        public CollisionInfo Move(Vector3 velocity, Space space = Space.Self)
        {
            CheckCollisions(ref velocity);
        
            transform.Translate(velocity, space);

            return collisionInfo;
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

            RaycastHit[] raycastHits = CastRays(velocity);
        
            collisionInfo.HasCollision = false;

            if (raycastHits.Length > 0)
            {
                foreach (RaycastHit hitInfo in raycastHits)
                {
                    if (hitInfo.collider.isTrigger && hitInfo.collider.TryGetComponent(out IMovementControllerTrigger trigger))
                    {
                        trigger.OnTrigger(gameObject);
                    }
                    else if (hitInfo.distance < rayLength)
                    {
                        rayLength = hitInfo.distance;
                
                        if (rayLength > radius)
                        {
                            velocity = direction * rayLength;
                        }
                        else
                        {
                            velocity = Vector3.zero;
                        }

                        collisionInfo.RaycastHit = hitInfo;
                        collisionInfo.HasCollision = true;
                    }
                }
            }
        }

        private RaycastHit[] CastRays(Vector3 velocity)
        {
            var hits = new List<RaycastHit>();

            Vector3 origin = transform.position + Vector3.up * castYOffset;
            float rayLength = velocity.magnitude;
            float velocityAngle = MathHelper.DirectionToAngle(velocity.normalized);
            float startAngle = velocityAngle - ArcLength / 2;
        
            for (int i = 0; i < rayAmount; i++)
            {
                float t = (float)i / (rayAmount - 1);
                float rayAngle = startAngle + t * ArcLength;
                Vector3 sphereOffset = MathHelper.AngleToDirection(rayAngle) * radius;
                Vector3 rayOrigin = origin + sphereOffset;
            
                if (Physics.Raycast(rayOrigin, velocity.normalized, out RaycastHit hitInfo, rayLength, collisionMask.value))
                {
                    hits.Add(hitInfo);
                }
            }
        
            return hits.ToArray();
        }
    }
}