using System;
using Helpers;
using Movement;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(SimpleMovementController), typeof(MultiClipSource))]
    public class DiscBehaviour : MonoBehaviour
    {
        [NonSerialized] public bool isBeingHeld;
        [NonSerialized] public Vector3 velocity;
    
        [SerializeField] private LayerMask actorMask;
        [SerializeField] private float speed = 18f;
        [SerializeField] private float actorCollisionDelay = 0.1f;

        private SimpleMovementController discController;
        private MultiClipSource audioSource;
    
        private const float YOffset = 1.23f;
        private const float ParentZOffset = 0.4f;

        public void LaunchDiscFromParent()
        {
            if (!isBeingHeld) return;

            Transform discTransform = transform;
            Vector3 direction = MathHelper.AngleToDirection(discTransform.parent.eulerAngles.y * Mathf.Deg2Rad);
            discTransform.SetParent(null);
            discTransform.eulerAngles = Vector3.zero;
            LaunchDisc(direction);
            discController.DisableMask(actorMask, actorCollisionDelay);
        }

        private void Awake()
        {
            discController = GetComponent<SimpleMovementController>();
            audioSource = GetComponent<MultiClipSource>();
        }

        private void OnEnable()
        {
            LaunchDisc(velocity);
        }

        private void OnDisable()
        {
            velocity = Vector3.zero;
            isBeingHeld = false;
        }

        private void Update()
        {
            if (isBeingHeld) return;
        
            SimpleMovementController.CollisionInfo collisionInfo = discController.Move(speed * Time.deltaTime * velocity);

            if (collisionInfo.HasCollision)
            {
                OnDiscCollision(collisionInfo.RaycastHit);
            }
        }

        private void OnDiscCollision(RaycastHit hitInfo)
        {
            GameObject hitObject = hitInfo.collider.gameObject;
        
            if ((1 << hitObject.layer & actorMask.value) != 0)
            {
                if (hitObject.TryGetComponent(out HoldsDiscBehaviour holdsDiscBehaviour))
                {
                    FollowObject(hitObject);
                    holdsDiscBehaviour.SetDisc(this);
                    audioSource.Play();
                }
            }
            else
            {
                velocity = Vector3.Reflect(velocity, hitInfo.normal);
                audioSource.Play();
            }
        }

        private void LaunchDisc(Vector3 newVelocity)
        {
            velocity = newVelocity;
            isBeingHeld = false;
        }
    
        private void FollowObject(GameObject parentObject)
        {
            velocity = Vector3.zero;
            Transform discTransform = transform;
            discTransform.SetParent(parentObject.transform);
            discTransform.localPosition = new Vector3(0, YOffset, ParentZOffset);
            isBeingHeld = true;
        }
    }
}
