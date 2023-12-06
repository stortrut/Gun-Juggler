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
        fireCooldownTimer = fireCooldown;

        //default values
        bulletSpeed = 20f;
        fireCooldown = 0.7f;
        bulletCount = 15;

        GetCurrentData();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) 
        {
            UpgradeWeaponLevel();
        }
        fireCooldownTimer -= Time.deltaTime;
        if (Input.GetMouseButton(0) && fireCooldownTimer < 0)
        {
            if (this.weaponEquipped)
            {
                ShootWideSpread(bulletSpeed, bulletDamage, bulletCount);
                if(knockback != null)
                    knockback.KnockBackMyself(3,1.5f,.2f,transform.position);
                fireCooldownTimer = fireCooldown;
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
