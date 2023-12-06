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
    }


    public override void UseWeapon()
    {
        ReflectStun();

        base.UseWeapon();
    }

    private void ReflectStun()
    {
        foreach (GameObject obj in stunZone.objectsInField)
        {
            if (obj == null) { return; }

            if (obj.CompareTag("EnemyBullet"))
            {
                var enemyBullet = obj.GetComponent<EnemyBullet>();
                enemyBullet.Deflected();
                //add bool so that enemy bullets now can damage enemies,
            }
            else if (obj.CompareTag("Enemy"))
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
        if (currentWeaponLevel >= stunGunLevelUpgradeData.Length) { return; }

        base.UpgradeWeapon();

        SetWeaponUpgradeData();
    }

    public override void SetWeaponUpgradeData()
    {
        //General
        StunGunUpgradeData currentSmallGunUpgradeData = stunGunLevelUpgradeData[currentWeaponLevel];

        currentWeaponBaseUpgradeData = currentSmallGunUpgradeData.weaponBaseUpgradeData;

        //Specifics


        //Base
        base.SetWeaponUpgradeData();
    }









    IEnumerator UnFreeze(float timeStunned, IStunnable stunnable)
    {
        yield return new WaitForSeconds(timeStunned);
        stunnable.isStunnable = false;
    }

    //tryck för att spela: deflecta bullets och stunna enemies
    //raycast, stun interface, deflect script
  
    private void Update()
    {
        fireCooldownTimer -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && fireCooldownTimer < 0)
        {
            if (this.weaponEquipped)
            {
                ReflectStun();
            }
        }
    }








}



[System.Serializable]
public class StunGunUpgradeData
{
    [Header("Genereic")]
    [SerializeField] public WeaponBaseUpgradeData weaponBaseUpgradeData;

    //[Header("Stun Gun Specific")]

}