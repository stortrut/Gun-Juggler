using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour,IAim
{
    [SerializeField] public Rigidbody2D rb2D;
    [SerializeField] private CapsuleCollider2D collider;
    [HideInInspector] public Vector3 aim;
    bool directionRight = true;
    public Vector2 direction = Vector2.right;
    public bool bulletDirectionRight
    {
        get { return directionRight; }
        set
        {
            directionRight=value;
            if (bulletDirectionRight == true)
            {
                direction = Vector2.right;
            }
            else
                direction = Vector2.left;
            return;
        }
    }
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
