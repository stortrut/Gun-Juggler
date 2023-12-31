using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuggleCatchCircle : MonoBehaviour
{
    [SerializeField] bool turnOffUpgradeFunction;

    [Header("Catch Values")]
    [SerializeField] float maxCatchAttemptCooldown = 0.4f;
    float currentCatchAttemptCooldownTimer;
    bool catchOnCoolDown;

    [SerializeField] float catchTimeWindow = 0.7f;


    [Header("Catch Circle Colors")]
    [SerializeField] Color canCatchWeaponColor;
    [SerializeField] Color waitForWeaponColor;
    [SerializeField] Color caughtWeaponColor;

    [Header("References")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] ParticleSystem gunCaughtEffect;


    private PlayerJuggle playerJuggle;
    private WeaponJuggleMovement currentCatchableGun;
    private bool canCatchWeapon;
    private bool caughtWeapon;

    private void Start()
    {
        playerJuggle = GetComponentInParent<PlayerJuggle>();

        spriteRenderer.color = waitForWeaponColor;

        currentCatchAttemptCooldownTimer = maxCatchAttemptCooldown;
    }

    [System.Obsolete]
    private void Update()
    {
        if (turnOffUpgradeFunction) { return; }

        if (Input.GetKeyDown(KeyCode.Mouse1)) //|| Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.LeftShift)
        {
            if (canCatchWeapon && !caughtWeapon && !catchOnCoolDown/* !currentCatchableGun.beingDropped*/)
            {
                gunCaughtEffect.Play();
                //Sound.Instance.SoundRandomized(Sound.Instance.catchingWeaponSounds);
                spriteRenderer.color = caughtWeaponColor;
                caughtWeapon = true;

                FindObjectOfType<HudPopcornFill>().PopcornAmountUpgrade();
            }

            catchOnCoolDown = true;
            currentCatchAttemptCooldownTimer = 0;
        }

        if (catchOnCoolDown)
        {
            if (currentCatchAttemptCooldownTimer < maxCatchAttemptCooldown)
            {
                currentCatchAttemptCooldownTimer += Time.deltaTime;
            }
            else
            {
                catchOnCoolDown = false;
                currentCatchAttemptCooldownTimer = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "WeaponBeingJuggled")
        {
            currentCatchableGun = collision.GetComponent<WeaponJuggleMovement>();

            StopCoroutine(nameof(CaughtWeaponCheckHandler));
            StartCoroutine(nameof(CaughtWeaponCheckHandler));
        }
    }

    public bool caughtGun = false;


    IEnumerator CaughtWeaponCheckHandler()
    {
        canCatchWeapon = true;
        spriteRenderer.color = canCatchWeaponColor;


        yield return new WaitForSeconds(0.1f);
        playerJuggle.ThrowUpWeaponInHand();
        playerJuggle.armAnimationHandler.StartCoroutine(nameof(playerJuggle.armAnimationHandler.PlayCatchWeaponAnimation));
        yield return new WaitForSeconds(catchTimeWindow - 0.1f);


        if (caughtWeapon)
        {
            Sound.Instance.SoundRandomized(Sound.Instance.equipNewWeapon, 0.4f, 1f);

            if(currentCatchableGun != null && currentCatchableGun.gameObject.TryGetComponent<WeaponBase>(out WeaponBase gun) == true)
                gun.UpgradeWeapon();
            currentCatchAttemptCooldownTimer = maxCatchAttemptCooldown;

            caughtGun = true;

            yield return new WaitForSeconds(0.3f);
        }
        else
        {
            if(currentCatchableGun != null && currentCatchableGun.gameObject.TryGetComponent<WeaponBase>(out WeaponBase gun) == true)
               gun.ResetWeaponUpgradeLevel();
                


            //playerJuggle.RemoveWeaponFromLoop(currentCatchableGun);
            //Sound.Instance.SoundRandomized(Sound.Instance.notCatchingWeaponSounds);
            //currentCatchableGun.DropWeapon();
        }

        spriteRenderer.color = waitForWeaponColor;
        caughtWeapon = false;
        canCatchWeapon = false;
    }


}
