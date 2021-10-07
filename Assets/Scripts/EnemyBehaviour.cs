using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    private HoldsDiscBehaviour holdsDiscBehaviour;
    private GameObject disc;
    
    private const float MinFireDelay = 0.5f;
    private const float MaxFireDelay = 1.5f;
    private const float RotationMin = 180f;
    private const float RotationMax = 360f;
    
    private void Start()
    {
        holdsDiscBehaviour = GetComponent<HoldsDiscBehaviour>();
        disc = GameObject.FindWithTag("Disc");
        StartCoroutine(CheckHoldsDisk());
    }

    private void FixedUpdate()
    {
        if (!holdsDiscBehaviour.HasDisc)
        {
            MoveTowardsDisc();
        }
    }

    private void FireInRandomDirection()
    {
        transform.rotation = Quaternion.Euler(0, Random.Range(RotationMin, RotationMax), 0);
        holdsDiscBehaviour.FireDisc();
    }

    private void MoveTowardsDisc()
    {
        Vector3 direction = Vector3.Scale(disc.transform.position - transform.position, new Vector3(0, 0, 1));
        transform.Translate(speed * Time.deltaTime * direction.normalized, Space.World);
    }
    
    private IEnumerator CheckHoldsDisk()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (holdsDiscBehaviour.HasDisc)
            {
                yield return new WaitForSeconds(Random.Range(MinFireDelay, MaxFireDelay));
                FireInRandomDirection();
            }
        }
    }
}
