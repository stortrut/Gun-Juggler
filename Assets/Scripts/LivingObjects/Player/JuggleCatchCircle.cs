using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuggleCatchCircle : MonoBehaviour
{
    [Header("Catch Values")]
    [SerializeField] float maxCatchAttemptCooldown = 0.4f;
    float currentCatchAttemptCooldownTimer;
    bool catchOnCoolDown;

    [SerializeField] float catchTimeWindow = 0.85f;


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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (canCatchWeapon && !caughtWeapon && !catchOnCoolDown/* !currentCatchableGun.beingDropped*/)
            {
                gunCaughtEffect.Play();
                Sound.Instance.SoundRandomized(Sound.Instance.catchingWeaponSounds);
                spriteRenderer.color = caughtWeaponColor;
                caughtWeapon = true;
                playerJuggle.armAnimationHandler.StartCoroutine(nameof(playerJuggle.armAnimationHandler.PlayCatchWeaponAnimation));
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
  
    IEnumerator CaughtWeaponCheckHandler()
    {
        canCatchWeapon = true;
        spriteRenderer.color = canCatchWeaponColor;

        yield return new WaitForSeconds(catchTimeWindow);

        if (caughtWeapon)
        {
            //Upgrade weapon here
            yield return new WaitForSeconds(0.3f);
        }
        else
        {
            Debug.Log("Failed Perfect Catch");

            //playerJuggle.RemoveWeaponFromLoop(currentCatchableGun);
            //Sound.Instance.SoundRandomized(Sound.Instance.notCatchingWeaponSounds);
            //currentCatchableGun.DropWeapon();
        }
        spriteRenderer.color = waitForWeaponColor;
        caughtWeapon = false;
        canCatchWeapon = false;
    }


}
