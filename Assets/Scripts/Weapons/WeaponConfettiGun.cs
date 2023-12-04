using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponConfettiGun : Gun
{
    [SerializeField] ConfettiGunData[] upgradeStates;
    private Knockback knockback;
    private void Start()
    {
        weaponType = WeaponType.ShotGun;
        knockback = GetComponentInParent<Knockback>();

        bulletSpeed = 20f;
        fireRate = .8f;
        Debug.Log("hej");
    }

    void Update()
    {
        fireRateTimer -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && fireRateTimer < 0)
        {
            if (this.weaponEquipped)
            {
                ShootWideSpread();
                if(knockback != null)
                    knockback.KnockBackMyself(5,3f,.4f,transform.position);
                fireRateTimer = fireRate;
                Sound.Instance.SoundRandomized(Sound.Instance.shootingSoundsConfettiGun);
            }
        }
    }
}

[System.Serializable]
class ConfettiGunData : WeaponUpgradeStatus
{
    [SerializeField] public int hej = 111111;
}
