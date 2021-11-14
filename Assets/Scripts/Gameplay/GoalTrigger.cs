using System;
using Movement;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Collider))]
    public class GoalTrigger : MonoBehaviour, IMovementControllerTrigger
    {
        public Action OnGoalScored;

        private const string DiscTag = "Disc";
    
        public void OnTrigger(GameObject other)
        {
            if (other.CompareTag(DiscTag) && other.TryGetComponent(out DiscBehaviour disc) && !disc.isBeingHeld)
            {
                OnGoalScored?.Invoke();
            }
        }
    }
}