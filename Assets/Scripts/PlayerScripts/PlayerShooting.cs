using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject[] projectilePrefab;
    public float bulletForce = 20f;
    private int weaponIndex = 0;
    private int weaponTier = 1;
    [SerializeField] private GameObject[] weaponPrefabs;
    [SerializeField] private int[] initializerIndex;
    private GameObject currentWeapon;
    private bool shown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShowWeapon();
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        UpgradeWeaponTier();
    }

    void ShowWeapon()
    {
        for (int i = 0; i < initializerIndex.Length && !shown; i++)
        {
            if (initializerIndex[i] == weaponTier)
            {
                Debug.Log("here " + weaponTier);
                currentWeapon = Instantiate(weaponPrefabs[weaponIndex], this.transform.position, this.transform.rotation);
                currentWeapon.transform.SetParent(this.transform);
            }
        }
        shown = true;
    }

    void Shoot()
    {
        if (currentWeapon)
        {
            GameObject bullet = Instantiate(projectilePrefab[weaponIndex], this.transform.position, this.transform.rotation);
            bullet.tag = "Projectile";

            PolygonCollider2D bulletCollider = bullet.AddComponent<PolygonCollider2D>() as PolygonCollider2D;
            bulletCollider.isTrigger = true;

            bullet.AddComponent(typeof(PlayerProjectileHitHandler));

            Rigidbody2D rb = bullet.AddComponent<Rigidbody2D>() as Rigidbody2D;
            rb.gravityScale = 0f;
            rb.AddForce(this.transform.right * bulletForce, ForceMode2D.Impulse);
        }
        
    }

    void UpgradeWeaponTier()
    {
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            weaponTier++;
            Destroy(currentWeapon);
            shown = false;
            ShowWeapon();
           
        }
    }
}
