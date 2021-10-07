using Helpers;
using UnityEngine;

[RequireComponent(typeof(HorizontalCharacterController))]
public class DiscBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 18f;
    [SerializeField] private LayerMask actorMask;

    private HorizontalCharacterController discController;
    private Vector3 velocity;
    private bool isFollowing;
    
    private const float YOffset = 1.23f;
    private const float ParentZOffset = 0.4f;

    public void LaunchDiscFromParent()
    {
        if (!isFollowing) return;

        Transform discTransform = transform;
        Vector3 direction = MathHelper.GetAngleVector(discTransform.parent.eulerAngles.y);
        discTransform.SetParent(null);
        discTransform.eulerAngles = Vector3.zero;
        LaunchDisc(direction);
    }

    private void Awake()
    {
        discController = GetComponent<HorizontalCharacterController>();
    }

    private void Start()
    {
        LaunchDisc(Vector3.right);
    }

    private void Update()
    {
        if (isFollowing) return;
        
        discController.Move(speed * Time.deltaTime * velocity);

        if (discController.CollisionInfo.HasCollision)
        {
            OnDiscCollision(discController.CollisionInfo.RaycastHit);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            Debug.Log("Goal scored");
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
                holdsDiscBehaviour.Disc = this;
            }
        }
        else
        {
            velocity = Vector3.Reflect(velocity, hitInfo.normal);
        }
    }

    private void LaunchDisc(Vector3 newVelocity)
    {
        velocity = newVelocity;
        isFollowing = false;
    }
    
    private void FollowObject(GameObject parentObject)
    {
        velocity = Vector3.zero;
        Transform discTransform = transform;
        discTransform.SetParent(parentObject.transform);
        discTransform.localPosition = new Vector3(0, YOffset, ParentZOffset);
        isFollowing = true;
    }
}
