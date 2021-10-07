using System;
using UnityEngine;

[RequireComponent(typeof(DiscController))]
public class DiscBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 18f;
    [SerializeField] private LayerMask actorMask;

    private DiscController discController;
    private GameObject lastLaunchPlayer;
    private Vector3 velocity;
    private bool isFollowing;
    
    private const float YOffset = 1.23f;
    private const float ParentZOffset = 0.4f;

    private void Awake()
    {
        discController = GetComponent<DiscController>();
        discController.OnCollide += OnDiscCollision;
    }

    private void Start()
    {
        LaunchDisc(Vector3.left);
    }

    private void Update()
    {
        if (isFollowing) return;
        
        discController.Move(speed * Time.deltaTime * velocity);
    }

    private void OnDiscCollision(RaycastHit hitInfo)
    {
        if ((1 << hitInfo.collider.gameObject.layer & actorMask.value) != 0)
        {
            Debug.Log("actor hit");
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

    public void LaunchDiscFromParent()
    {
        if (isFollowing)
        {
            Quaternion parentRotation = transform.parent.rotation;
            transform.SetParent(null);
            Vector3 direction = parentRotation * Vector3.forward;
            LaunchDisc(direction);
            isFollowing = false;
        }
    }

    public void SetFollow(GameObject parentObject)
    {
        lastLaunchPlayer = parentObject;
        Transform discTransform = transform;
        discTransform.SetParent(parentObject.transform);
        discTransform.localPosition = new Vector3(0, YOffset, ParentZOffset);
        isFollowing = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            Debug.Log("Goal scored");
        }
    }
}
