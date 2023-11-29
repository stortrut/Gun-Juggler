using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSmallGun : Gun
{
    private void Start()
    {
        weaponType = WeaponType.SmallGun;
        bulletSpeed = 20f;
        fireRate = .2f;
    }

    void Update()
    {
        fireRateTimer-=Time.deltaTime;
        if (Input.GetMouseButton(0) && fireRateTimer< 0)
        {
            if (weaponEquipped)
            {
                Shoot();
                fireRateTimer = fireRate;
            }
        }
    }
}
