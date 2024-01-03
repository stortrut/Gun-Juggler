using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPopcornGun : Gun
{
    [SerializeField] Sprite[] popcorns;
    [SerializeField] float popcornSpeed;
    [SerializeField] float popcornDamage;

    void Start()
    {
        weaponType = WeaponType.PopcornGun;
        fireCooldown = 0.1f;
    }

    void Update()
    {
        
    }

    public override void UseWeapon()
    {
        if (UpgradeCombo.Instance != null)
        {
            UpgradeCombo.Instance.hitSinceShot = false;
            StartCoroutine(UpgradeCombo.Instance.Combo());
        }

        Shoot();

        base.UseWeapon();
    }

    private void Shoot()
    {
        var white = weaponSpriterenderer.color = Color.white;
        CreateNewBullet(popcornSpeed, popcornDamage, white, gunPoint.rotation);
    }
}
