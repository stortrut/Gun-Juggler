using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    WaterPistol,
    ConfettiGun,
    TrumpetGun,
    PopcornGun
}
public class WeaponBase : MonoBehaviour
{
    [Header("References")]
    [ReadOnly] public bool weaponEquipped = false;
    [HideInInspector] public float fireCooldown;
    [HideInInspector] protected float fireCooldownTimer;
    [HideInInspector] public float _waitUntilThrowTime;


    [SerializeField] protected SpriteRenderer weaponSpriterenderer;
    [SerializeField] public Rigidbody2D rb2D;
    [SerializeField] public Collider2D weaponCollider;
    [SerializeField] protected SpriteRenderer heartSpriteRenderer;
    [SerializeField] WeaponJuggleMovement weaponJuggleMovement;

    [HideInInspector] public bool isHeart;
 

    public WeaponType weaponType;

    public GameObject bullet;
    public Transform gunPoint;

    [SerializeField] protected int currentWeaponLevel = 0;

    [HideInInspector] public WeaponBaseUpgradeData currentWeaponBaseUpgradeData;

    public Aim aim;
    public bool aimAgain = true;
    public GameObject player;  
    public PlayerUseWeaponInputStopper canUseWeaponChecker;

    private void Start()
    {
        canUseWeaponChecker = FindObjectOfType<PlayerUseWeaponInputStopper>();
        player = Manager.Instance.player.gameObject;
        //UnEquipWeapon();
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
            else if(!weaponJuggleMovement.beingThrown && player.GetComponent<PlayerJuggle>().pauseJuggling)
            {
                AdjustAim();
            }

            fireCooldownTimer += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Mouse0) && fireCooldownTimer > fireCooldown && canUseWeaponChecker.isAbleToUseWeapon)
            {
                if (weaponEquipped && !player.GetComponent<PlayerJuggle>().pauseJuggling)
                {
                    if (FindObjectOfType<PlayerJuggle>().canNotUseWeapons) { Debug.Log("Can not use weapon"); return; }

                    UseWeapon();

                    fireCooldownTimer = 0;
                }
            }
        }
    }

    public void EquipWeapon()
    {
        if (isHeart) { return; }
        if (weaponJuggleMovement.beingThrown) { return; }

        //Sound.Instance.SoundSet(Sound.Instance.equipWeaponSounds, (int)weaponType);

        fireCooldownTimer = fireCooldown * 1.1f;

        StopCoroutine(nameof(ThrowUpWeaponWhenWeaponHasBeenFullyUsed));


        weaponEquipped = true;
        aimAgain = true;
    }

    public void UnEquipWeapon()
    {
        weaponEquipped = false;
    }

    public virtual void UseWeapon()
    {
        

        if(weaponType != WeaponType.PopcornGun)
            aimAgain = false;
        int enumIndex = (int)weaponType;

        if (Score.Instance != null)
            Sound.Instance.SoundSet(Sound.Instance.weaponShootingEnumOrder, enumIndex, 1, .15f);

        StartCoroutine(nameof(ThrowUpWeaponWhenWeaponHasBeenFullyUsed), this.GetComponent<WeaponBase>());
    }

    IEnumerator ThrowUpWeaponWhenWeaponHasBeenFullyUsed(WeaponBase thisWeapon)
    {
        yield return new WaitForSeconds(_waitUntilThrowTime);

        fireCooldownTimer = fireCooldown * 1.1f;

        if (weaponJuggleMovement.beingThrown) 
        {
            yield break; 
        }

        if(player.GetComponent<PlayerJuggle>().weaponInHand == thisWeapon.weaponJuggleMovement)
        {
            FindObjectOfType<PlayerJuggle>().ThrowUpWeaponInHand(); 
            StopCoroutine(nameof(ThrowUpWeaponWhenWeaponHasBeenFullyUsed));
        }
    }

    public virtual void AdjustAim()
    {
        transform.rotation = aim.bulletRotation;
    }

    public virtual void UpgradeWeapon()
    {
        currentWeaponLevel++;
        if(currentWeaponLevel > 1) { currentWeaponLevel = 1; }
        if (currentWeaponLevel < 0) { currentWeaponLevel = 0; }

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




        if(currentWeaponBaseUpgradeData.stage1Art == null) { return; }

        if(currentWeaponLevel == 0)
        {
            currentWeaponBaseUpgradeData.stage1Art.SetActive(true);
            currentWeaponBaseUpgradeData.stage1Outline.SetActive(true);

            currentWeaponBaseUpgradeData.stage2Art.SetActive(false);
            currentWeaponBaseUpgradeData.stage2Outline.SetActive(false);
        }
        else
        {
            currentWeaponBaseUpgradeData.stage2Art.SetActive(true);
            currentWeaponBaseUpgradeData.stage2Outline.SetActive(true);

            currentWeaponBaseUpgradeData.stage1Art.SetActive(false);
            currentWeaponBaseUpgradeData.stage1Outline.SetActive(false);
        }
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

    [SerializeField] public GameObject stage1Art;
    [SerializeField] public GameObject stage1Outline;

    [SerializeField] public GameObject stage2Art;
    [SerializeField] public GameObject stage2Outline;
}