using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class WeaponStunGun : WeaponBase
{
    [Header("References")]
    [SerializeField] private GameObject stunZoneObject;
    [SerializeField] private StunZone stunZone;
    [SerializeField] private Animator animator;

    [Header("Upgrades")]
    [SerializeField] StunGunUpgradeData[] stunGunLevelUpgradeData;

    private Knockback knockback;


    private void Start()
    {
        weaponType = WeaponType.TrumpetGun;

        SetWeaponUpgradeData();
    }


    public override void UseWeapon()
    {
        //if(UpgradeCombo.Instance != null)
        //{ 
        //    UpgradeCombo.Instance.hitSinceShot = false;
        //    UpgradeCombo.Instance.comboTween.Kill();
        //    StartCoroutine(UpgradeCombo.Instance.Combo());
        //}
        stunZone.SoundWave();
        if (knockback != null)
        {
            knockback.KnockBackMyself(3.2f, 4f, 0.5f, gunPoint.transform);
        }
        
        base.UseWeapon();
        ReflectStun();
        Score.Instance.bulletsShot++;
        //Sound.Instance.SoundRandomized(Sound.Instance.weaponShootingSoundsEnumOrder[]);
    }

    private void ReflectStun()
    {
        //hit = false;
        //foreach (GameObject obj in stunZone.objectsInField)
        //{
        //    if (obj == null) { return; }

        //    if (obj.CompareTag("EnemyBullet"))
        //    {
        //        var enemyBullet = obj.GetComponent<IAim>();
        //        enemyBullet.Deflected();

        //        //add bool so that enemy bullets now can damage enemies,
        //    }
        //    else if (obj.CompareTag("Enemy") || obj.CompareTag("EnemyNonTargetable"))
        //    {

        //        var stunnable = obj.GetComponents<IStunnable>();
        //        var damageable = obj.GetComponent<Health>();
               
        //        hit = true;
        //        if (stunnable == null) { return; }
        //        //Debug.Log(stunnable);
        //        foreach(var stun in stunnable)
        //        { 
        //        stun.isStunnable = true;
        //        StartCoroutine(UnFreeze(2, stun,damageable));
                
        //        }

                //stunZone.objectsInField.RemoveAll(item => item == null);

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
        stunZoneObject.transform.localScale *= currentStunGunUpgradeData.scale;

        //Base
        base.SetWeaponUpgradeData();
    }
        

}



[System.Serializable]
public class StunGunUpgradeData : WeaponBaseUpgradeData
{
    [Header("Stun Gun Specific")]
    [SerializeField] public int soundWaveCount;
    [SerializeField] public float spread;
    [SerializeField] public float scale;

}