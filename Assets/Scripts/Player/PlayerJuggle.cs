using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJuggle : MonoBehaviour
{
    [SerializeField] List<WeaponJuggleMovement> weapons = new();

    [SerializeField] private WeaponJuggleMovement weaponInHand;

    public void CatchWeapon(WeaponJuggleMovement newWeapon)
    {
        weaponInHand.ThrowUpWeapon();

        weaponInHand = newWeapon;
    }



}