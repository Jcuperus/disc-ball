using UnityEngine;

namespace Gameplay
{
    public class LookAtObject : MonoBehaviour
    {
        public GameObject subject;
    
        private void Update()
        {
            transform.LookAt(subject.transform.position);
        }
    }
}
