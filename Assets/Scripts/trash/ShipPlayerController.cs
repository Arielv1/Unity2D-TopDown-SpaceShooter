using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// X,Y axes rotate with the player
public class ShipPlayerController : MonoBehaviour
{
    //public float moveSpeed;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Vector2 lastMoveDirection;
    [SerializeField] private float force = 10.0f;
    [SerializeField] private float minimumSpeed = 0.2f;
    [SerializeField] private float maxSpeed = 5f;
    private float acceleration = 0f;
    [SerializeField] private float rotationSpeed = 180f;
    private float slowDownTimer;
    private float moveX = 0, moveY = 0;
    private Vector3 cursorPosition;
    private float rotAngle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        SetCursorPosition();
    }

    void FixedUpdate()
    {
        Move();
        ClampVelocity();
        Slow();
    }


    void SetCursorPosition()
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        cursorPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        Vector3 difference = cursorPosition - transform.position;
        rotAngle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotAngle);
    }

    void Slow()
    {
        if (Mathf.Abs(rb.velocity.x) > minimumSpeed)
        {
            if (rb.velocity.x > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x - Time.deltaTime, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x + Time.deltaTime, rb.velocity.y);
            }
        }

        if (Mathf.Abs(rb.velocity.y) > 0.1)
        {
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - Time.deltaTime * 2);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + Time.deltaTime * 2);
            }
        }
    }

    void ProcessInputs()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotAngle += rotationSpeed;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotAngle -= rotationSpeed;
        }
        transform.rotation = Quaternion.Euler(0f, 0f, rotAngle);
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        //moveDirection = new Vector2(moveX, moveY).normalized;
        moveDirection = new Vector2(moveY, -moveX).normalized;// <-- with addforce
    }

    private void Move()
    {

        Vector2 direction = transform.TransformDirection(moveDirection * Time.deltaTime * force);
        //     rb.AddForce(direction * force);
        rb.velocity += direction;

    }


    private void ClampVelocity()
    {
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed),
                                  Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed));
        Debug.Log(rb.velocity);
    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Astroid")
        {
            collision.gameObject.SetActive(false);
        }
    }*/
}
