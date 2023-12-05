using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : WeaponBase
{
    [SerializeField] GunData[] upgradeStatus;

    [SerializeField] protected float bulletDamage, bulletSpeed;
    [SerializeField] protected int currentWeaponLevel = 0;

    [SerializeField] protected GameObject bulletSmall;
    [SerializeField] protected Vector2 spawnBulletPos;
    [SerializeField] protected float bulletSpread = 40;

    public float rotationAngle;
    //public float radius = 10f; //how far from gunpoint the bullets spawn
    public void UpgradeWeaponLevel()
    {
        //if weaponEquipped?
        currentWeaponLevel++;
    }

    protected void GetCurrentData()
    {
        if(upgradeStatus.Length <= 0) { return; }

        this.bulletSpeed = upgradeStatus[currentWeaponLevel].bulletSpeed;
        this.bulletDamage = upgradeStatus[currentWeaponLevel].bulletDamage;
        this.fireRate = upgradeStatus[currentWeaponLevel].fireRate;
    }

    public void Shoot(float bulletSpeedInput, float bulletDamageInput)
    {
        bulletSpeed = bulletSpeedInput;
        GameObject weaponBullet = Instantiate(bullet, gunPoint.position, gunPoint.rotation);
        //Sound.Instance.EnemyNotTakingDamage();

        Bullet bulletScript = weaponBullet.GetComponent<Bullet>();
        bulletScript.SetColor(spriterenderer.color);
        bulletScript.SetBulletData(bulletSpeedInput, bulletDamageInput);

        weaponBullet.GetComponent<Rigidbody2D>().velocity = weaponBullet.transform.right * bulletSpeed;
        Destroy(weaponBullet, 2f);
    }

    public void ShootWideSpread(float bulletSpeed, float bulletDamageInput, int halfAmountOfBulletCount)
    {
        CreateNewBullet(bulletSpeed, bulletDamageInput, spriterenderer.color, Quaternion.identity);

        inverseBullets(1, halfAmountOfBulletCount);
        inverseBullets(-1, halfAmountOfBulletCount);

        void inverseBullets(int inverseMultiplier, int halfAmountOfBulletCount)      
        {
            CreateNewBullet(bulletSpeed, bulletDamageInput, spriterenderer.color, Quaternion.Euler(0, 0, bulletSpread * inverseMultiplier));

            for (int i = 0; i < halfAmountOfBulletCount; i++)           //in between edges and middle
            {
                float rotationAngle = Random.Range(5, 35);
                CreateNewBullet(bulletSpeed, bulletDamageInput, spriterenderer.color, Quaternion.Euler(0, 0, rotationAngle * inverseMultiplier));
            }
        }
    }

    private void CreateNewBullet(float bulletSpeed, float bulletDamageInput, Color color, Quaternion rotation)
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
class GunData : WeaponUpgradeStatus
{
    
}
