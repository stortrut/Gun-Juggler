using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] GameObject weaponPrefabToPickup;

    [SerializeField] bool justDropAllWeapons;


    [SerializeField] bool replaceAllPlayerWeaponsWithThisWeapon;
    [SerializeField] bool andAlsoAddAWeaponIfPlayerHasNone;


    [SerializeField] bool dropAllWeaponsAndThenPickUpOne;



    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (justDropAllWeapons)
            {
                List<WeaponJuggleMovement> oldWeapons = new();
                for (int i = 0; i < collision.GetComponent<PlayerJuggle>().weaponsCurrentlyInJuggleLoop.Count; i++)
                {
                    WeaponJuggleMovement oldWeaponData = collision.GetComponent<PlayerJuggle>().weaponsCurrentlyInJuggleLoop[i];
                    oldWeapons.Add(oldWeaponData);
                }

                for (int i = 0; i < oldWeapons.Count - 1; i++)
                {
                    oldWeapons[i].DropWeapon();
                }
            }

            if (replaceAllPlayerWeaponsWithThisWeapon)
            {
                if (andAlsoAddAWeaponIfPlayerHasNone && collision.GetComponent<PlayerJuggle>().weaponsCurrentlyInJuggleLoop.Count < 1)
                {
                    collision.GetComponent<PlayerJuggle>().CreateAndAddWeaponToLoop(weaponPrefabToPickup);
                }
                else
                {
                    collision.GetComponent<PlayerJuggle>().ReplaceAllWeaponsWithAnotherWeapon(weaponPrefabToPickup);
                }
            }
            else if (dropAllWeaponsAndThenPickUpOne)
            {
                List<WeaponJuggleMovement> oldWeapons = new();
                for (int i = 0; i < collision.GetComponent<PlayerJuggle>().weaponsCurrentlyInJuggleLoop.Count; i++)
                {
                    WeaponJuggleMovement oldWeaponData = collision.GetComponent<PlayerJuggle>().weaponsCurrentlyInJuggleLoop[i];
                    oldWeapons.Add(oldWeaponData);
                }

                for (int i = 0; i < oldWeapons.Count - 1; i++)
                {
                    oldWeapons[i].DropWeapon();
                }

                collision.GetComponent<PlayerJuggle>().ReplaceAllWeaponsWithAnotherWeapon(weaponPrefabToPickup);
            }

            else
            {
                //Debug.Log("Picked Up Weapon");
                collision.GetComponent<PlayerJuggle>().CreateAndAddWeaponToLoop(weaponPrefabToPickup);
            }


            Sound.instance.SoundRandomized(Sound.instance.equipNewWeapon, .8f);

            Destroy(gameObject);
        }
    }



}
