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

    public AutoAim autoAim;
    


    private void Start()
    {
        //autoAim = GetComponentInParent<PlayerMovement>().gameObject.GetComponentInChildren<AutoAim>();
        if(autoAim == null) { Debug.Log("ERRROROROROOROR"); }
    }



    private void Update()
    {
        if (autoAim == null) { Debug.Log("ERRROROROROOROR"); }
        if (autoAim.bulletRotation == null) { Debug.Log("ERRROROROROOROR"); }


        this.transform.rotation = autoAim.bulletRotation;



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