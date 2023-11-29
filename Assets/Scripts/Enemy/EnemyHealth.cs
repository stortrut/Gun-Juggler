using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : Health
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            OnTrigger();
            if (hasProtection == false)
            { 
                ApplyDamage(1);
                Sound.Instance.SoundRandomized(Sound.Instance.enemyTakingDamageSounds);
            }
            else if (hasProtection == true)
            {
                Sound.Instance.SoundRandomized(Sound.Instance.enemyNotTakingDamageSounds);
            }

            if (TryGetComponent(out Knockback knockbackComponent))
            {
                knockbackComponent.KnockBackMyself(15, 10, .4f, transform.position);
            }
        }
    }

    protected void EnemyTakeDamage()
    {

    }
}
    

