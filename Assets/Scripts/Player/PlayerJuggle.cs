using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJuggle : MonoBehaviour
{
    List<WeaponJuggleMovement> weaponsCurrentlyInJuggleLoop = new();

    private bool isJuggling;
    private WeaponJuggleMovement weaponInHand;

    private void Start()
    {
        WeaponJuggleMovement[] weaponsOnPlayer = GetComponentsInChildren<WeaponJuggleMovement>();
        foreach (WeaponJuggleMovement weapon in weaponsOnPlayer)
        {
            weaponsCurrentlyInJuggleLoop.Add(weapon);
        }

        weaponInHand = weaponsCurrentlyInJuggleLoop[0];

        weaponsCurrentlyInJuggleLoop[0].weaponBase.EquipWeapon();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isJuggling)
        {
            isJuggling = true;
            StartCoroutine(nameof(ThrowUpAllWeaponsWithSameInterval), 2.41808f / weaponsCurrentlyInJuggleLoop.Count);
        }
    }



    IEnumerator ThrowUpAllWeaponsWithSameInterval(float waitTimeBetweenEachThrow)
    {
        for (int i = 0; i < weaponsCurrentlyInJuggleLoop.Count; i++)
        {
            weaponsCurrentlyInJuggleLoop[i].ThrowUpWeapon();
            yield return new WaitForSeconds(waitTimeBetweenEachThrow);
        }
    }

    


    public void CatchWeapon(WeaponJuggleMovement newWeapon)
    {
        weaponInHand.ThrowUpWeapon();
        weaponInHand.weaponBase.UnEquipWeapon();

        newWeapon.beingThrown = false;
        newWeapon.weaponBase.EquipWeapon();
        weaponInHand = newWeapon;
    }

    public void RemoveWeaponFromLoop(WeaponJuggleMovement weaponToRemoved)
    {
        weaponsCurrentlyInJuggleLoop.Remove(weaponToRemoved);
    }

}