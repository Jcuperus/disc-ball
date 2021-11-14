using UnityEngine;

namespace Movement
{
    public interface IMovementControllerTrigger
    {
        public void OnTrigger(GameObject other);
    }
}