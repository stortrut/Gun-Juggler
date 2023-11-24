using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponConfettiGun : Weapon
{
    [SerializeField] private Knockback knockback;
    private void Start()
    {   
        bulletSpeed = 20f;
        fireRate = 0.8f;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (weaponEquipped)
            {
                Shoot();
                //knockback.KnockBackMyself(transform.position);
            }
        }
    }
}
