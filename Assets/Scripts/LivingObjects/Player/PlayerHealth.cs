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
        s_player = player;
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
                StartCoroutine(cameraShake.ShakingRandomly(.1f, .6f, .1f, 1));

                //Sound.Instance.SoundRandomized(Sound.Instance.playerTakingDamageSounds);
                Destroy(other.gameObject);

                player.GetComponent<PlayerJuggle>().ReplaceRandomWeaponWithHeart();
                //Debug.Log(health);
                if(health <= 0)
                {
                    if(!died)
                    {
                        StartCoroutine(nameof(PlayerDied));
                        died = true;
                    }
                    

                } 
            }
            else if (hasProtection == true)
            {
                Sound.Instance.SoundRandomized(Sound.Instance.enemyNotTakingDamageSounds);
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
        Sound.Instance.SoundRandomized(Sound.Instance.notCatchingWeaponSounds);
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
