using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public SpriteRenderer spriteRenderer;
    [SerializeField] public Rigidbody2D rb2D;

    [HideInInspector] public float bulletSpeed;
    [HideInInspector] public float bulletDamage;

    public Vector2 direction = Vector2.right;
    private static int bulletNumber;
    [SerializeField] float bulletLifeTime = 8f;


    private void Start()
    {
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
            Destroy(gameObject);
        }
    }
}