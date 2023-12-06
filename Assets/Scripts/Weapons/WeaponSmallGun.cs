using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSmallGun : Gun
{
    [SerializeField] SmallGunUpgradeData[] smallGunLevelUpgradeData;

    private void Start()
    {
        weaponType = WeaponType.SmallGun;
        fireCooldownTimer = fireCooldown;

        SetWeaponUpgradeData();
    }

    public override void UseWeapon()
    {
        Debug.Log("Used small weapon");

        Shoot();

        base.UseWeapon();
    }


    private void Shoot()
    {
        GameObject weaponBullet = Instantiate(bullet, gunPoint.position, gunPoint.rotation);

        Bullet bulletScript = weaponBullet.GetComponent<Bullet>();
        bulletScript.SetColor(spriterenderer.color);
        bulletScript.SetBulletData(currentBulletSpeed, currentBulletDamage);
    }


    public override void UpgradeWeapon()
    {
        if(currentWeaponLevel >= smallGunLevelUpgradeData.Length) { return; }

        base.UpgradeWeapon();

        SetWeaponUpgradeData();
    }

    public override void SetWeaponUpgradeData()
    {
        //General
        SmallGunUpgradeData currentSmallGunUpgradeData = smallGunLevelUpgradeData[currentWeaponLevel];

        currentGunBaseUpgradeData = currentSmallGunUpgradeData.gunUpgradeData;
        currentWeaponBaseUpgradeData = currentSmallGunUpgradeData.weaponBaseUpgradeData;

        //Specifics


        //Base
        base.SetWeaponUpgradeData();
    }
}

[System.Serializable]
public class SmallGunUpgradeData
{
    [Header("Genereic")]
    [SerializeField] public WeaponBaseUpgradeData weaponBaseUpgradeData;
    [SerializeField] public GunBaseUpgradeData gunUpgradeData;

    [Header("Small Gun Specific")]
    [SerializeField] public string name;

}