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

    private void Update()
    {

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

    private void OnCollisionEnter2D(Collision2D other)
    {   
        Destroy(gameObject);
    }
}
