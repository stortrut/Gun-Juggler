using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour,IAim
{
    [SerializeField] public Rigidbody2D rb2D;
    [SerializeField] private CapsuleCollider2D collider;
    [HideInInspector] public Vector3 aim;
    //bool canDamageEnemies=false;

    public void Deflected()
    {
        collider.size *= 1.5f;
        transform.localScale *= 1.5f;
        rb2D.velocity *= -Vector2.one;
       // canDamageEnemies = true;
        transform.gameObject.tag = "Bullet";
    }
    public void AimInfo(Vector3 aimInfo)
    {
        aim=aimInfo;
    }

}
