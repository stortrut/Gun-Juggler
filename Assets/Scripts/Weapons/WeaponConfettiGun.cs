using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponConfettiGun : Gun
{
    private Knockback knockback;
    public int bulletCount;

    private void Start()
    {
        weaponType = WeaponType.ShotGun;
        knockback = GetComponentInParent<Knockback>();
        fireRateTimer = fireRate;

        //default values
        bulletSpeed = 20f;
        fireRate = 0.7f;
        bulletCount = 15;

        GetCurrentData();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) 
        {
            UpgradeWeaponLevel();
        }
        fireRateTimer -= Time.deltaTime;
        if (Input.GetMouseButton(0) && fireRateTimer < 0)
        {
            if (this.weaponEquipped)
            {
                ShootWideSpread(bulletSpeed, bulletDamage, bulletCount);
                if(knockback != null)
                    knockback.KnockBackMyself(3,1.5f,.2f,transform.position);
                fireRateTimer = fireRate;
                Sound.Instance.SoundRandomized(Sound.Instance.shootingSoundsConfettiGun);
            }
        }
    }
}

//[System.Serializable]
//class ConfettiGunData : WeaponUpgradeStatus
//{
//    [SerializeField] public int bulletCount;
//}
