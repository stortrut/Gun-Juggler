using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGun : Weapon
{
    [SerializeField]
    bool equipped = false;
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
