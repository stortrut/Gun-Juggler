using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour,IAim
{
    [SerializeField] public Rigidbody2D rb2D;
    [SerializeField] private CapsuleCollider2D capsuleCollider;
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
        capsuleCollider.size *= 1.5f;
        //transform.localScale *= 1.5f;
       // transform.position -= new Vector3(0,1,0);
        transform.rotation *= new Quaternion(0,0,-1,0);
        //rb2D.gravityScale = 0;
       // rb2D.velocity *= Vector2.right;
        //rb2D.velocity.y = 0;
        rb2D.velocity *= -Vector2.one;
       // canDamageEnemies = true;
        transform.gameObject.tag = "Bullet";
    }
    public void AimInfo(Vector3 aimInfo)
    {
        aim=aimInfo;
    }

}
