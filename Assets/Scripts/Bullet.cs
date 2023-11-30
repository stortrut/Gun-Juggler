using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Gun
{
    [SerializeField] SpriteRenderer spriteRenderer;
    //better bulletspeed logic system?

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5);
    }

    private void Update()
    {
        rb2D.velocity = transform.right*20;
    }

    public void SetColor(Color newColor)
    {
        spriteRenderer.color = newColor;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        Destroy(gameObject);  
    }

    private void OnTriggerEnter2D(Collider2D damagedObject)
    {   
           
    }
}
