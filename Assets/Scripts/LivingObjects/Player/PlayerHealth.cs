using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    Vector3 knockback;
    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            if (hasProtection == false)
            {
                
                ApplyDamage(1);
                Sound.Instance.SoundRandomized(Sound.Instance.playerTakingDamageSounds);
                Destroy(other.gameObject);

            }
            else if (hasProtection == true)
            {
                Sound.Instance.SoundRandomized(Sound.Instance.enemyNotTakingDamageSounds);
            }

            if (TryGetComponent(out Knockback knockbackComponent))
            {
                Debug.Log("enemy knockback");
              
                 knockback=new Vector3(10, 5, 0.2f);  
               // if(other.transform.position.x > transform.position.x)
               // {
                    knockback.x *= -1;
               // }
                knockbackComponent.KnockBackMyself(knockback.x, knockback.y, knockback.z, transform.position);
            }
        }
    }

}
