using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrackPlayer : MonoBehaviour
{
    private PlayerShipController player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerShipController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() 
    {
        FacePlayer();    
    }

    void FacePlayer()
    {
        Vector3 difference = transform.position - player.transform.position;
        float rotAngle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotAngle);
    }

}
