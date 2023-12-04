using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour //, IStunnable
{
    [SerializeField] private EnemyMovementPatrolling patrolling; 
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int velocity = 5;
    bool hitObstacle;
    bool groundExists;
    float distanceSide = 1f;

    LayerMask normal;
    Vector3 raycastRightDirection = -Vector2.right;
    Vector2 savedVelocity;
    Vector2 raycastPosition;
    float heightOfObject;
    float widthOfObject;
    bool startStop=false;
    bool facingRight;
    float currentTime=0;

    //public bool isStunnable { get { return isStunned; } set { isStunned = value; } }
    void Start()
    {
        heightOfObject=GetComponent<BoxCollider2D>().bounds.size.y;
        widthOfObject = GetComponent<BoxCollider2D>().bounds.size.x;
        normal =LayerMask.GetMask("Default");
        widthOfObject *= -1;
        //rigidBody.velocity = Vector2.right * velocity;

    } 


    void Update()
    {
        //if (isStunned == true)
        //{
        //    return;
        //}
        RayCast();
        //Walk();     
        

        if (Input.GetKeyDown(KeyCode.P))
        {
            Flip();
        }
    }

    public void Walk()
    {
      
    }
   
    public void Flip()
    {
        //if(rigidBody.velocity==Vector2.zero||rigidBody.velocity!=Vector2.right*5)
        //{
        //    if(facingRight)
        //    {
        //     rigidBody.velocity = Vector2.right * -velocity;
        //    }
        //    else
        //    {
        //     rigidBody.velocity = Vector2.right * velocity;
        //    }
        //}
        //ChangeVelocity(-1f);
        //spriteRenderer.flipX = !spriteRenderer.flipX;
        raycastRightDirection *=-1;
        facingRight = !facingRight;
        
    }
    private void ChangeVelocity(float Change)
    {
        rigidBody.velocity *= Change;
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
    private void RayCast() 
    { 
        raycastPosition = new Vector2(transform.position.x - (widthOfObject / 2), transform.position.y - (heightOfObject / 2.2f));
        hitObstacle = Physics2D.Raycast(raycastPosition, raycastRightDirection, distanceSide, normal);
        if(!hitObstacle)
        {
            hitObstacle = Physics2D.Raycast(new Vector2(raycastPosition.x, raycastPosition.y + heightOfObject/  2), raycastRightDirection, distanceSide, normal);
        }
        groundExists = Physics2D.Raycast(raycastPosition, Vector2.down * distanceSide, 5, normal);
        Debug.DrawRay(raycastPosition, raycastRightDirection * distanceSide, Color.blue);
        Debug.DrawRay(new Vector2(raycastPosition.x, raycastPosition.y + heightOfObject/4), raycastRightDirection, Color.red);
        Debug.DrawRay(raycastPosition, Vector2.down * 2, Color.yellow);
        if (hitObstacle == true)
        {
            //Debug.Log(hitObstacle);
        }
        if ((hitObstacle == true || groundExists == false) && (Time.time - currentTime > 1.5f || currentTime == 0))
        {
            currentTime = Time.time;
            hitObstacle = false;
            groundExists = true;
            patrolling.Turn();
            
            //Stop();
            Flip();
            //Invoke(nameof(Flip), 0.4f);
            // Invoke(nameof(Stop),0.3f);
        }
    }
}
//private void OnTriggerEnter2D(Collider2D other)
//{
//    if (other.gameObject.layer == 3)
//    {
//        Debug.Log(other.gameObject.name);
//        PlayerCheck();
//    }


//}
//private void OnTriggerExit2D(Collider2D other)
//{
//    if (other.gameObject.layer == 3)
//    {
//        Debug.Log(other.gameObject.name);
//        ChangeVelocity(-4);
//    }
//}

//private void PlayerCheck()
//{
//    if ((Player.transform.position.x > transform.position.x) == facingRight)
//    {
//      Flip();

//    }
//   else
//    {

//    }
//    ChangeVelocity(4);
//}
