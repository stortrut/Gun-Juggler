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

        SetWeaponUpgradeData();
    }



    public override void UseWeapon()
    {
        ShootWideSpread(currentBulletSpeed, currentBulletDamage, currentBulletCount);
        CameraShake.instance.ShakingRandomly(.2f, .5f, .5f);

        if (knockback != null)
            knockback.KnockBackMyself(3.5f, 5f, 0.2f, gunPoint.transform);

        base.UseWeapon();
    }

    public void ShootWideSpread(float bulletSpeed, float bulletDamageInput, int bulletCount)
    {
        CreateNewBullet(bulletSpeed, bulletDamageInput, weaponSpriterenderer.color, gunPoint.rotation);

        int halfAmountOfBulletCount = bulletCount / 2;

        inverseBullets(1, halfAmountOfBulletCount);
        inverseBullets(-1, halfAmountOfBulletCount);

        void inverseBullets(int inverseMultiplier, int halfAmountOfBulletCount)
        {
            CreateNewBullet(bulletSpeed, bulletDamageInput, weaponSpriterenderer.color, gunPoint.rotation * Quaternion.Euler(0, 0, currentSpread * inverseMultiplier));

            for (int i = 0; i < halfAmountOfBulletCount; i++)           //in between edges and middle
            {
                float rotationAngle = Random.Range(5, 35);
                CreateNewBullet(bulletSpeed, bulletDamageInput, weaponSpriterenderer.color, gunPoint.rotation * Quaternion.Euler(0, 0, rotationAngle * inverseMultiplier));
            }
        }
    }





    public override void UpgradeWeapon()
    {
        if (currentWeaponLevel >= shotGunLevelUpgradeData.Length - 1) { return; }

        base.UpgradeWeapon();

        SetWeaponUpgradeData();
    }

    public override void SetWeaponUpgradeData()
    {
        //General
        ShotGunUpgradeData currentShotGunUpgradeData = shotGunLevelUpgradeData[currentWeaponLevel];
        currentWeaponBaseUpgradeData = shotGunLevelUpgradeData[currentWeaponLevel];
        currentGunBaseUpgradeData = shotGunLevelUpgradeData[currentWeaponLevel];

        //Specifics
        currentBulletCount = currentShotGunUpgradeData.bulletCount;
        currentSpread = currentShotGunUpgradeData.spread;

        //Base
        base.SetWeaponUpgradeData();
    }
}


[System.Serializable]
public class ShotGunUpgradeData : GunBaseUpgradeData
{
    [Header("Confetti Gun Specific")]
    [SerializeField] public int bulletCount;
    [SerializeField] public float spread;
}