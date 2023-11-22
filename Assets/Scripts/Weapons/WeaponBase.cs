using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{

    [SerializeField] protected bool weaponEquipped;
    [SerializeField] protected float damage, bulletSpeed, fireRate;
    [SerializeField] protected SpriteRenderer spriterenderer;

    public GameObject bullet;
    public Transform gunPoint; 

    public void WeaponEquipped()
    {
        weaponEquipped = true;
    }

    public void WeaponNotEquipped()
    {
        weaponEquipped = false;
    }
}

