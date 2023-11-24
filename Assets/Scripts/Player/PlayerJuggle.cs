using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJuggle : MonoBehaviour
{
    List<WeaponJuggleMovement> weaponsCurrentlyInJuggleLoop = new();

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

        for (int i = 1; i < weaponsCurrentlyInJuggleLoop.Count; i++)
        {
            weaponsCurrentlyInJuggleLoop[i].ThrowUpWeapon();
            weaponsCurrentlyInJuggleLoop[i].curveDeltaTime = (i * 2) - 0.6f;
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



}