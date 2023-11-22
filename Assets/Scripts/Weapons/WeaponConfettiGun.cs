using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponConfettiGun : Weapon
{
    [SerializeField] private bool equipped = true;
    [SerializeField] private Knockback knockback;
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
                knockback.KnockBackMyself(transform.position,50);
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
