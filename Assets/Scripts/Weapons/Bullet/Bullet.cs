using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            gameObject.SetActive(false);
            Destroy(gameObject,1f);
        }
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            if (piercing) 
            {
                Pierce();
            }
            else
            {
                gameObject.SetActive(false);
                Destroy(gameObject, 1f);
            }
        }
        else if(other.gameObject.CompareTag("EnemyNonTargetable"))
        {
            if (piercing)
            {
                Pierce();
            }
            else
            {
                gameObject.SetActive(false);
                Destroy(gameObject, 1f);
            }
        }
    }
        private void Pierce()
        {
            pierceTarget--;
            if (pierceTarget == 0)
            {
                Destroy(gameObject);
            }   
        }
    
}