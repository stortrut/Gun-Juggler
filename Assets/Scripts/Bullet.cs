using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Weapon
{
    [SerializeField] SpriteRenderer spriteRenderer;
     Rigidbody2D rb2D;
    //better bulletspeed logic system?

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        
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
        Destroy(this.gameObject);  
    }

    private void OnTriggerEnter2D(Collider2D damagedObject)
    {
        IDamageable HitObject=damagedObject.gameObject.GetComponent<IDamageable>();
        if (HitObject != null && HitObject.hasProtection==true)
        {
            Sound.Instance.EnemyNotTakingDamage();
            Debug.Log(HitObject.hasProtection);
        }
        
        if (HitObject != null && HitObject.hasProtection==false)
        {
            HitObject.ApplyDamage(1);
            Sound.Instance.EnemyTakingDamage();
            if (damagedObject.TryGetComponent(out Knockback knockbackComponent))        
            {
                Debug.Log("enemy knockback");
                knockbackComponent.KnockBackMyself(15,10,.4f,transform.position);
            }
        }
        
        Destroy(this.gameObject);   
    }
}
