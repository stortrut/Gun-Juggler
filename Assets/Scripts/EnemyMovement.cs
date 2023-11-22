using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject Player; 
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int velocity;
    
    bool playerNear;
    bool onPatrol;
    bool hitObstacle;
    bool groundExists;
    float distanceSide = 0.1f;


    LayerMask normal = 0;
    Vector3 rightdirection = Vector2.right;
    Vector2 savedVelocity;
    bool startStop=false;
    bool facingRight;
    float currentTime=0;

    void Start()
    {
       
       
        Patrol();
        
    }


    void Update()
    {
        hitObstacle = Physics2D.Raycast(transform.position + rightdirection/1.6f, rightdirection, distanceSide,normal);
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
        
        

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 4)
        {
            Debug.Log(other.gameObject.name);
            PlayerCheck();
        }
            

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 4)
        {
            Debug.Log(other.gameObject.name);
            ChangeVelocity(0.5f);
        }
    }

    private void PlayerCheck()
    {
        if (Player.transform.position.x < transform.position.x == facingRight)
        {
          Flip();

        }
       else
        {
          
        }
        ChangeVelocity(2f);
    }
    
    public void Patrol()
    {
        if(rigidBody.velocity==Vector2.zero)
        {
            ChangeVelocity(velocity);
        }
        else
        {
        rigidBody.velocity = savedVelocity;
        }
        onPatrol = true;
        

    }

    public void Flip()
    {
        if(rigidBody.velocity==Vector2.zero)
        {
            if(facingRight)
            {
             rigidBody.velocity = Vector2.right * -velocity;
            }
            else
            {
             rigidBody.velocity = Vector2.right * velocity;
            }
                
        }
        //  velocity *= -1;
        ChangeVelocity(-1f);
        spriteRenderer.flipX = true;
        rightdirection *=-1;
        facingRight = !facingRight;
    }
    private void ChangeVelocity(float Change)
    {
        rigidBody.velocity *= Change;
    }
    private void ChangeVelocity(int Change)
    {
        rigidBody.velocity += new Vector2(Change,0);
    }
    public void Stop()
    {
        startStop = !startStop;


        switch (startStop)
        {
            
            case true:
                savedVelocity = rigidBody.velocity;
                Debug.Log(savedVelocity);   
                ChangeVelocity(0f);
                
                break;
            case false:
                rigidBody.velocity=savedVelocity;
                
                
                break;


        }
      
    }
}
