using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    [SerializeField] private new BoxCollider2D collider;
    bool canDamageEnemies=false;
    public void Deflected()
    {
        collider.size *= 1.5f;
        transform.localScale *= 1.5f;
        rb2D.velocity *= -Vector2.one;
        canDamageEnemies = true;
        transform.gameObject.tag = "Bullet";
    }
        private void OnTriggerEnter2D(Collider2D damagedobject)
    {
        
    }

}
