using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    Vector3 knockback;
    public GameObject player;
    public static GameObject s_player;

    private void Awake()
    {
        s_player = player;

        //Singleton.Instance = new Singleton(player);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            if (hasProtection == false)
            {
                ApplyDamage(1);
                //Sound.Instance.SoundRandomized(Sound.Instance.playerTakingDamageSounds);
                Destroy(other.gameObject);

                player.GetComponent<PlayerJuggle>().ReplaceRandomWeaponWithHeart();

                if(health <= 0)
                {
                    StartCoroutine(nameof(PlayerDied));
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


    IEnumerator PlayerDied()
    {
        Debug.Log("Player Died");
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene(0);
    }
}
