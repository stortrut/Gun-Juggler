using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{

    [SerializeField] public bool weaponEquipped;
    [SerializeField] protected float damage, bulletSpeed, fireRate;
    [SerializeField] protected SpriteRenderer spriterenderer;
    [SerializeField] public Rigidbody2D rb2D;
    [SerializeField] public Collider2D weaponCollider;

    public WeaponType weaponType;

    public GameObject bullet;
    public Transform gunPoint; 

    public void EquipWeapon()
    {
        if(weaponType == WeaponType.SmallGun)
        {

        }


        Sound.Instance.SoundSet(Sound.Instance.equipWeaponSounds, (int)weaponType);
        weaponEquipped = true;
    }

    public void UnEquipWeapon()
    {
        weaponEquipped = false;
    }

    public enum WeaponType
    {
        SmallGun,
        ShotGun
    }

}

