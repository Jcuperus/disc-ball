using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    
    private HoldsDiscBehaviour _holdsDiscBehaviour;
    private Vector3 _startPosition;
    private float _verticalRange = 4.5f;
    private float _horizontalRange = 3f;
    private float _rotationSpeed = 8f;
    
    // Start is called before the first frame update
    void Start()
    {
        _holdsDiscBehaviour = GetComponent<HoldsDiscBehaviour>();
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LookAtMouse();
        MovePlayer();
        FireDisc();
    }
    
    void LookAtMouse()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(mouseRay, out RaycastHit raycastHit, 100))
        {
            Vector3 direction = (raycastHit.point - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Euler(0, Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed).eulerAngles.y, 0);
        }
    }

    void MovePlayer()
    {
        Vector3 currentPosition = transform.position;
        float horizontalAxis = 0;
        float verticalAxis = 0;
        
        if (transform.position.x < _startPosition.x - _horizontalRange)
        {
            transform.position = new Vector3(_startPosition.x - _horizontalRange, currentPosition.y, currentPosition.z);
        } else if (transform.position.x > _startPosition.x + _horizontalRange)
        {
            transform.position = new Vector3(_startPosition.x + _horizontalRange, currentPosition.y, currentPosition.z);
        } else
        {
            horizontalAxis = Input.GetAxis("Horizontal");
        }

        if (transform.position.z < _startPosition.z - _verticalRange)
        {
            transform.position = new Vector3(currentPosition.x, currentPosition.y, _startPosition.z - _verticalRange);
        } else if (transform.position.z > _startPosition.z + _verticalRange)
        {
            transform.position = new Vector3(currentPosition.x, currentPosition.y, _startPosition.z + _verticalRange);
        } else
        {
            verticalAxis = Input.GetAxis("Vertical");
        }
        
        Vector3 movementVector = new Vector3(horizontalAxis, 0, verticalAxis);
        transform.Translate(speed * Time.deltaTime * movementVector, Space.World);
    }

    void FireDisc()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _holdsDiscBehaviour.FireDisc();
        }
    }
}
