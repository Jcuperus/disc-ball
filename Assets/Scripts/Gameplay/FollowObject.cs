using UnityEngine;

namespace Gameplay
{
    public class FollowObject : MonoBehaviour
    {
        [SerializeField] private Transform subjectTransform;

        private Vector3 initialOffset;
        private readonly Vector3 vectorXY = new Vector3(1f, 1f, 0);
        
        private void Awake()
        {
            initialOffset = Vector3.Scale(transform.position - subjectTransform.position, vectorXY);
        }

        private void Update()
        {
            Vector3 subjectPosition = subjectTransform.position;
            transform.position = Vector3.Scale(subjectPosition, vectorXY) + initialOffset;
            
            Vector3 lookDirection = subjectPosition - transform.position;
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }
}