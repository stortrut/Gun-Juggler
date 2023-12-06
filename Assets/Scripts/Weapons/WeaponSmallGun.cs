using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSmallGun : Gun
{
    private void Start()
    {
        weaponType = WeaponType.SmallGun;
        fireCooldownTimer = fireCooldown;

        //default values
        bulletSpeed = 20f;
        fireCooldown = 0.2f;
        GetCurrentData();
    }

    private void Update()
    {
        fireCooldownTimer += Time.deltaTime;
        if (Input.GetMouseButton(0) && fireCooldownTimer > fireCooldown)
        {
            if (weaponEquipped)
            {
                Debug.Log("Shoot");

                Shoot(bulletSpeed, bulletDamage);
                fireCooldownTimer = 0;
                Sound.Instance.SoundRandomized(Sound.Instance.shootingSounds);
            }
        }
    }

    //[System.Serializable]
    //class SmallGunData : WeaponUpgradeStatus
    //{

    //}
}
