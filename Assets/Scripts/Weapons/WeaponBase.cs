using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{

    [SerializeField] public bool weaponEquipped;
    [SerializeField] protected float damage, bulletSpeed, fireRate;
    [SerializeField] protected SpriteRenderer spriterenderer;

    public GameObject bullet;
    public Transform gunPoint; 

    public void EquipWeapon()
    {
        weaponEquipped = true;
    }

    public void UnEquipWeapon()
    {
        weaponEquipped = false;
    }
}

