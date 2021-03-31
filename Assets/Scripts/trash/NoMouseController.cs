using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMouseController : MonoBehaviour
{

    private Rigidbody2D rb;
    private float rotAngle = 0f;
    [SerializeField] private float rotationSpeed = 180f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float accelForce = 5f;

    private Vector3 moveDirection ;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        TurningInput();
        MovingInput();
    }

    void FixedUpdate() 
    {
        Turn();
        Move();
        ClampVelocity();
    }

    void TurningInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotAngle += rotationSpeed;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotAngle -= rotationSpeed;
        }

        
    }

    void MovingInput()
    {   
        moveDirection = new Vector3(Input.GetAxis("Horizontal"),
                                    Input.GetAxis("Vertical"), 
                                    0).normalized;
    }

    void Turn()
    {   
        transform.rotation = Quaternion.Euler(0f, 0f, rotAngle);
    }

    private void ClampVelocity()
    {
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), 
                                  Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed)); 
    }


    void Move()
    {
        transform.position += moveDirection * Time.deltaTime * accelForce;
    }



}

