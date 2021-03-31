using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerShipController player;
    private float z;
    void Start()
    {
        player = FindObjectOfType<PlayerShipController>();
        z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
       Vector3 playerPos = player.transform.position;
       Vector3 currentPos = transform.position;
       transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
      
    }
}
