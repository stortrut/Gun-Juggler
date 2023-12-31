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
    private Vector2 positionForEffectAnimationScript;
    private bool colorischanged;
    private bool dummy;
    public bool hasDied;
    private IStunnable[] stunnable;

    private void Awake()
    {
        maxHealth = health;
        enemyAnimator = GetComponent<EnemyAnimator>();
        positionForEffectAnimationScript = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + .5f);
    }
    private void Start()
    {
        hasDied = false;
        maxHealth = health;
        
        if (enemyAnimator != null )
        {
           //var enemyType = enemyAnimator.enemyType;   
        }
        else if (enemyAnimator == null)
        {
            //dummy = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.gameObject.CompareTag("Bullet"))
        {
            Score.Instance.pieClownHit++;
            int enumIndex = (int)enemyAnimator.enemyType;
            Sound.Instance.SoundSet(Sound.Instance.enemyTakingDamageEnumOrder, enumIndex, 0.1f,.2f);
            // UpgradeCombo.Instance.hitSinceShot = true;
            // UpgradeCombo.Instance.comboTween.Kill();
            //Destroy(other.gameObject);
            ColorChange(1);
           // Invoke(nameof(ColorChange), 0.3f);

            if (hasProtection == false)
            {
                var damage = other.gameObject.GetComponent<Bullet>().bulletDamage;

                if(other.gameObject.GetComponent<Bullet>() == null) { damage = other.gameObject.GetComponentInChildren<Bullet>().bulletDamage; }
                
                if (TryGetComponent(out Knockback knockbackComponent))
                {
                    if (other.gameObject.TryGetComponent<Bullet>(out Bullet bulletScript))
                    {
                        float knockbackSpeed = bulletScript.bulletSpeed;
                        if (stunnable == null)
                        {
                            stunnable = GetComponents<IStunnable>();
                        }
                        knockbackComponent.KnockBackMyself(knockbackSpeed, knockbackSpeed/5, .2f, other.transform.position);
                    }
                }

                ApplyDamage(damage);
                if(healthImage != null)
                healthImage.UpdateHealth(health, maxHealth);
                if (enemyAnimator.enemyType == EnemyType.Dummy)
                {
                    enemyAnimator.TakingDamage();
                }

                //int enumIndex = (int)enemyAnimator.enemyType;
                //Sound.instance.SoundSet(Sound.instance.enemyTakingDamageEnumOrder, enumIndex, 2f);

                if (health <= 0)
                {
                    if (hasDied == true) { return; }

                    hasDied = true;

                   // Death();
                }
            }
            else if (hasProtection == true)
            {
                //int enumIndex = (int)enemyAnimator.enemyType;
                //Sound.instance.SoundSet(Sound.instance.enemyNotTakingDamageEnumOrder, enumIndex);
            }
        }
    }
    private void ColorChange(int color)
    {
        //if (colorischanged == false)
        //{
        //    spriteRenderer.color = new Color (1,1,1, (health/maxHealth) * (health / maxHealth));
        //}
        //if (hasProtection == true)
        //{
        //    spriteRenderer.color = Color.blue;  
        //}
        //colorischanged = true;
    }
    private void ColorChange()
    {
        spriteRenderer.color = Color.white;
        colorischanged = false;
    }
    public override void Death()
    {
        FindObjectOfType<PlayerHealth>().GivePlayerWeaponAndHealthBack();
        if (enemyAnimator == null)
        {
            Debug.Log("ERROR did not find the enemyAnimator, every enemy has to have a enemyanimator in the art object and a enemyanimator script in logic");
            return;
        }

        else if (enemyAnimator.enemyType == EnemyType.Dummy)
        {
            Invoke(nameof(DummyDeath), 1f);
            //CameraShakeRobert.instance.AddTrauma(0.2f);
            enemyAnimator.Dying();
        }
        else if (enemyAnimator.enemyType == EnemyType.Giraffe)
        {
            Destroy(gameObject);
            //CameraShakeRobert.instance.AddTrauma(0.2f);
            EffectAnimations.Instance.BalloonPop(positionForEffectAnimationScript);
            Sound.Instance.SoundSet(Sound.Instance.balloonPop, 0);

        }
        else if (enemyAnimator.enemyType == EnemyType.PieClown)
        {
            Invoke(nameof(ClownDeath), 1.5f);
            Sound.Instance.SoundSet(Sound.Instance.enemyTakingDamageEnumOrder, 5,1f,.1f);
            CameraShakeRobert.instance.AddTrauma(0.3f);
            enemyAnimator.Dying();
            Score.Instance.pieClownHit++;
        }
    }

    void DummyDeath()
    {
        //CameraShakeRobert.instance.AddTrauma(0.1f);
        Vector2 positionForEffectAnimationScript = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + .5f);
        EffectAnimations.Instance.EnemyPoof(positionForEffectAnimationScript);
        Sound.Instance.SoundSet(Sound.Instance.poof, 0);
        Destroy(gameObject);
    }

    void ClownDeath()
    {
        CameraShakeRobert.instance.AddTrauma(0.1f);
        Vector2 positionForEffectAnimationScript = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        EffectAnimations.Instance.BigExplosion(positionForEffectAnimationScript, new Vector3(1,1,1));
        Sound.Instance.SoundSet(Sound.Instance.explosion, 0);
        //Destroy(gameObject);
        //Destroy(transform.parent.gameObject);
    }
}
    

