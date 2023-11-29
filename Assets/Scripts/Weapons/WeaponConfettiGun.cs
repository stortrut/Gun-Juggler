using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponConfettiGun : Gun
{
    private Knockback knockback;
    private void Start()
    {
        knockback = GetComponentInParent<Knockback>();

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
                fireRateTimer = fireRate;
                Sound.Instance.SoundRandomized(Sound.Instance.shootingSoundsConfettiGun);
            }
        }
    }
}
