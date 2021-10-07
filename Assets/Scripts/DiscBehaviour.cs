using System;
using UnityEngine;

public class DiscBehaviour : MonoBehaviour
{
    public float speed = 18f;

    private GameObject _lastLaunchPlayer;
    private Rigidbody _rigidbody;
    private bool _isFollowing;
    private float _yOffset = 1.23f;
    private float _parentZOffset = 0.4f;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        LaunchDisc(Vector3.back);
    }

    public void LaunchDisc(Vector3 direction)
    {
        _rigidbody.AddForce(speed * direction, ForceMode.Impulse);
        _isFollowing = false;
    }

    public void LaunchDiscFromParent()
    {
        if (_isFollowing)
        {
            Quaternion parentRotation = transform.parent.rotation;
            transform.SetParent(null);
            Vector3 direction = parentRotation * Vector3.forward;
            LaunchDisc(direction);
            _isFollowing = false;
        }
    }

    public void SetFollow(GameObject parentObject)
    {
        _lastLaunchPlayer = parentObject;
        _rigidbody.velocity = Vector3.zero;
        Transform discTransform = transform;
        discTransform.SetParent(parentObject.transform);
        discTransform.localPosition = new Vector3(0, _yOffset, _parentZOffset);
        _isFollowing = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            Debug.Log("Goal by " + _lastLaunchPlayer.name);
        }
    }
}
