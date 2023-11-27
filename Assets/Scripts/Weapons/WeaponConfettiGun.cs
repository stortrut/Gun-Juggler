using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponConfettiGun : Weapon
{
    [SerializeField] private Knockback knockback;
    private void Start()
    {   
        bulletSpeed = 20f;
        fireRate = 1f;
    }

    void Update()
    {
        fireRateTimer -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && fireRateTimer < 0)
        {
            if (this.weaponEquipped)
            {
                Shoot();
                if(knockback != null)
                    knockback.KnockBackMyself(5,3f,.4f,transform.position);
                Sound.Instance.ConfettiGunShoot();
                fireRateTimer = fireRate;
            }
        }
    }
}
