using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Gun : WeaponBase
{
    [Header("Gun Specifics")]
    [SerializeField] protected GameObject bulletSmall;

    [HideInInspector] public GunBaseUpgradeData currentGunBaseUpgradeData;
    [HideInInspector] protected float currentBulletDamage, currentBulletSpeed;




   
    public override void SetWeaponUpgradeData()
    {
        base.SetWeaponUpgradeData();

        currentBulletDamage = currentGunBaseUpgradeData.bulletDamage;
        currentBulletSpeed = currentGunBaseUpgradeData.bulletSpeed;
        



        //if(upgradeLevelData.Length <= 0) { Debug.Log("No upgrade ERROR"); return; }

        //this.currentBulletSpeed = upgradeLevelData[currentWeaponLevel].bulletSpeed;
        //this.currentBulletDamage = upgradeLevelData[currentWeaponLevel].bulletDamage;
        //this.fireCooldown = upgradeLevelData[currentWeaponLevel].weaponCooldown;
    }

    //public void UseWeapon(float bulletSpeedInput, float bulletDamageInput)
    //{
    //    bulletSpeed = bulletSpeedInput;
    //    GameObject weaponBullet = Instantiate(bullet, gunPoint.position, gunPoint.rotation);
    //    //Sound.Instance.EnemyNotTakingDamage();

    //    Bullet bulletScript = weaponBullet.GetComponent<Bullet>();
    //    bulletScript.SetColor(spriterenderer.color);
    //    bulletScript.SetBulletData(bulletSpeedInput, bulletDamageInput);

    //    weaponBullet.GetComponent<Rigidbody2D>().velocity = weaponBullet.transform.right * bulletSpeed;
    //    Destroy(weaponBullet, 2f);
    //}



    //Used by guns
    public void CreateNewBullet(float bulletSpeed, float bulletDamageInput, Color color, Quaternion rotation)
    {
        GameObject weaponBullet = Instantiate(bulletSmall, gunPoint.position, rotation);
        Bullet bulletScript = weaponBullet.GetComponent<Bullet>();
        bulletScript.SetColor(color);
        bulletScript.SetBulletData(bulletSpeed, bulletDamageInput);

        weaponBullet.GetComponent<Rigidbody2D>().velocity = weaponBullet.transform.right * bulletSpeed;
        Destroy(weaponBullet, 1);
    }


}


[System.Serializable]
public class GunBaseUpgradeData
{
    [SerializeField] public float bulletDamage;
    [SerializeField] public float bulletSpeed;
}