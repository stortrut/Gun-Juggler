using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            if (hasProtection == false)
            {
                ApplyDamage(1);
                Sound.Instance.SoundRandomized(Sound.Instance.playerTakingDamageSounds);

            }
            else if (hasProtection == true)
            {
                Sound.Instance.SoundRandomized(Sound.Instance.enemyNotTakingDamageSounds);
            }

            if (TryGetComponent(out Knockback knockbackComponent))
            {
                Debug.Log("enemy knockback");
                knockbackComponent.KnockBackMyself(15, 10, .4f, transform.position);
            }
        }
    }

}
