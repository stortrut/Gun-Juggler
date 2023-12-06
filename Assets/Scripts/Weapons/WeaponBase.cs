using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    [Header("References")]
    [HideInInspector] public bool weaponEquipped;
    [HideInInspector] public float fireCooldown;
    [HideInInspector] protected float fireCooldownTimer;
    [SerializeField] protected SpriteRenderer spriterenderer;
    [SerializeField] public Rigidbody2D rb2D;
    [SerializeField] public Collider2D weaponCollider;

    [HideInInspector] public WeaponType weaponType;

    public GameObject bullet;
    public Transform gunPoint;

    [HideInInspector] protected int currentWeaponLevel = 0;

    [HideInInspector] public WeaponBaseUpgradeData currentWeaponBaseUpgradeData;



    private void Update()
    {
        fireCooldownTimer += Time.deltaTime;
        if (Input.GetMouseButton(0) && fireCooldownTimer > fireCooldown)
        {
            if (weaponEquipped)
            {
                Debug.Log("Shoot");

                UseWeapon();

                fireCooldownTimer = 0;
            }
        }
    }

    public void EquipWeapon()
    {
        Sound.Instance.SoundSet(Sound.Instance.equipWeaponSounds, (int)weaponType);
        weaponEquipped = true;
    }

    public void UnEquipWeapon()
    {
        weaponEquipped = false;
    }

    public virtual void UseWeapon()
    {
        Debug.Log("You Used Weapon /From weapon base");
    }



    public virtual void UpgradeWeapon()
    {
        currentWeaponLevel++;
    }


    public virtual void SetWeaponUpgradeData()
    {

        fireCooldown = currentWeaponBaseUpgradeData.weaponCooldown;

        Debug.Log("Weapon base upgrade data is set");
    }






    public enum WeaponType
    {
        SmallGun,
        ShotGun,
        StunGun
    }
}

[System.Serializable]
public class WeaponBaseUpgradeData
{
    [SerializeField] public float weaponCooldown;
}