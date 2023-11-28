using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuggleCatchCircle : MonoBehaviour
{
    [SerializeField] float catchTime = 0.85f;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Color canCatchWeaponColor;
    [SerializeField] Color waitForWeaponColor;
    [SerializeField] Color caughtWeaponColor;
    [SerializeField] ParticleSystem gunCaughtEffect;


    private PlayerJuggle playerJuggle;
    private WeaponJuggleMovement currentCatchableGun;
    private bool canCatchWeapon;
    private bool caughtWeapon;

    private void Start()
    {
        playerJuggle = GetComponentInParent<PlayerJuggle>();

        spriteRenderer.color = waitForWeaponColor;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (canCatchWeapon && !caughtWeapon && !currentCatchableGun.beingDropped)
            {
                gunCaughtEffect.Play();
                spriteRenderer.color = caughtWeaponColor;
                caughtWeapon = true;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "WeaponBeingJuggled")
        {
            currentCatchableGun = collision.GetComponent<WeaponJuggleMovement>();

            StopCoroutine(nameof(CanCatchWeapon));
            StartCoroutine(nameof(CanCatchWeapon));
        }
    }
  
    IEnumerator CanCatchWeapon()
    {
        canCatchWeapon = true;
        spriteRenderer.color = canCatchWeaponColor;

        yield return new WaitForSeconds(catchTime);

        if (caughtWeapon)
        {
            yield return new WaitForSeconds(0.3f);
        }
        else
        {
            playerJuggle.RemoveWeaponFromLoop(currentCatchableGun);
            currentCatchableGun.DropWeapon();
        }
        spriteRenderer.color = waitForWeaponColor;
        caughtWeapon = false;
        canCatchWeapon = false;

    }


}
