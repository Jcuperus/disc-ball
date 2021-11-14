using UnityEngine;

namespace Menu
{
    public class FollowDisc : MonoBehaviour
    {
        public GameObject subject;

        private void Update()
        {
            if (!subject) return;

            Vector3 lookDirection = subject.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(lookDirection.normalized, Vector3.forward);
        }
    }
}
