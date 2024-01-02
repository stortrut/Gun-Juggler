using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    Vector3 knockback;
    public GameObject player;
    public static GameObject s_player;
    private bool died;
    private bool canTakeDamage = true;
    private CameraShake cameraShake;

    [SerializeField] SpriteRenderer[] allPlayerSprites;

    private void Awake()
    {
        maxHealth = health;
        s_player = gameObject;
        cameraShake = FindObjectOfType<CameraShake>();

        //Singleton.Instance = new Singleton(player);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            if (hasProtection == false && canTakeDamage)
            {
                ApplyDamage(1);
                StartCoroutine(nameof(FlashRed));
                CameraShakeRobert.instance.AddTrauma(0.1f);

                //Sound.Instance.SoundRandomized(Sound.Instance.playerTakingDamageSounds);
                Destroy(other.gameObject);
                AudienceSatisfaction.Instance.AudienceHappiness(-10);
                //gameObject.GetComponent<PlayerJuggle>().ReplaceRandomWeaponWithHeart();
                //Debug.Log(health);
            
            }
            else if (hasProtection == true)
            {
                //Sound.instance.SoundRandomized(Sound.instance.enemyNotTakingDamage);
            }

            //if (TryGetComponent(out Knockback knockbackComponent))
            //{
            //     Debug.Log("enemy knockback");

            //     knockback = new Vector3(10, 5, 0.2f);  
            //   // if(other.transform.position.x > transform.position.x)
            //   // {
            //     knockback.x *= -1;
            //   // }
            //     knockbackComponent.KnockBackMyself(knockback.x, knockback.y, knockback.z, transform.position);
            //}
        }
        else if (other.gameObject.CompareTag("Meelee"))
        {
            ApplyDamage(1);
            StartCoroutine(nameof(FlashRed));
            StartCoroutine(cameraShake.ShakingRandomly(.1f, .6f, .1f, 1));
           // gameObject.GetComponent<PlayerJuggle>().ReplaceRandomWeaponWithHeart();
        }
        if (health <= 0)
        {
            if (!died)
            {
                StartCoroutine(nameof(PlayerDied));
                died = true;
            }


        }
    }

    IEnumerator FlashRed()
    {
        for (int i = 0; i < allPlayerSprites.Length; i++)
        {
            allPlayerSprites[i].color = Color.red;
        }
        canTakeDamage = false;
        yield return new WaitForSeconds(0.23f);
        canTakeDamage = true;
        for (int i = 0; i < allPlayerSprites.Length; i++)
        {
            allPlayerSprites[i].color = Color.white;
        }
    }

    IEnumerator PlayerDied()
    {
        player.GetComponentInChildren<PlayerJuggle>().DropAllWeaponsOnGround();
        player.GetComponentInChildren<DeathAnimationHandler>().TriggerDeathAnimation();
        Sound.instance.SoundRandomized(Sound.instance.ohNo);
        //Debug.Log("Player Died");
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //SceneManager.LoadScene(0);
    }

    public void GivePlayerWeaponAndHealthBack()
    {
        player.GetComponent<PlayerJuggle>().ReplaceRandomHeartWithWeapon();
        health++;
        if(health > 3) { health = 3; }
    }
}
