using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D damagedObject)
    {
        IDamageable HitObject=damagedObject.gameObject.GetComponent<IDamageable>();
           
        if (HitObject != null && HitObject.Protection==0)
        {
            HitObject.ApplyDamage(1);
        }
        Destroy(this.gameObject);   
    }
}
