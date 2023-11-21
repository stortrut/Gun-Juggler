using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject Player;
    public Vector2 velocity;
    Rigidbody2D rb2D;
    bool playerNear;    
    bool onPatrol;
    bool hitObstacle;
    float distanceSide=0.6f;
    SpriteRenderer EnemySprite;
    Vector2 rightdirection = Vector2.right;
    
    void Start()
    {
        EnemySprite=GetComponent<SpriteRenderer>();
        rb2D= GetComponent<Rigidbody2D>();
        Patrol();
    }

  
    void Update()
    {
        hitObstacle=Physics2D.Raycast(transform.position+new Vector3(distanceSide,0),rightdirection,distanceSide); 
        Debug.DrawRay(transform.position, rightdirection * distanceSide, Color.blue);
        if (hitObstacle==true)
        {
            Flip();
            hitObstacle = false;
        }
       
        if (Input.GetKeyDown(KeyCode.P))
        {
            Patrol();
        }
        if (!playerNear && !onPatrol)
        {
            Patrol();
        }
        
    }

    public void Patrol() 
    { 
        onPatrol=true;
        velocity *= Vector2.right;
        rb2D.velocity = velocity;
        
    }

    public void Flip()
    {
        velocity = -velocity;
        rb2D.velocity = velocity;
        EnemySprite.flipX = true;
        rightdirection = -rightdirection;
    }


}
