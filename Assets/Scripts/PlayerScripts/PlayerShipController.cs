using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// absolute X,Y axes
public class PlayerShipController : MonoBehaviour
{   
    //private Vector3 moveDirection;
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    [SerializeField] private float accelForce = 1.5f;
    [SerializeField] private float maxSpeed = 5f;

    [SerializeField] private GameObject[] weaponPrefabs;
    [SerializeField] private GameObject[] firePoints;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveInputs();
    }

    void FixedUpdate()
    {
        RotateShipToCursor();
        Move();
        ClampVelocity();
      
    }
    void RotateShipToCursor()
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        Vector3 difference = cursorPosition - transform.position;
        float rotAngle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // (-180,180)
        transform.rotation = Quaternion.Euler(0f, 0f, rotAngle);
    }

    void MoveInputs()
    {
        //moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0).normalized;
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
    }

    void Move()
    {
        rb.velocity += moveDirection * Time.deltaTime * accelForce;         
    }

    void ClampVelocity()
    {
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed),
                                  Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed));
    }

    
}
