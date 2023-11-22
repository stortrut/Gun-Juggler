using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public float damage;
    public float bulletSpeed;
    public float fireRate;
    public GameObject bullet;
    public Transform gunPoint; 
    public bool weaponEquipped { get; set; }

    public void WeaponEquipped()
    {
        weaponEquipped = true;
    }

    public void WeaponNotEquipped()
    {
        weaponEquipped = false;
    }
}

