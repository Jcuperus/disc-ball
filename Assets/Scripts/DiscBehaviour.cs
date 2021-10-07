using UnityEngine;

public class DiscBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 18f;

    private GameObject lastLaunchPlayer;
    private new Rigidbody rigidbody;
    private bool isFollowing;
    
    private const float YOffset = 1.23f;
    private const float ParentZOffset = 0.4f;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        LaunchDisc(Vector3.back);
    }

    private void LaunchDisc(Vector3 direction)
    {
        rigidbody.AddForce(speed * direction, ForceMode.Impulse);
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
        rigidbody.velocity = Vector3.zero;
        Transform discTransform = transform;
        discTransform.SetParent(parentObject.transform);
        discTransform.localPosition = new Vector3(0, YOffset, ParentZOffset);
        isFollowing = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            Debug.Log("Goal by " + lastLaunchPlayer.name);
        }
    }
}
