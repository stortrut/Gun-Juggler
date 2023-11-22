using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Collider2D mainPlayerCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;


    [Header("Walk")]
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float speed = 7f;
    [SerializeField] private float deacceleration = 10f;

    private float horizontalInput;
    private float velocityToAddX;

    [HideInInspector] public bool isFacingRight;


    [Header("Jump")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private float groundCheckDistanceFromCollider;

    private float calculatedGroundCheckLenght;

    private bool onGround = false;
    private bool isJumping = false;





    // Update is called once per frame
    void Update()
    {
        calculatedGroundCheckLenght = (mainPlayerCollider.bounds.size.y / 2) + groundCheckDistanceFromCollider;


        Walk();
        Jump();

        if (onGround)
        {
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
                isTouchingGround = true;
        }

        return isTouchingGround;
    }


    private void Walk()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if(horizontalInput > 0) 
        { 
            isFacingRight = true;
            spriteRenderer.flipX = false;
        }
        if (horizontalInput < 0) 
        { 
            isFacingRight = false;
            spriteRenderer.flipX = true;
        }

        velocityToAddX += horizontalInput * acceleration * Time.deltaTime;
        velocityToAddX = Mathf.Clamp(velocityToAddX, -speed, speed);

        if (horizontalInput == 0 || (horizontalInput < 0 == velocityToAddX > 0))
        {
            velocityToAddX *= 1 - deacceleration * Time.deltaTime;
        }

        rigidBody.velocity = new Vector2(velocityToAddX, rigidBody.velocity.y);
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
