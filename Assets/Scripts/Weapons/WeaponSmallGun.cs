using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSmallGun : Gun
{
    [SerializeField] SmallGunData[] upgradeStatus;
    private void Start()
    {
        weaponType = WeaponType.SmallGun;

        //default values
        bulletSpeed = 20f;
        fireRate = .2f;
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
        if (Input.GetKeyDown(KeyCode.G))
        {
            UpgradeWeaponLevel();
        }
        
        fireRateTimer -=Time.deltaTime;
        if (Input.GetMouseButton(0) && fireRateTimer< 0)
        {
            if (weaponEquipped)
            {
                Shoot();
                fireRateTimer = fireRate;
                Sound.Instance.SoundRandomized(Sound.Instance.shootingSounds);
            }
        }
    }

    [System.Serializable]
    class SmallGunData : WeaponUpgradeStatus
    {

    }
}
