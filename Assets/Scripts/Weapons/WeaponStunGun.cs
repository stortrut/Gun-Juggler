using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class WeaponStunGun : WeaponBase
{
    [Header("References")]
    [SerializeField] private StunZone stunZone;
    [SerializeField] private Animator animator;

    private bool hit = false;
    [Header("Upgrades")]
    [SerializeField] StunGunUpgradeData[] stunGunLevelUpgradeData;


    private void Start()
    {
        weaponType = WeaponType.StunGun;

        SetWeaponUpgradeData();
    }


    public override void UseWeapon()
    {
        if(UpgradeCombo.Instance != null)
        { 
        UpgradeCombo.Instance.hitSinceShot = false;
        UpgradeCombo.Instance.comboTween.Kill();
        StartCoroutine(UpgradeCombo.Instance.Combo());
        }
        stunZone.SoundWave();
        base.UseWeapon();
        ReflectStun();
        //Sound.Instance.SoundRandomized(Sound.Instance.weaponShootingSoundsEnumOrder[2]);
    }

    private void ReflectStun()
    {
        hit = false;
        foreach (GameObject obj in stunZone.objectsInField)
        {
            if (obj == null) { return; }

            if (obj.CompareTag("EnemyBullet"))
            {
                var enemyBullet = obj.GetComponent<IAim>();
                enemyBullet.Deflected();

                //add bool so that enemy bullets now can damage enemies,
            }
            else if (obj.CompareTag("Enemy") || obj.CompareTag("EnemyNonTargetable"))
            {

                var stunnable = obj.GetComponents<IStunnable>();
                var damageable = obj.GetComponent<Health>();
               
                hit = true;
                if (stunnable == null) { return; }
                //Debug.Log(stunnable);
                foreach(var stun in stunnable)
                { 
                stun.isStunnable = true;
                StartCoroutine(UnFreeze(2, stun,damageable));
                
                }

                //stunZone.objectsInField.RemoveAll(item => item == null);

            }
        }
        if(hit == true)
        {
            UpgradeCombo.Instance.hitSinceShot = true;
            UpgradeCombo.Instance.comboTween.Kill();
        }
    }




    //public override void AdjustAim()
    //{
    //    transform.rotation = Quaternion.identity;
    //}

    public override void UpgradeWeapon()
    {
        if (currentWeaponLevel >= stunGunLevelUpgradeData.Length - 1) { return; }

        base.UpgradeWeapon();

        SetWeaponUpgradeData();
    }

    public override void SetWeaponUpgradeData()
    {
        //General
        StunGunUpgradeData currentStunGunUpgradeData = stunGunLevelUpgradeData[currentWeaponLevel];
        currentWeaponBaseUpgradeData = stunGunLevelUpgradeData[currentWeaponLevel];

        //Specifics

        //Base
        base.SetWeaponUpgradeData();
    }
        
    IEnumerator UnFreeze(float timeStunned, IStunnable stunnable, IDamageable damageable)
    {
        yield return new WaitForSeconds(timeStunned);
        stunnable.isStunnable = false;
        damageable.ApplyDamage(1);

    }
}



[System.Serializable]
public class StunGunUpgradeData : WeaponBaseUpgradeData
{
    [Header("Stun Gun Specific")]
    [SerializeField] public int soundWaveCount;
    [SerializeField] public float spread;

}