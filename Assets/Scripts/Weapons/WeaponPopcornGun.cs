using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPopcornGun : Gun
{
    [SerializeField] Sprite[] popcorns;
    [SerializeField] float popcornSpeed;
    [SerializeField] float popcornDamage;

    [SerializeField] public GameObject audioSource;

    void Start()
    {
        weaponType = WeaponType.PopcornGun;
        fireCooldown = 0.1f;
        _waitUntilThrowTime = 100f;

        StartCoroutine(nameof(ShootAllTheTime));
    }

    private void Update()
    {
        aimAgain = true;


        player = Manager.Instance.player.gameObject;

        if(player == null) { return; }

        aim = player.GetComponentInChildren<Aim>();

        transform.rotation = aim.bulletRotation;


        AdjustAim();


    }

    IEnumerator ShootAllTheTime()
    {
        if (weaponEquipped)
        {
            yield return new WaitForSeconds(0.05f);
            Shoot();
            StartCoroutine(nameof(ShootAllTheTime));

        }

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
