using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GoalTrigger : MonoBehaviour
{
    public Action OnGoalScored;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Disc"))
        {
            OnGoalScored.Invoke();
        }
    }
}