using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public SpriteRenderer spriteRenderer;
    [SerializeField] public Rigidbody2D rb2D;
    public float bulletSpeed;
    public float bulletDamage;


    [SerializeField] float bulletLifeTime = 5f;


    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        rb2D.velocity = transform.right * bulletSpeed;

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
