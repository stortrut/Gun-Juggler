using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public SpriteRenderer spriteRenderer;
    [SerializeField] public Rigidbody2D rb2D;
    public float bulletSpeed;
    public float bulletDamage;

    //better bulletspeed logic system?

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5);
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
