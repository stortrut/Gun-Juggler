using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJuggle : MonoBehaviour
{
    [SerializeField] List<WeaponJuggleMovement> weapons = new();

    [SerializeField] private WeaponJuggleMovement weaponInHand;


    private void Start()
    {
        weapons[1].ThrowUpWeapon();
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