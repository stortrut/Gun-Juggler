using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class EnemyHealth : Health
{
    EnemyAnimator enemyAnimator;
    
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private HealthUI healthImage;
    private bool colorischanged;
    private bool dummy;
    private bool died;

    private void Awake()
    {
        enemyAnimator = GetComponent<EnemyAnimator>();
    }
    private void Start()
    {
        died = false;
        maxHealth = health;
        
        if (enemyAnimator != null )
        {
            dummy = true;   
        }
        else if (enemyAnimator == null)
        {
            dummy = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.gameObject.CompareTag("Bullet"))
        {
            UpgradeCombo.Instance.hitSinceShot = true;
            UpgradeCombo.Instance.comboTween.Kill();
            Destroy(other.gameObject);
            ColorChange(1);
            Invoke(nameof(ColorChange), 0.3f);
            if (hasProtection == false)
            { 
                ApplyDamage(1);
                healthImage.UpdateHealth(health, maxHealth);
                if (dummy)
                {
                    enemyAnimator.TakingDamage();
                }
                Sound.Instance.SoundRandomized(Sound.Instance.enemyTakingDamageSounds);
                if (health==0)
                {
                    if(died == true) { return; }

                    died = true;

                    FindObjectOfType<PlayerHealth>().GivePlayerWeaponAndHealthBack();
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
    private void ColorChange(int color)
    {
        if (colorischanged == false)
        {
            spriteRenderer.color = Color.red;
        }
        if (hasProtection == true)
        {
            spriteRenderer.color = Color.blue;
        }
        colorischanged = true;
    }
    private void ColorChange()
    {
        spriteRenderer.color = Color.white;
        colorischanged = false;
    }
    void Death()
    {
        if(enemyAnimator == null) { Debug.Log("ERROR did not find the enemyAnimator, every enemy has to have a enemyanimator in the art object and a enemyanimator script in logic"); }
        Vector2 positionForEffectAnimationScript = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + .5f);
        if (enemyAnimator.enemyType == EnemyType.Dummy)
        {
            Invoke(nameof(DummyDeath), 1f);
            enemyAnimator.Dying();
        }
        else if (enemyAnimator.enemyType == EnemyType.Giraffe)
        {
            EffectAnimations.Instance.BalloonPop(positionForEffectAnimationScript);
            Destroy(gameObject);
        }
        else if (enemyAnimator.enemyType == EnemyType.PieClown)
        {
            Invoke(nameof(ClownDeath), 1.5f);
            enemyAnimator.Dying();
        }
    }

    //IEnumerator WaitSeconds(float secondsToWait)
    //{
    //    yield return new WaitForSeconds(secondsToWait);
    //}

    void DummyDeath()
    {
        Vector2 positionForEffectAnimationScript = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + .5f);
        EffectAnimations.Instance.EnemyPoof(positionForEffectAnimationScript);
        Destroy(gameObject);
    }

    void ClownDeath()
    {
        Vector2 positionForEffectAnimationScript = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + .5f);
        EffectAnimations.Instance.BalloonPop(positionForEffectAnimationScript);
        Destroy(gameObject);
    }
}
    

