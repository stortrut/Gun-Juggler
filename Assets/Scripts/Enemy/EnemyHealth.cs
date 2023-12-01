using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : Health
{
    EnemyAnimator animatorScript;
    private bool dummy;
    private void Start()
    {
        animatorScript = GetComponent<EnemyAnimator>();
        if (animatorScript != null )
        {
            dummy = true;   
        }
        else if (animatorScript == null)
        {
            dummy = false;
        }
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
                if (dummy)
                {
                    animatorScript.EnemyTakeDamage();
                }
                Sound.Instance.SoundRandomized(Sound.Instance.enemyTakingDamageSounds);
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
        Vector2 animPos = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y + .5f);
        Debug.Log(this.dummy);
        if (this.dummy == true)
        {
            Debug.Log("dummy, not balloon");
            //StartCoroutine("WaitSeconds", 2f);

            Invoke(nameof(DummyDeath), 1f);
            animatorScript.EnemyDying();
        }
        else if (this.dummy == false) 
        {
            Debug.Log(this.dummy+"igen?");
            Debug.Log("balloon");
            EffectAnimations.Instance.BalloonPop(animPos);
            Destroy(this.gameObject);
        }
    }

    //IEnumerator WaitSeconds(float secondsToWait)
    //{
    //    yield return new WaitForSeconds(secondsToWait);
    //}

    void DummyDeath()
    {
        Vector2 animPos = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y - .5f);
        EffectAnimations.Instance.EnemyPoof(animPos);
        Destroy(this.gameObject);
    }
}
    

