using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IStunnable
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Collider2D mainPlayerCollider;
    [SerializeField] private LegAnimationHandler legs;
    [SerializeField] private Health playerHealth;


    [Header("Walk")]
    [SerializeField] private float acceleration = 10f;
    [SerializeField] public float speed = 7f;
    [SerializeField] private float deacceleration = 10f;

    private float horizontalInput;
    public float velocityToAddX;

    [HideInInspector] public bool isFacingRight;
    
    [HideInInspector] public bool isStunned = false;    
    [HideInInspector] public bool timeStop;
    [HideInInspector] public float timeStun;
    [HideInInspector] private FollowPlayer followPlayer;

    [Header("Jump")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private float groundCheckDistanceFromCollider;
    [SerializeField] private bool actuallyTouchingGround = true;

    private float calculatedGroundCheckLenght;

    [HideInInspector] public bool onGround = false;
    public bool isJumping = false;
    
    public bool isStunnable { get { return isStunned; } set { isStunned = value; } }
  
    void Start()
    {
        followPlayer = FindObjectOfType<FollowPlayer>();
    }

    void Update()
    {
        calculatedGroundCheckLenght = (mainPlayerCollider.bounds.size.y / 2) + groundCheckDistanceFromCollider;

        if (playerHealth.isDead)
        {
            return;
        }
        if (isStunned == true) 
        {
            return;
        }
        Walk();
        Jump();

        if (onGround)
        {
            //followPlayer.AllowCameraFollowInYAxis();
            isJumping = false;
        }
    }
    private void FixedUpdate()
    {
        onGround = CheckIfTouchingGround();
    }

    private bool CheckIfTouchingGround()
    {
        bool isTouchingGround = false;

        RaycastHit2D[] groundCheckArray = Physics2D.RaycastAll(transform.position, Vector2.down, calculatedGroundCheckLenght);

        foreach (RaycastHit2D rayCastHitObject in groundCheckArray)
        {
            if (rayCastHitObject.collider.CompareTag("Ground"))
            {
                isTouchingGround = true;
            }
        }

        return isTouchingGround;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CompareTag("Ground"))
        {
            actuallyTouchingGround = true;
        }
    }

    private void Walk()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        //if(DotweenPlayer.Instance.isRunning == false)
        //{
        //    DotweenPlayer.Instance.NoInput();
        //}
        //if(horizontalInput > 0) 
        //{ 
        //    isFacingRight = true;

        //    legs.SetDirectionToForwards();
        //    DotweenPlayer.Instance.SwerveRight();
        //}
        //if (horizontalInput < 0) 
        //{ 
        //    isFacingRight = false;

        //    legs.SetDirectionToBackwards();
        //    DotweenPlayer.Instance.SwerveLeft();
        //}

        velocityToAddX += horizontalInput * acceleration * Time.deltaTime;
        velocityToAddX = Mathf.Clamp(velocityToAddX, -speed, speed);

        if (horizontalInput == 0 || (horizontalInput < 0 == velocityToAddX > 0))
        {
            velocityToAddX *= 1 - deacceleration * Time.deltaTime;


            legs.PauseAnimation(true);
            //if (!bodyAnimator.GetBool("Dead"))
            //{
            //    bodyAnimator.speed = 0;
            //}
        }
        else
        {
            legs.PauseAnimation(false);
            //DotweenPlayer.Instance.Input();
        }
        //else
        //{
        //    if (!bodyAnimator.GetBool("Dead"))
        //    {
        //        bodyAnimator.speed = 1;
        //    }
        //}
        if(onGround)
        {
        rigidBody.velocity = new Vector2(velocityToAddX, rigidBody.velocity.y);
        }
        else
        {
            rigidBody.velocity += new Vector2(velocityToAddX * Time.deltaTime * 4, 0);
        }
        rigidBody.velocity = new Vector2(Mathf.Clamp(rigidBody.velocity.x, -7, 7),rigidBody.velocity.y);
    }

    private void Jump()
    {
        if (onGround && Input.GetButtonDown("Jump"))
        {
            float jumpVelocity = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpVelocity);
            isJumping = true;
        }
    }
}
