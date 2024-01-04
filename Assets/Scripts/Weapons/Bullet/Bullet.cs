using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] public SpriteRenderer spriteRenderer;
    [SerializeField] public Rigidbody2D rb2D;

    [HideInInspector] public float bulletSpeed;
    [HideInInspector] public float bulletDamage;

    public Vector2 direction = Vector2.right;
    [SerializeField] float bulletLifeTime = 15f;
    public bool piercing;
    private int pierceTarget = 3;
    public bool isWaterGun = false;

    bool waterBulletDestroyed = false;

    private void Start()
    {
        if (rb2D == null) { return; } 

        rb2D.velocity = transform.right * bulletSpeed;
        Invoke(nameof(Death),bulletLifeTime);
    }

    public bool bulletDirectionRight
    {
        get { return direction.x > 0; }
        set { direction = value ? Vector2.right : Vector2.left; }
    }

    public void SetBulletData(float inputSpeed, float inputDamage)
    {
        bulletSpeed = inputSpeed;
        bulletDamage = inputDamage;
    }

    public void SetColor(Color newColor)
    {
        spriteRenderer.color = newColor;
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    private void WaterGunDeath()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        if (!waterBulletDestroyed) 
        {
            EffectAnimations.Instance.WaterSplash(transform.position, transform.localScale * 3);
            waterBulletDestroyed = true;
        }
        
        Destroy(gameObject, .1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (isWaterGun)
            {
                WaterGunDeath();
            }
            else
            {
                Death();
            }
        }

        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            if (piercing) 
            {
                Pierce();
            }
            else if (isWaterGun)
            {
                WaterGunDeath();
            }
            else
            {
                Death();
            }
        }

        else if(other.gameObject.CompareTag("EnemyNonTargetable"))
        {
            if (piercing)
            {
                Pierce();
            }

            else if (isWaterGun)
            {
                WaterGunDeath();
            }
            else
            {
                Death();
            }
        }
    }
        private void Pierce()
        {
            pierceTarget--;
            if (pierceTarget == 0)
            {
                if (isWaterGun)
                {
                    WaterGunDeath();
                }
                else
                {
                    Death();
                }
            }   
        }
    
}