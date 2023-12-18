using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.ShaderGraph.Internal;
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
    public bool died;

    private void Awake()
    {
        maxHealth = health;
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
                var damage = other.gameObject.GetComponent<Bullet>().bulletDamage;

                if(other.gameObject.GetComponent<Bullet>() == null) { damage = other.gameObject.GetComponentInChildren<Bullet>().bulletDamage; }
                
                if (TryGetComponent(out Knockback knockbackComponent))
                {
                    if (other.gameObject.TryGetComponent<Bullet>(out Bullet bulletScript))
                    {
                        float knockbackSpeed = bulletScript.bulletSpeed;
                        knockbackComponent.KnockBackMyself(knockbackSpeed, knockbackSpeed/5, .2f, other.transform.position);
                    }
                }

                ApplyDamage(damage);
                if(healthImage != null)
                healthImage.UpdateHealth(health, maxHealth);
                if (dummy)
                {
                    enemyAnimator.TakingDamage();
                }

                Sound.Instance.SoundRandomized(Sound.Instance.enemyTakingDamageSounds);

                if (health <= 0)
                {
                    if (died == true) { return; }

                    died = true;

                    Death();
                }
            }
            else if (hasProtection == true)
            {
                Sound.Instance.SoundRandomized(Sound.Instance.enemyNotTakingDamageSounds);
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
    public override void Death()
    {
        FindObjectOfType<PlayerHealth>().GivePlayerWeaponAndHealthBack();
        Vector2 positionForEffectAnimationScript = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + .5f);
        if (enemyAnimator == null)
        {
            Debug.Log("ERROR did not find the enemyAnimator, every enemy has to have a enemyanimator in the art object and a enemyanimator script in logic");
        }
         
        else if (enemyAnimator.enemyType == EnemyType.Dummy)
        {
            Invoke(nameof(DummyDeath), 1f); 
            enemyAnimator.Dying();
        }
        else if (enemyAnimator.enemyType == EnemyType.Giraffe)
        {
            EffectAnimations.Instance.BalloonPop(positionForEffectAnimationScript);
            Sound.Instance.SoundSet(Sound.Instance.balloonPop, 0);
            Destroy(gameObject);
        }
        else if (enemyAnimator.enemyType == EnemyType.PieClown)
        {
            Invoke(nameof(ClownDeath), 1.5f);
            enemyAnimator.Dying();
        }
    }

    void DummyDeath()
    {
        Vector2 positionForEffectAnimationScript = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + .5f);
        EffectAnimations.Instance.EnemyPoof(positionForEffectAnimationScript);
        //Sound.Instance.SoundSet(Sound.Instance.pof, 0);
        Destroy(gameObject);
    }

    void ClownDeath()
    {
        Vector2 positionForEffectAnimationScript = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + .5f);
        EffectAnimations.Instance.EnemyPoof(positionForEffectAnimationScript);
        //Sound.Instance.SoundSet(Sound.Instance.pof, 0);
        Destroy(gameObject);
    }
}
    

