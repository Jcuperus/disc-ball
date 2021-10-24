using System.Collections;
using UnityEngine;

[RequireComponent(typeof(HoldsDiscBehaviour), typeof(SimpleMovementController))]
public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float minFireDelay = 0.5f, maxFireDelay = 1.5f;
    [SerializeField] private float fireRotationMin = 190f, fireRotationMax = 350f;
    
    private HoldsDiscBehaviour holdsDiscBehaviour;
    private SimpleMovementController movementController;
    private Transform discTransform;
    
    
    
    private void Start()
    {
        holdsDiscBehaviour = GetComponent<HoldsDiscBehaviour>();
        movementController = GetComponent<SimpleMovementController>();
        discTransform = GameManager.Instance.DiscInstance.gameObject.transform;
        
        StartCoroutine(CheckHoldsDisk());
    }

    private void Update()
    {
        if (!holdsDiscBehaviour.HasDisc)
        {
            MoveTowardsDisc();
        }
    }

    private void FireInRandomDirection()
    {
        transform.rotation = Quaternion.Euler(0, Random.Range(fireRotationMin, fireRotationMax), 0);
        holdsDiscBehaviour.FireDisc();
    }

    private void MoveTowardsDisc()
    {
        Vector3 direction = Vector3.Scale(discTransform.position - transform.position, new Vector3(0, 0, 1f));
        
        movementController.Move(speed * Time.deltaTime * direction.normalized, Space.World);
    }
    
    private IEnumerator CheckHoldsDisk()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (holdsDiscBehaviour.HasDisc)
            {
                yield return new WaitForSeconds(Random.Range(minFireDelay, maxFireDelay));
                FireInRandomDirection();
            }
        }
    }
}
