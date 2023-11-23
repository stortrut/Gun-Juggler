using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    public void SetColor(Color newColor)
    {
        spriteRenderer.color = newColor;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this.gameObject);       //note: vapnet förstör bulleten just nu när de kolliderar
    }
    private void OnTriggerEnter2D(Collider2D damagedObject)
    {
        IDamageable HitObject=damagedObject.gameObject.GetComponent<IDamageable>();
        if (HitObject != null)
        {

         Debug.Log(HitObject.hasProtection);
        }
        
        if (HitObject != null && HitObject.hasProtection==false)
        {
            HitObject.ApplyDamage(1);
            
        }
        Destroy(this.gameObject);   
    }
}
