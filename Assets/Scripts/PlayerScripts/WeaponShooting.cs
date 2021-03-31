using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    [SerializeField] private Transform[] firePoints;
    [SerializeField] private GameObject projectilePrefab;
    public float bulletForce = 20f;
    private List<GameObject> projectiles = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        for (int i = 0; i < firePoints.Length; i++)
        {
            GameObject bullet = Instantiate(projectilePrefab, firePoints[i].transform.position, this.transform.rotation);
            bullet.tag = "Projectile";

            PolygonCollider2D bulletCollider = bullet.AddComponent<PolygonCollider2D>() as PolygonCollider2D;
            bulletCollider.isTrigger = true;

            bullet.AddComponent(typeof(PlayerProjectileHitHandler));

            Rigidbody2D rb = bullet.AddComponent<Rigidbody2D>() as Rigidbody2D;
            rb.gravityScale = 0f;
            rb.AddForce(this.transform.right * bulletForce, ForceMode2D.Impulse);

            projectiles.Add(bullet);
        }
    }
}
