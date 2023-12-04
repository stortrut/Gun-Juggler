using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponConfettiGun : Gun
{
    [SerializeField] ConfettiGunData[] upgradeStatus;
    private Knockback knockback;

    private void Start()
    {
        weaponType = WeaponType.ShotGun;
        knockback = GetComponentInParent<Knockback>();
        fireRateTimer = fireRate;

        //default values
        bulletSpeed = 20f;
        fireRate = .7f;

        GetCurrentData();
    }
    public void UpgradeWeaponLevel()
    {
        currentWeaponLevel++;
        GetCurrentData();
    }
    protected void GetCurrentData()
    {
        if (upgradeStatus[currentWeaponLevel] != null)
        {
            this.bulletSpeed = upgradeStatus[currentWeaponLevel].bulletSpeed;
            this.bulletDamage = upgradeStatus[currentWeaponLevel].bulletDamage;
            this.fireRate = upgradeStatus[currentWeaponLevel].fireRate;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) 
        {
            UpgradeWeaponLevel();
        }
        fireRateTimer -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && fireRateTimer < 0)
        {
            if (this.weaponEquipped)
            {
                ShootWideSpread();
                if(knockback != null)
                    knockback.KnockBackMyself(3,1.5f,.2f,transform.position);
                fireRateTimer = fireRate;
                Sound.Instance.SoundRandomized(Sound.Instance.shootingSoundsConfettiGun);
            }
        }
    }
}

[System.Serializable]
class ConfettiGunData : WeaponUpgradeStatus
{
    //[SerializeField] int bulletCount;
}
