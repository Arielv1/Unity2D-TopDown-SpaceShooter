using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// absolute X,Y axes
public class PlayerShipController : MonoBehaviour
{
    PlayerUIHandler playerUIHandler;

    //private Vector3 moveDirection;
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    [SerializeField] private float accelForce = 1.5f;
    [SerializeField] private float maxSpeed = 5f;

    [SerializeField] private GameObject[] weaponPrefabs;
    [SerializeField] private GameObject[] firePoints;


    [SerializeField] private float shipMaxHealth = 100f;
    private float shipCurrentHealth = 100f;
    [SerializeField] private float shipHealthIncrementAmount = 15f;

    [SerializeField] private float dashCooldownTime = 5f;
    [SerializeField] private float dashTimerCounterIncrement = 0.1f;
    private float dashTimer = 0f;
    [SerializeField] private float dashForce = 25f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerUIHandler = GetComponent<PlayerUIHandler>();
        SetUIBars();
    }

    void SetUIBars()
    {
        playerUIHandler.healthBar.SetMaxValue(shipMaxHealth);
        playerUIHandler.healthBar.SetValue(shipMaxHealth);
        playerUIHandler.dashBar.SetMaxValue(dashCooldownTime);
        playerUIHandler.dashBar.SetValue(dashCooldownTime);
    }
    // Update is called once per frame

    Vector3 getCursorPosition()
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        cursorPosition = new Vector3(cursorPosition.x, cursorPosition.y, 0f);
        return cursorPosition;
    }

    void Update()
    {
        MoveInputs();
        if (Input.GetKeyDown(KeyCode.X))
        {
            shipMaxHealth += shipHealthIncrementAmount;
            shipCurrentHealth += shipHealthIncrementAmount;
            playerUIHandler.UpdateBarMaxValue(playerUIHandler.healthBar, shipMaxHealth + shipHealthIncrementAmount, shipCurrentHealth + shipHealthIncrementAmount);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            shipCurrentHealth -= 10f;
            playerUIHandler.healthBar.SetValue(shipCurrentHealth);
        }
    }

    void FixedUpdate()
    {
        RotateShipToCursor();
        
        Move();
        Dash();
        ClampVelocity();
      
    }
    void RotateShipToCursor()
    {
        
        Vector3 difference = getCursorPosition() - transform.position;
        float rotAngle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // (-180,180)
        transform.rotation = Quaternion.Euler(0f, 0f, rotAngle);
    }

    void MoveInputs()
    {
        
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
    }



    void Move()
    {
        rb.velocity += moveDirection * Time.deltaTime * accelForce;         
    }

    void Dash()
    {
        dashTimer = (dashTimer > 0f && dashTimer < dashCooldownTime) ? dashTimer + dashTimerCounterIncrement : 0f;
        
        playerUIHandler.dashBar.SetValue(dashTimer);
        if (Input.GetKey(KeyCode.LeftShift) && dashTimer == 0f)
        {
            Vector3 cursorPosition = getCursorPosition();
            transform.position = Vector3.MoveTowards(transform.position, cursorPosition, accelForce * Time.deltaTime * dashForce);
            rb.velocity = new Vector2(cursorPosition.x, cursorPosition.y).normalized * Time.deltaTime * accelForce;
            dashTimer += dashTimerCounterIncrement;
        }
    }

    void ClampVelocity()
    {
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed),
                                  Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed));
    }

    
}
