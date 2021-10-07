using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class EnemyBehaviour : MonoBehaviour
{
    private HoldsDiscBehaviour _holdsDiscBehaviour;
    private GameObject _disc;
    private float _minFireDelay = 0.5f;
    private float _maxFireDelay = 1.5f;
    private float _rotationMin = 180f;
    private float _rotationMax = 360f;

    public float speed = 2f;
    
    void Start()
    {
        _holdsDiscBehaviour = GetComponent<HoldsDiscBehaviour>();
        _disc = GameObject.FindWithTag("Disc");
        StartCoroutine(CheckHoldsDisk());
    }

    void FixedUpdate()
    {
        if (!_holdsDiscBehaviour.hasDisc)
        {
            MoveTowardsDisc();
        }
    }

    void FireInRandomDirection()
    {
        transform.rotation = Quaternion.Euler(0, Random.Range(_rotationMin, _rotationMax), 0);
        _holdsDiscBehaviour.FireDisc();
    }

    void MoveTowardsDisc()
    {
        Vector3 direction = Vector3.Scale(_disc.transform.position - transform.position, new Vector3(0, 0, 1));
        transform.Translate(speed * Time.deltaTime * direction.normalized, Space.World);
    }
    
    IEnumerator CheckHoldsDisk()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (_holdsDiscBehaviour.hasDisc)
            {
                yield return new WaitForSeconds(Random.Range(_minFireDelay, _maxFireDelay));
                FireInRandomDirection();
            }
        }
    }
}
