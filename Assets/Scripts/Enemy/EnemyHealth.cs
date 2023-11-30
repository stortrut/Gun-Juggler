using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : Health
{
    EnemyAnimator animatorScript;
    private void Start()
    {
        animatorScript = GetComponent<EnemyAnimator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            OnTrigger();
            if (hasProtection == false)
            { 
                ApplyDamage(1);
                Sound.Instance.SoundRandomized(Sound.Instance.enemyTakingDamageSounds);
                animatorScript.EnemyTakeDamage();
                if (health==0)
                {
                Death();
                }
                
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

    void Death()
    {
        EffectAnimations.Instance.ConfettiExplosion(this.gameObject.transform.position);
        Destroy(this.gameObject);
    }
}
    

