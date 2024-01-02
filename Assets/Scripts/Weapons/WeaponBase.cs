using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    WaterPistol,
    ConfettiGun,
    TrumpetGun,
    Heart,
    PopcornGun
}
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

    private Aim aim;
    private bool aimAgain;
    private GameObject player;  
    public PlayerUseWeaponInputStopper canUseWeaponChecker;

    private void Start()
    {
        canUseWeaponChecker = FindObjectOfType<PlayerUseWeaponInputStopper>();
        player = Manager.Instance.player.gameObject;
    }

    private void Update()
    {
        if (player == null && Manager.Instance.player.gameObject != null)
        {
            player = Manager.Instance.player.gameObject;
            aim = player.GetComponentInChildren<Aim>();
        }
        if (player != null)
        {
            aim = player.GetComponentInChildren<Aim>();

            if (canUseWeaponChecker == null)
            {
                canUseWeaponChecker = FindObjectOfType<PlayerUseWeaponInputStopper>();
            }
            if (canUseWeaponChecker == null)
            {
                canUseWeaponChecker = player.GetComponentInChildren<PlayerUseWeaponInputStopper>();
            }

            if (!weaponJuggleMovement.beingThrown && aimAgain == true)
            {
                AdjustAim();
            }

            fireCooldownTimer += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Mouse0) && fireCooldownTimer > fireCooldown && canUseWeaponChecker.isAbleToUseWeapon)
            {
                if (weaponEquipped)
                {
                    UseWeapon();

                    fireCooldownTimer = 0;
                }
            }
        }
    }

    public void EquipWeapon()
    {
        if (isHeart) { return; }

        //Sound.Instance.SoundSet(Sound.Instance.equipWeaponSounds, (int)weaponType);
        weaponEquipped = true;
        aimAgain = true;
    }

    public void UnEquipWeapon()
    {
        weaponEquipped = false;
    }

    public virtual void UseWeapon()
    {
        Score.Instance.bulletsShot++;
        aimAgain = false;
        int enumIndex = (int)weaponType;
        Sound.instance.SoundSet(Sound.instance.weaponShootingEnumOrder, enumIndex);

        StartCoroutine(nameof(ThrowUpWeaponWhenWeaponHasBeenFullyUsed), this.GetComponent<WeaponBase>());
    }

    IEnumerator ThrowUpWeaponWhenWeaponHasBeenFullyUsed(WeaponBase thisWeapon)
    {
        yield return new WaitForSeconds(_waitUntilThrowTime);

        if (weaponJuggleMovement.beingThrown) 
        { 
            yield break; 
        }

        if(player.GetComponent<PlayerJuggle>().weaponInHand = thisWeapon.weaponJuggleMovement)
        {
            FindObjectOfType<PlayerJuggle>().ThrowUpWeaponInHand();
        }
    }

    public virtual void AdjustAim()
    {
        transform.rotation = aim.bulletRotation;
    }

    public virtual void UpgradeWeapon()
    {
        currentWeaponLevel++;
    }

    public void ResetWeaponUpgradeLevel()
    {
        currentWeaponLevel = 0;

        SetWeaponUpgradeData();
    }

    public virtual void SetWeaponUpgradeData()
    {
        fireCooldown = currentWeaponBaseUpgradeData.weaponCooldown;
        _waitUntilThrowTime = currentWeaponBaseUpgradeData.waitUntilThrowTime;
    }


    public void ReplaceWeaponWithHeart()
    {
        UnEquipWeapon();
        isHeart = true;
        weaponSpriterenderer.enabled = false;
        heartSpriteRenderer.enabled = true;
        HealthBar.Instance.RemoveHeart(1);
    }

    public void ReplaceHeartWithWeapon()
    {
        EquipWeapon();
        isHeart = false;
        weaponSpriterenderer.enabled = true;
        heartSpriteRenderer.enabled = false;
        HealthBar.Instance.AddHeart(1);
    }


}

[System.Serializable]
public class WeaponBaseUpgradeData
{
    [Header("Weapon General Data")]
    [SerializeField] public float weaponCooldown;
    [SerializeField] public float waitUntilThrowTime;

}