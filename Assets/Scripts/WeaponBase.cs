using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public float Damage;
    public float Speed;
    public float FireRate;
    public GameObject Bullet;
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

