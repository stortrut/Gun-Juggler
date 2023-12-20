using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

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
    [SerializeField] private float deacceleration = 3f;

    [ReadOnly] public float horizontalInput;
    [HideInInspector] public float velocityToAddX;

    [ReadOnly] public bool isFacingRight;
    
    [HideInInspector] public bool isStunned = false;    
    [HideInInspector] public bool timeStop;
    [HideInInspector] public float timeStun;
    private float timeActive;
    [HideInInspector] private DotweenPlayer dotweenPlayer;

    [Header("Jump")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private float groundCheckDistanceFromCollider;
    [ReadOnly][SerializeField] private bool actuallyTouchingGround = true;
    [ReadOnly] public bool isJumping = false;

    private float calculatedGroundCheckLenght;

    [HideInInspector] public bool onGround = false;
   
    
    public bool isStunnable { get { return isStunned; } set { isStunned = value; } }
  
    void Start()
    {
        dotweenPlayer = FindObjectOfType<DotweenPlayer>();
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

        var veloX = rigidBody.velocity.x;
        timeActive += Time.deltaTime*2;
        timeActive = Mathf.Clamp(timeActive, 0, 4);
        legs.SetSpeed(veloX);
        RotateFromSpeed(veloX);
        //if ((Mathf.Abs(horizontalInput) >= .1f) && !dotweenPlayer.hasStarted)
        //{
        //    // if saved data från dotweenscript har ändrats, dvs blivit negativ från pos eller tvärt om, sen senast den callades ska den andra tweenen som tweenar tillbaks till 0 köras istället
        //    StartCoroutine(dotweenPlayer.SwerveEnumerator());
        //}

        if (MathF.Abs(veloX) < 0.5f)
        {
            legs.PauseAnimation(true);
            timeActive = 0;
        }
        else
        {
            if (veloX > 0)
            {
                legs.SetDirectionToForwards();
            }
            else
            {
                legs.SetDirectionToBackwards();
            }
        }

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
    public void RotateFromSpeed(float veloX)
    {
        transform.rotation = Quaternion.Euler(0, 0, -veloX * (5 - timeActive));
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
