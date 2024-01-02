using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] GameObject weaponPrefabToPickup;

    [SerializeField] bool replaceAllPlayerWeaponsWithThisWeapon;
    [SerializeField] bool andAlsoAddAWeaponIfPlayerHasNone;


    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (replaceAllPlayerWeaponsWithThisWeapon)
            {
                if (andAlsoAddAWeaponIfPlayerHasNone && collision.GetComponent<PlayerJuggle>().weaponsCurrentlyInJuggleLoop.Count < 1)
                {
                    Debug.Log("Player Had No Weapons So we added one because the option was chosen");
                    collision.GetComponent<PlayerJuggle>().CreateAndAddWeaponToLoop(weaponPrefabToPickup);
                }
                else
                {
                    collision.GetComponent<PlayerJuggle>().ReplaceAllWeaponsWithAnotherWeapon(weaponPrefabToPickup);
                }
            }
            else
            {
                //Debug.Log("Picked Up Weapon");
                collision.GetComponent<PlayerJuggle>().CreateAndAddWeaponToLoop(weaponPrefabToPickup);
            }


            Sound.instance.SoundRandomized(Sound.instance.equipNewWeapon);

            Destroy(gameObject);
        }
    }



}
