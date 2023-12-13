using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    [Header("References")]
    [HideInInspector] public bool weaponEquipped;
    [HideInInspector] public float fireCooldown;
    [HideInInspector] protected float fireCooldownTimer;
    [HideInInspector] public float _waitUntilThrowTime;


    [SerializeField] protected SpriteRenderer weaponSpriterenderer;
    [SerializeField] public Rigidbody2D rb2D;
    [SerializeField] public Collider2D weaponCollider;
    [SerializeField] protected SpriteRenderer heartSpriteRenderer;
    [SerializeField] WeaponJuggleMovement weaponJuggleMovement;






    [HideInInspector] public bool isHeart;
 

    [HideInInspector] public WeaponType weaponType;

    public GameObject bullet;
    public Transform gunPoint;

    [SerializeField] protected int currentWeaponLevel = 0;

    [HideInInspector] public WeaponBaseUpgradeData currentWeaponBaseUpgradeData;

    private AutoAim autoAim;
    private Aim aim;
    private GameObject player;  
    public PlayerUseWeaponInputStopper canUseWeaponChecker;
    
    private void Start()
    {
        canUseWeaponChecker = FindObjectOfType<PlayerUseWeaponInputStopper>();
        player = Manager.Instance.player;
    }


    private void Update()
    {
        if (player == null)
        {
            player = Manager.Instance.player;
            //autoAim = player.GetComponentInChildren<AutoAim>();
            aim = player.GetComponentInChildren<Aim>();
        }
        aim = player.GetComponentInChildren<Aim>();

        if (canUseWeaponChecker == null)
        {
            canUseWeaponChecker = FindObjectOfType<PlayerUseWeaponInputStopper>();
        }
        if (canUseWeaponChecker == null)
        {
            canUseWeaponChecker = player.GetComponentInChildren<PlayerUseWeaponInputStopper>();
        }

        if (!weaponJuggleMovement.beingThrown)
        {
            AdjustAim();
        }

        fireCooldownTimer += Time.deltaTime;
        if (Input.GetMouseButton(0) && fireCooldownTimer > fireCooldown && canUseWeaponChecker.isAbleToUseWeapon)
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
        if (isHeart) { return; }

        //Sound.Instance.SoundSet(Sound.Instance.equipWeaponSounds, (int)weaponType);

        weaponEquipped = true;
    }

    public void UnEquipWeapon()
    {
        weaponEquipped = false;
    }

    public virtual void UseWeapon()
    {
        StartCoroutine(nameof(ThrowUpWeaponWhenWeaponHasBeenFullyUsed), this.GetComponent<WeaponBase>());
    }

    IEnumerator ThrowUpWeaponWhenWeaponHasBeenFullyUsed(WeaponBase thisWeapon)
    {
        yield return new WaitForSeconds(_waitUntilThrowTime);

        Debug.Log("Waited " + _waitUntilThrowTime + " seconds before threw");

        if(player.GetComponent<PlayerJuggle>().weaponInHand = thisWeapon.weaponJuggleMovement)
        {
            GetComponentInParent<PlayerJuggle>().ThrowUpWeaponInHand();
        }
    }



    public virtual void AdjustAim()
    {
        transform.rotation = aim.bulletRotation;
        //transform.rotation = autoAim.bulletRotation;

    }

    public virtual void UpgradeWeapon()
    {
        currentWeaponLevel++;
    }


    public virtual void SetWeaponUpgradeData()
    {
        fireCooldown = currentWeaponBaseUpgradeData.weaponCooldown;
        _waitUntilThrowTime = currentWeaponBaseUpgradeData.waitUntilThrowTime;

        Debug.Log("This is: " + weaponType);

        Debug.Log("The upgrade data waitthrow time wwas " + currentWeaponBaseUpgradeData.waitUntilThrowTime);

        Debug.Log("Set waitTIme to " + _waitUntilThrowTime);

    }


    public void ReplaceWeaponWithHeart()
    {
        UnEquipWeapon();
        isHeart = true;
        weaponSpriterenderer.enabled = false;
        heartSpriteRenderer.enabled = true;
    }

    public void ReplaceHeartWithWeapon()
    {
        EquipWeapon();
        isHeart = false;
        weaponSpriterenderer.enabled = true;
        heartSpriteRenderer.enabled = false;
    }


    public enum WeaponType
    {
        SmallGun,
        ShotGun,
        StunGun,
        Heart
    }
}

[System.Serializable]
public class WeaponBaseUpgradeData
{
    [Header("Weapon General Data")]
    [SerializeField] public float weaponCooldown;
    [SerializeField] public float waitUntilThrowTime;

}