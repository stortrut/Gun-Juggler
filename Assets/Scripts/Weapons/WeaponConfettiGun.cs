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

    private void Start()
    {
        weaponType = WeaponType.ConfettiGun;
        knockback = GetComponentInParent<Knockback>();

        SetWeaponUpgradeData();
    }

    public override void UseWeapon()
    {
        //if (UpgradeCombo.Instance != null)
        //{
        //    UpgradeCombo.Instance.hitSinceShot = false;
        //    UpgradeCombo.Instance.comboTween.Kill();
        //    StartCoroutine(UpgradeCombo.Instance.Combo());
        //}
        if(AudienceSatisfaction.Instance != null)
        {
            AudienceSatisfaction.Instance.AudienceHappiness(5);
            Sound.Instance.SoundSet(Sound.Instance.audienceApplauding, 1, .4f, .1f);
            Sound.Instance.SoundSet(Sound.Instance.otherPositiveReactions, 0, .2f, .1f);
        }
        //bulletWave.Clear();
        ShootWideSpread(currentBulletSpeed, currentBulletDamage, currentBulletCount);
        //StartCoroutine(CameraShake.instance.ShakingRandomly(.1f, .6f, .1f, 1));
        //CameraShakeRobert.instance.AddTrauma(1f);

        FindObjectOfType<CameraShake>().BasicCameraShake();

        if (knockback != null)
            knockback.KnockBackMyself(3f, 4f, 0.4f, gunPoint.transform);

        base.UseWeapon();
    }

    public void ShootWideSpread(float bulletSpeed, float bulletDamageInput, int bulletCount)
    {
        //spawnedBullet = CreateNewBullet(bulletSpeed, bulletDamageInput, weaponSpriterenderer.color, gunPoint.rotation);
        //bulletWave.Add(spawnedBullet);

        float spread = 30;


        float angleDistanceBetweenBullets = spread / bulletCount;
        float angelSum = -90;


        EffectAnimations.Instance.ConfettiBurst(gunPoint.position, new Vector3(2,2), gunPoint.rotation);
        for (int i = 0; i < bulletCount; i++)
        {
            float bulletAngle = angleDistanceBetweenBullets;
            if (i == 0)
                bulletAngle = (180 - spread) / 2;

            angelSum += bulletAngle;

            spawnedBullet = CreateNewBullet(bulletSpeed, bulletDamageInput, weaponSpriterenderer.color, gunPoint.rotation * Quaternion.Euler(0, 0,  angelSum));
            Score.Instance.bulletsShot++;
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