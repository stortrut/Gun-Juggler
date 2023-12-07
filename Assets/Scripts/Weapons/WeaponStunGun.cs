using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStunGun : WeaponBase
{
    [Header("References")]
    [SerializeField] private StunZone stunZone;

    [Header("Upgrades")]
    [SerializeField] StunGunUpgradeData[] stunGunLevelUpgradeData;


    private void Start()
    {
        weaponType = WeaponType.StunGun;

        SetWeaponUpgradeData();
    }


    public override void UseWeapon()
    {
        ReflectStun();

        base.UseWeapon();
    }

    private void ReflectStun()
    {
        Debug.Log("Used stun gun");

        foreach (GameObject obj in stunZone.objectsInField)
        {
            if (obj == null) { return; }

            if (obj.CompareTag("EnemyBullet"))
            {
                var enemyBullet = obj.GetComponent<IAim>();
                enemyBullet.Deflected();

                Debug.Log("Stunned bullet");


                //add bool so that enemy bullets now can damage enemies,
            }
            else if (obj.CompareTag("Enemy") || obj.CompareTag("EnemyNonTargetable"))
            {
                var stunnable = obj.GetComponent<IStunnable>();

                if (stunnable == null) { return; }

                stunnable.isStunnable = true;
                StartCoroutine(UnFreeze(2, stunnable));
            }
        }
    }






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


    IEnumerator UnFreeze(float timeStunned, IStunnable stunnable)
    {
        yield return new WaitForSeconds(timeStunned);
        stunnable.isStunnable = false;
    }
}



[System.Serializable]
public class StunGunUpgradeData : WeaponBaseUpgradeData
{
    //[Header("Stun Gun Specific")]

}