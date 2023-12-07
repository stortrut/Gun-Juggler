using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    [Header("References")]
    [HideInInspector] public bool weaponEquipped;
    [SerializeField] public float fireCooldown;
    [SerializeField] protected float fireCooldownTimer;
    [SerializeField] protected SpriteRenderer spriterenderer;
    [SerializeField] public Rigidbody2D rb2D;
    [SerializeField] public Collider2D weaponCollider;

    [HideInInspector] public WeaponType weaponType;

    public GameObject bullet;
    public Transform gunPoint;

    [SerializeField] protected int currentWeaponLevel = 0;

    [HideInInspector] public WeaponBaseUpgradeData currentWeaponBaseUpgradeData;

    private AutoAim autoAim;
    private GameObject player;

    private void Update()
    {
        if (player == null)
        {
            player = Manager.Instance.player;
            autoAim = player.GetComponentInChildren<AutoAim>();
        }


        AdjustAim();


        fireCooldownTimer += Time.deltaTime;
        if (Input.GetMouseButton(0) && fireCooldownTimer > fireCooldown)
        {
            if (weaponEquipped)
            {
                UseWeapon();

                fireCooldownTimer = 0;
            }
        }
    }

    public void EquipWeapon()
    {
        //Sound.Instance.SoundSet(Sound.Instance.equipWeaponSounds, (int)weaponType);
        weaponEquipped = true;
    }

    public void UnEquipWeapon()
    {
        weaponEquipped = false;
    }

    public virtual void UseWeapon()
    {

    }

    public virtual void AdjustAim()
    {
        transform.rotation = autoAim.bulletRotation;
    }

    public virtual void UpgradeWeapon()
    {
        currentWeaponLevel++;
    }


    public virtual void SetWeaponUpgradeData()
    {
        fireCooldown = currentWeaponBaseUpgradeData.weaponCooldown;
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
    [Header("Weapon General Data")]
    [SerializeField] public float weaponCooldown;
}