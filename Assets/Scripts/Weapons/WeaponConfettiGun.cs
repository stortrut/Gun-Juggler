using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponConfettiGun : Gun
{
    [SerializeField] ShotGunUpgradeData[] shotGunLevelUpgradeData;
    private int currentBulletCount;
    private float currentSpread;

    private Knockback knockback;


    private void Start()
    {
        weaponType = WeaponType.ShotGun;
        knockback = GetComponentInParent<Knockback>();
    }



    public override void UseWeapon()
    {
        ShootWideSpread(currentBulletSpeed, currentBulletDamage, currentBulletCount);

        if (knockback != null)
            knockback.KnockBackMyself(3, 1.5f, .2f, transform.position);

        base.UseWeapon();
    }

    public void ShootWideSpread(float bulletSpeed, float bulletDamageInput, int bulletCount)
    {
        CreateNewBullet(bulletSpeed, bulletDamageInput, spriterenderer.color, Quaternion.identity);

        int halfAmountOfBulletCount = bulletCount / 2;

        inverseBullets(1, halfAmountOfBulletCount);
        inverseBullets(-1, halfAmountOfBulletCount);

        void inverseBullets(int inverseMultiplier, int halfAmountOfBulletCount)
        {
            CreateNewBullet(bulletSpeed, bulletDamageInput, spriterenderer.color, Quaternion.Euler(0, 0, currentSpread * inverseMultiplier));

            for (int i = 0; i < halfAmountOfBulletCount; i++)           //in between edges and middle
            {
                float rotationAngle = Random.Range(5, 35);
                CreateNewBullet(bulletSpeed, bulletDamageInput, spriterenderer.color, Quaternion.Euler(0, 0, rotationAngle * inverseMultiplier));
            }
        }
    }





    public override void UpgradeWeapon()
    {
        if (currentWeaponLevel >= shotGunLevelUpgradeData.Length) { return; }

        base.UpgradeWeapon();

        SetWeaponUpgradeData();
    }

    public override void SetWeaponUpgradeData()
    {
        //General
        ShotGunUpgradeData currentSmallGunUpgradeData = shotGunLevelUpgradeData[currentWeaponLevel];

        currentGunBaseUpgradeData = currentSmallGunUpgradeData.gunUpgradeData;
        currentWeaponBaseUpgradeData = currentSmallGunUpgradeData.weaponBaseUpgradeData;

        //Specifics
        currentBulletCount = currentSmallGunUpgradeData.bulletCount;
        currentSpread = currentSmallGunUpgradeData.spread;

        //Base
        base.SetWeaponUpgradeData();
    }
}


[System.Serializable]
public class ShotGunUpgradeData
{
    [Header("Genereic")]
    [SerializeField] public WeaponBaseUpgradeData weaponBaseUpgradeData;
    [SerializeField] public GunBaseUpgradeData gunUpgradeData;

    [Header("Confetti Gun Specific")]
    [SerializeField] public int bulletCount;
    [SerializeField] public float spread;
}