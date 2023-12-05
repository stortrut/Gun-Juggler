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

    private void Update()
    {
        fireRateTimer += Time.deltaTime;
        if (Input.GetMouseButton(0) && fireRateTimer > fireRate)
        {
            if (weaponEquipped)
            {
                Debug.Log("Shoot");

                Shoot(bulletSpeed, bulletDamage);
                fireRateTimer = 0;
                Sound.Instance.SoundRandomized(Sound.Instance.shootingSounds);
            }
        }
    }

    //[System.Serializable]
    //class SmallGunData : WeaponUpgradeStatus
    //{

    //}
}
