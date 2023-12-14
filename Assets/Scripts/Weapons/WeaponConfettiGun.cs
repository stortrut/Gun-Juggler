using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeaponConfettiGun : Gun
{
    [SerializeField] ShotGunUpgradeData[] shotGunLevelUpgradeData;
    private int currentBulletCount;
    private float currentSpread;
    //public static List<GameObject> bulletWave = new();
    private Knockback knockback;
    private GameObject spawnedBullet;
    private CameraShake cameraShake;

    private void Start()
    {
        weaponType = WeaponType.ShotGun;
        knockback = GetComponentInParent<Knockback>();
        cameraShake = FindObjectOfType<CameraShake>();

        SetWeaponUpgradeData();
    }

    public override void UseWeapon()
    {
        UpgradeCombo.Instance.hitSinceShot = false;
        UpgradeCombo.Instance.comboTween.Kill();
        StartCoroutine(UpgradeCombo.Instance.Combo(1.5f));

        //bulletWave.Clear();
        ShootWideSpread(currentBulletSpeed, currentBulletDamage, currentBulletCount);
        //CameraShake.instance.ShakingRandomly(.2f, .5f, .5f, 3);
        StartCoroutine(cameraShake.ShakingRandomly(.1f, .6f, .1f, 1));

        if (knockback != null)
            knockback.KnockBackMyself(3.2f, 4f, 0.2f, gunPoint.transform);

        base.UseWeapon();
    }

    public void ShootWideSpread(float bulletSpeed, float bulletDamageInput, int bulletCount)
    {
        //spawnedBullet = CreateNewBullet(bulletSpeed, bulletDamageInput, weaponSpriterenderer.color, gunPoint.rotation);
        //bulletWave.Add(spawnedBullet);

        float angleDistanceBetweenBullets = 180 / bulletCount;
        float angelSum = -90;

        for (int i = 0; i < bulletCount; i++)
        {
            float bulletAngle = angleDistanceBetweenBullets;
            if (i == 0)
                bulletAngle = (angleDistanceBetweenBullets) / 2;

            angelSum += bulletAngle;

            spawnedBullet = CreateNewBullet(bulletSpeed, bulletDamageInput, weaponSpriterenderer.color, gunPoint.rotation * Quaternion.Euler(0, 0,  angelSum));
        }


        //int halfAmountOfBulletCount = bulletCount / 2;

        //inverseBullets(1, halfAmountOfBulletCount);
        //inverseBullets(-1, halfAmountOfBulletCount);

        //void inverseBullets(int inverseMultiplier, int halfAmountOfBulletCount)
        //{
        //    spawnedBullet = CreateNewBullet(bulletSpeed, bulletDamageInput, weaponSpriterenderer.color, gunPoint.rotation * Quaternion.Euler(0, 0, currentSpread * inverseMultiplier));
        //    //bulletWave.Add(spawnedBullet);

        //    for (int i = 0; i < halfAmountOfBulletCount; i++)           //in between edges and middle
        //    {
        //        float rotationAngle = Random.Range(5, 35);
        //        spawnedBullet = CreateNewBullet(bulletSpeed, bulletDamageInput, weaponSpriterenderer.color, gunPoint.rotation * Quaternion.Euler(0, 0, rotationAngle * inverseMultiplier));
        //        //bulletWave.Add(spawnedBullet);
        //    }
        //}

        if (cameraShake == null)
        {
            cameraShake = FindObjectOfType<CameraShake>();
            Debug.Log("null?", cameraShake);
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
        //currentSpread = currentShotGunUpgradeData.spread;

        //Base
        base.SetWeaponUpgradeData();
    }
}


[System.Serializable]
public class ShotGunUpgradeData : GunBaseUpgradeData
{
    [Header("Confetti Gun Specific")]
    [SerializeField] public int bulletCount;
    //[SerializeField] public float spread;
}