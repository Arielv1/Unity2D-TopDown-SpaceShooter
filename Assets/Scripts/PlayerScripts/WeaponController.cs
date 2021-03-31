using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject[] weaponsPrefabs;
    private int currentWeaponIndex = 0;
    private int weaponLevel = 0;
    [SerializeField] private int[] levelToShowWeapon = {0, 2};
    Hashtable weaponTable = new Hashtable();
    enum Weapon { BLASTER = 0 , MISSILE_LAUNCHER = 1, LASER = 2, GRAVITY_BEAM = 3}
    enum WeaponStatus { VOID = 0, EXISTS = 1, UPGRADED = 2}
    private bool isWeaponShown;
    GameObject weaponToShow;
    GameObject currentWeapon;

    // Start is called before the first frame update
    void Start()
    {
        SetWeaponTable();
        weaponToShow = weaponsPrefabs[0];
        ShowWeapon();
    }


    void SetWeaponTable()
    {
        weaponTable.Add(Weapon.BLASTER, WeaponStatus.EXISTS);
        weaponTable.Add(Weapon.MISSILE_LAUNCHER, WeaponStatus.VOID);
        weaponTable.Add(Weapon.LASER, WeaponStatus.VOID);
        weaponTable.Add(Weapon.GRAVITY_BEAM, WeaponStatus.VOID);
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.P))
        {
            weaponTable[Weapon.MISSILE_LAUNCHER] = WeaponStatus.EXISTS;
            currentWeaponIndex = 2;
            weaponToShow = weaponsPrefabs[currentWeaponIndex];
            ShowWeapon();
        }

        SwitchWeapon();
        UpgradeShip();
        UpgradeCurrentlyEquipedWeapon();
       
    }

    void SwitchWeapon()
    {

        SetWeaponVersion(KeyCode.Alpha1, Weapon.BLASTER);
        SetWeaponVersion(KeyCode.Alpha2, Weapon.MISSILE_LAUNCHER);
        SetWeaponVersion(KeyCode.Alpha3, Weapon.LASER);
        SetWeaponVersion(KeyCode.Alpha4, Weapon.GRAVITY_BEAM);   
    }

    void SetWeaponVersion(KeyCode keyCode, Weapon weapon)
    {
        if (Input.GetKeyDown(keyCode) && (int)weaponTable[weapon] != (int)WeaponStatus.VOID)
        {
            currentWeaponIndex = 2 * (int)weapon;
            if ((int)weaponTable[weapon] != (int)WeaponStatus.UPGRADED)
            {
                Debug.Log("Switching to DEFAULT " + weapon);
                weaponToShow = weaponsPrefabs[currentWeaponIndex];
            }
            else
            {
                
                Debug.Log("Switching to UPGRADED " + weapon);
                weaponToShow = weaponsPrefabs[currentWeaponIndex + 1];
            }
            ShowWeapon();
        }
        
    }


    void ShowWeapon()
    {
        Destroy(currentWeapon);
        for (int i = 0; i < levelToShowWeapon.Length; i++)
        {
            if (weaponLevel == levelToShowWeapon[i])
            {
                currentWeapon = Instantiate(weaponToShow, this.transform.position, this.transform.rotation);

                currentWeapon.transform.SetParent(this.transform.transform);
            }
        }
    }

    void UpgradeShip()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            weaponLevel++;
            ShowWeapon();

        }
    }


    void UpgradeCurrentlyEquipedWeapon()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            switch (currentWeaponIndex/2)
            {
                case (int)Weapon.BLASTER:
                    UpgradeWeapon(Weapon.BLASTER);
                    break;
                case (int)Weapon.MISSILE_LAUNCHER:
                    UpgradeWeapon(Weapon.MISSILE_LAUNCHER);
                    break;
                case (int)Weapon.LASER:
                    UpgradeWeapon(Weapon.LASER);
                    break;
                case (int)Weapon.GRAVITY_BEAM:
                    UpgradeWeapon(Weapon.GRAVITY_BEAM);
                    break;
                default:
                    break;
            }
            weaponToShow = weaponsPrefabs[currentWeaponIndex + 1];
            ShowWeapon();
            
        }
    }

    void UpgradeWeapon(Weapon weapon)
    {
        Debug.Log("Upgrading " + weapon);
        if ((int)weaponTable[weapon] == (int)WeaponStatus.VOID)
        {
            weaponTable[weapon] = WeaponStatus.EXISTS;
            return;
        }

        if ((int)weaponTable[weapon] == (int)WeaponStatus.EXISTS)
        {
            weaponTable[weapon] = WeaponStatus.UPGRADED;
            return;
        }
        
    }

    
}
