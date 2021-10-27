using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    [SerializeField] private GameObject subject;
    
    private void Update()
    {
        transform.LookAt(subject.transform.position);
    }
}
