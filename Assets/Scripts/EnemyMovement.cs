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
    bool groundExists;
    float distanceSide = 0.1f;
    SpriteRenderer EnemySprite;
    Vector3 rightdirection = Vector2.right;
    Vector2 savedVelocity;
    bool startStop=true;
    bool stopStop=true;
    float currentTime=0;

    void Start()
    {
        EnemySprite = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        Patrol();
        
    }   


    void Update()
    {
        hitObstacle = Physics2D.Raycast(transform.position + rightdirection/1.6f, rightdirection, distanceSide);
        groundExists = Physics2D.Raycast(transform.position + rightdirection/1.6f, Vector2.down, 5);
        Debug.DrawRay(transform.position + rightdirection/1.6f, rightdirection * distanceSide, Color.blue);
        Debug.DrawRay(transform.position + rightdirection/1.6f, Vector2.down * 2, Color.yellow);
        
        if ((hitObstacle == true || groundExists == false) && (Time.time-currentTime>1.5f||currentTime==0))
        {
            currentTime = Time.time;
            hitObstacle = false;
            groundExists = true;
            Stop();
            Invoke(nameof(Flip), 0.4f);
            Invoke(nameof(Stop),0.3f);
            
            
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Flip();
        }
        if (!playerNear && !onPatrol)
        {
            Patrol();
        }

    }

    public void Patrol()
    {
        if(rb2D.velocity==Vector2.zero)
        {
            rb2D.velocity = velocity;
        }
        else
        {
        rb2D.velocity = savedVelocity;
        }
        onPatrol = true;
        

    }

    public void Flip()
    {
        velocity *= -1;
        rb2D.velocity *= -1;
        EnemySprite.flipX = true;
        rightdirection *=-1;
    }

    public void Stop()
    {


        switch (startStop)
        {

            case true:
                savedVelocity = rb2D.velocity;
                Debug.Log(savedVelocity);
                rb2D.velocity = Vector2.zero;
                startStop = !startStop;
                break;
            case false:
                rb2D.velocity=savedVelocity;
                startStop = !startStop;
                
                break;


        }



    }
}
