using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSmallGun : Gun
{
    private void Start()
    {
        weaponType = WeaponType.SmallGun;
        fireRateTimer = fireRate;

        //default values
        bulletSpeed = 20f;
        fireRate = 0.2f;
        GetCurrentData();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            UpgradeWeaponLevel();
        }
        
        fireRateTimer -=Time.deltaTime;
        if (Input.GetMouseButton(0) && fireRateTimer< 0)
        {
            if (weaponEquipped)
            {
                Shoot(bulletSpeed, bulletDamage);
                fireRateTimer = fireRate;
                Sound.Instance.SoundRandomized(Sound.Instance.shootingSounds);
            }
        }
    }

    //[System.Serializable]
    //class SmallGunData : WeaponUpgradeStatus
    //{

    //}
}
