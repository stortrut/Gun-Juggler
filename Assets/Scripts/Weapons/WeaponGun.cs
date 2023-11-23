using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGun : Weapon
{
    private void Start()
    {
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
