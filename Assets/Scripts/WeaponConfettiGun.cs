using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponConfettiGun : Weapon
{
    [SerializeField]
    bool equipped = true;
    private void Start()
    {
        bulletSpeed = 20f;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (equipped)
            {
                Shoot();
                //Weapon.Knockback();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (!equipped)
            {
                equipped = true;
            }
            else if (equipped)
            {
                equipped = false;
            }
        }
    }
}
