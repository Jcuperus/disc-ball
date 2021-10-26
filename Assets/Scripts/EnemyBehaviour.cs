using System.Collections;
using Helpers;
using UnityEngine;

[RequireComponent(typeof(HoldsDiscBehaviour), typeof(SimpleMovementController))]
public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 2f, rotationSpeed = 8f;
    [SerializeField] private float minMoveAmount = 0.01f;
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
            LookAtDisc();
        }
    }

    private void FireInRandomDirection()
    {
        transform.rotation = Quaternion.Euler(0f, Random.Range(fireRotationMin, fireRotationMax), 0f);
        holdsDiscBehaviour.FireDisc();
    }

    private void MoveTowardsDisc()
    {
        Vector3 direction = Vector3.Scale(discTransform.position - transform.position, new Vector3(0, 0, 1f));

        if (Mathf.Abs(direction.z) < minMoveAmount) direction.z = 0f;

        movementController.Move(speed * Time.deltaTime * direction.normalized, Space.World);
    }

    private void LookAtDisc()
    {
        Vector3 direction = (discTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.Euler(0f, MathHelper.GetVectorAngle(direction), 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
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
