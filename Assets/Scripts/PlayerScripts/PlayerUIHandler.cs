using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIHandler : MonoBehaviour
{
    public HealthBar healthBar;
    [SerializeField] private float health;
    private float hpIncrementAmout;
    [SerializeField] private float hpBarOffset = 0.02f;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxHealth(health);
        healthBar.SetHealth(health);
        hpIncrementAmout = 0.15f * healthBar.transform.localScale.x;
        //hpBarOffset += hpIncrementAmout;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            health -= 5;
            healthBar.SetHealth(health);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            healthBar.SetMaxHealth(hpIncrementAmout);
            //healthBar.transform.localScale = new Vector3 (hpIncrementAmout + healthBar.transform.localScale.x, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
            //healthBar.transform.position = new Vector3(hpIncrementAmout + healthBar.transform.position.x, healthBar.transform.position.y, healthBar.transform.position.z);
            healthBar.transform.localScale -= Vector3.left * hpIncrementAmout;
            healthBar.transform.position -= Vector3.left * hpBarOffset;
        }
    }
}
