using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public SpriteRenderer spriteRenderer;
    [SerializeField] public Rigidbody2D rb2D;
    public float bulletSpeed;
    public float bulletDamage;

    public Vector2 direction = Vector2.right;


    [SerializeField] float bulletLifeTime = 5f;

    public bool bulletDirectionRight
    {
        get { return bulletDirectionRight; }
        set
        {
            direction = -direction;
            return;
        }
    }

            private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        rb2D.velocity = direction * bulletSpeed;

        Destroy(gameObject, bulletLifeTime);
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

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if(other.gameObject.CompareTag("Ground"))
        {
            
            Destroy(gameObject); 
        }
        
    }
}
