using Helpers;
using Movement;
using UnityEngine;

[RequireComponent(typeof(HoldsDiscBehaviour), typeof(SimpleMovementController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f, rotationSpeed = 8f;
    
    private HoldsDiscBehaviour holdsDiscBehaviour;
    private SimpleMovementController movementController;
    
    private void Awake()
    {
        holdsDiscBehaviour = GetComponent<HoldsDiscBehaviour>();
        movementController = GetComponent<SimpleMovementController>();
    }
    
    private void Update()
    {
        LookAtMouse();
        MovePlayer();
        FireDisc();
    }

    private void LookAtMouse()
    {
        if (!Camera.main) return;
        
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(mouseRay, out RaycastHit raycastHit, 100))
        {
            Vector3 direction = (raycastHit.point - transform.position).normalized;
            float directionDegrees = MathHelper.DirectionToAngle(direction) * Mathf.Rad2Deg;
            Quaternion lookRotation = Quaternion.Euler(0f, directionDegrees, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void MovePlayer()
    {
        var inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        movementController.Move(speed * Time.deltaTime * inputVector.normalized, Space.World);
    }

    private void FireDisc()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            holdsDiscBehaviour.FireDisc();
        }
    }
}
