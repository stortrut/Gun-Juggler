using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D rb2D;

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






    private void OnTriggerEnter2D(Collider2D damagedObject)
    {   
           
    }
}
