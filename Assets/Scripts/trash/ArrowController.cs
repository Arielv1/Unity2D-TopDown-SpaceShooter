using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float rotAngle = 0f;
    private Quaternion quaternionAngle;
    [SerializeField] private float rotationSpeed = 180f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float accelForce = 5f;
    private float z, y;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        InputHandler();

    }

    void FixedUpdate() 
    {
        Turn();
        Move();
    }


    void InputHandler()
    {   
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            z += rotationSpeed;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            z -= rotationSpeed;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            y -= accelForce;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            y += accelForce;
        }
    }

    void Move()
    {
        Vector3 pos = transform.position;
        Vector3 velocity = new Vector3(0,y * Time.deltaTime,0);
        pos += quaternionAngle * velocity;

        transform.position = pos;
    }

    void Turn()
    {
        quaternionAngle =  Quaternion.Euler(0, 0, z);
        transform.rotation = quaternionAngle;
    }

    private void ClampVelocity()
    {
        accelForce = Mathf.Clamp(accelForce, -maxSpeed, maxSpeed);
    }
   /* void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Astroid")
        {
            collision.gameObject.SetActive(false);
        }
    }*/
}
