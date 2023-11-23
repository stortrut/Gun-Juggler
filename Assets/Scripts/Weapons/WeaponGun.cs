using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGun : Weapon
{
    private void Start()
    {
        bulletSpeed = 20f;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (weaponEquipped)
            {
                Shoot();
            }
        }
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space))
        {
            if (!weaponEquipped)
            {
                weaponEquipped = true;
                //spriterenderer.enabled = true;
            }
            else if (weaponEquipped)
            {
                weaponEquipped = false;
                //spriterenderer.enabled = false;
            }
        }
    }
}
