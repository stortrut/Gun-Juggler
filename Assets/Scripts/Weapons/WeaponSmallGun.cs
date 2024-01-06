using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSmallGun : Gun
{
    [SerializeField] SmallGunUpgradeData[] smallGunLevelUpgradeData;

    private void Start()
    {
        weaponType = WeaponType.WaterPistol;
        fireCooldownTimer = fireCooldown;

        SetWeaponUpgradeData();
    }

    public override void UseWeapon()
    {
        //if (UpgradeCombo.Instance != null)
        //{
        //    UpgradeCombo.Instance.hitSinceShot = false;
        //    StartCoroutine(UpgradeCombo.Instance.Combo());
        //}

        Shoot();

        base.UseWeapon();
    }


    private void Shoot()
    {
        CreateNewBullet(currentBulletSpeed, currentBulletDamage, weaponSpriterenderer.color, gunPoint.rotation);
        Score.Instance.bulletsShot++;
        //GameObject weaponBullet = Instantiate(bullet, gunPoint.position, gunPoint.rotation);

        //Bullet bulletScript = weaponBullet.GetComponent<Bullet>();
        //bulletScript.SetColor(weaponSpriterenderer.color);
        //bulletScript.SetBulletData(currentBulletSpeed, currentBulletDamage);
    }


    public override void UpgradeWeapon()
    {
        if(currentWeaponLevel >= smallGunLevelUpgradeData.Length - 1) { return; }

        base.UpgradeWeapon();

        SetWeaponUpgradeData();
    }

    public override void SetWeaponUpgradeData()
    {
        //General
        SmallGunUpgradeData currentSmallGunUpgradeData = smallGunLevelUpgradeData[currentWeaponLevel];
        currentWeaponBaseUpgradeData = smallGunLevelUpgradeData[currentWeaponLevel];
        currentGunBaseUpgradeData = smallGunLevelUpgradeData[currentWeaponLevel];

        //Specifics

        //Base
        base.SetWeaponUpgradeData();
    }
}

[System.Serializable]
public class SmallGunUpgradeData : GunBaseUpgradeData
{
    //[Header("Small Gun Specific")]
    //[SerializeField] public string funny;
}