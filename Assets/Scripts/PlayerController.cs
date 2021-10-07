using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    
    private HoldsDiscBehaviour holdsDiscBehaviour;
    private Vector3 startPosition;
    
    private const float VerticalRange = 4.5f;
    private const float HorizontalRange = 3f;
    private const float RotationSpeed = 8f;
    
    private void Start()
    {
        holdsDiscBehaviour = GetComponent<HoldsDiscBehaviour>();
        startPosition = transform.position;
    }
    
    private void FixedUpdate()
    {
        LookAtMouse();
        MovePlayer();
        FireDisc();
    }
    
    private void LookAtMouse()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(mouseRay, out RaycastHit raycastHit, 100))
        {
            Vector3 direction = (raycastHit.point - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Euler(0, Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * RotationSpeed).eulerAngles.y, 0);
        }
    }

    private void MovePlayer()
    {
        Vector3 currentPosition = transform.position;
        float horizontalAxis = 0;
        float verticalAxis = 0;
        
        if (transform.position.x < startPosition.x - HorizontalRange)
        {
            transform.position = new Vector3(startPosition.x - HorizontalRange, currentPosition.y, currentPosition.z);
        } else if (transform.position.x > startPosition.x + HorizontalRange)
        {
            transform.position = new Vector3(startPosition.x + HorizontalRange, currentPosition.y, currentPosition.z);
        } else
        {
            horizontalAxis = Input.GetAxis("Horizontal");
        }

        if (transform.position.z < startPosition.z - VerticalRange)
        {
            transform.position = new Vector3(currentPosition.x, currentPosition.y, startPosition.z - VerticalRange);
        } else if (transform.position.z > startPosition.z + VerticalRange)
        {
            transform.position = new Vector3(currentPosition.x, currentPosition.y, startPosition.z + VerticalRange);
        } else
        {
            verticalAxis = Input.GetAxis("Vertical");
        }
        
        Vector3 movementVector = new Vector3(horizontalAxis, 0, verticalAxis);
        transform.Translate(speed * Time.deltaTime * movementVector, Space.World);
    }

    private void FireDisc()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            holdsDiscBehaviour.FireDisc();
        }
    }
}
