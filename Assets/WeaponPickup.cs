using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] GameObject weaponPrefabToPickup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerJuggle>().AddWeaponToLoop(weaponPrefabToPickup);

            Sound.instance.SoundRandomized(Sound.instance.equipNewWeapon);

            Destroy(gameObject);
        }
    }



}
