using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour, IStunnable
{
    [Header("References")]
    [SerializeField] public Rigidbody2D rigidBody;
    [SerializeField] private Collider2D mainPlayerCollider;
    [SerializeField] private LegAnimationHandler legs;
    [SerializeField] private Health playerHealth;
    [SerializeField] private Animator anim;

    [Header("Walk")]
    [SerializeField] private float acceleration = 10f;
    [SerializeField] public float speed = 7f;
    [SerializeField] private float deacceleration = 3f;

    [ReadOnly] public float horizontalInputRaw;
    [ReadOnly] public float horizontalInput;

    [HideInInspector] public float velocityToAddX;
    [HideInInspector] private Vector3 velocityToAdd;
    Quaternion currentRotation = new Quaternion(0, 0, 0, 0);
    private float rotationSpeed = .2f;
    private float rotationAmount = 10;

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

    [SerializeField] private GameObject shadow;
    private float calculatedGroundCheckLenght;
    private float landingBounceAmount;
    private float savedVelocityForBounce;
    private bool shouldAddBounceForce = false;

    [SerializeField] GameObject wheelArtHolder;

    [HideInInspector] public bool onGround = false;

    [Header("Tutorial Controllers")]
    public bool turnOffMovement;



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
        //if (isStunned == true) 
        //{
        //    return;
        //}


        Walk();
        Jump();


        //if (onGround)
        //{
        //    //followPlayer.AllowCameraFollowInYAxis();
        //    isJumping = false;
        //}
        if (actuallyTouchingGround)
        {
            isJumping = false;
        }
    }

    private void FixedUpdate()
    {
        onGround = CheckIfTouchingGround();
      //  var shade = Shadow();
       // shadow.transform.position = new Vector3(shadow.transform.position.x, shade, shadow.transform.position.z);
    }

    //private float Shadow()
    //{
    //    LayerMask mask = LayerMask.GetMask("Ground");
    //    var shadow = Physics2D.Raycast(transform.position, Vector2.down, 5, mask);
    //    Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - 5, transform.position.z), Color.green);
    //    return shadow.point.y;
    //}
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
        if (collision.gameObject.CompareTag("Ground"))
        {
            actuallyTouchingGround = true;
            
            if (shouldAddBounceForce)
            {
                Debug.Log("savedvelocity: " + savedVelocityForBounce);
                //FindObjectOfType<FollowPlayer>().trampolineJumping = true;

                AddBounceForce();
            }
            else
            {
                if (rigidBody.velocity.y < 0)
                {
                    Sound.Instance.SoundSet(Sound.Instance.landingWithBike, 0, .08f * savedVelocityForBounce, .7f);
                }
            }
        }
        if (collision.gameObject.CompareTag("Trampoline"))
        {
          

            FindObjectOfType<FollowPlayer>().trampolineJumping = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            actuallyTouchingGround = false;
        }
    }

    private void AddBounceForce()
    {
        Sound.Instance.SoundSet(Sound.Instance.landingWithBike, 0, .1f*savedVelocityForBounce/2, .7f);
        rigidBody.AddForce(new Vector2(0, 11 * savedVelocityForBounce));
        shouldAddBounceForce = false;
        savedVelocityForBounce = savedVelocityForBounce / 2;
    }

    private void Walk()
    {
        if (!turnOffMovement)
        {
            horizontalInputRaw = Input.GetAxisRaw("Horizontal");
            horizontalInput = Input.GetAxis("Horizontal");
        }
        else
        {
            horizontalInputRaw = 0;
            horizontalInput = 0;
        }

        var veloX = rigidBody.velocity.x;
        timeActive += Time.deltaTime * 2;
        timeActive = Mathf.Clamp(timeActive, 0, 4);
        legs.SetSpeed(veloX);
        RotateBasedOnHorizontalInput(horizontalInput);
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
            if (horizontalInputRaw > 0)
            {
                legs.SetDirectionToForwards();
                anim.SetBool("Forward", true);
            }
            else
            {
                legs.SetDirectionToBackwards();
                anim.SetBool("Forward", false);
            }
        }

        velocityToAddX += horizontalInputRaw * acceleration * Time.deltaTime;
        velocityToAddX = Mathf.Clamp(velocityToAddX, -speed, speed);

        if (horizontalInputRaw == 0 || (horizontalInputRaw < 0 == velocityToAddX > 0))
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

        if (onGround)
        {
            rigidBody.velocity = new Vector2(velocityToAddX, rigidBody.velocity.y);
        }

        else
        {
            rigidBody.velocity = new Vector2(velocityToAddX*.8f, rigidBody.velocity.y);
        }

        rigidBody.velocity = new Vector2(Mathf.Clamp(rigidBody.velocity.x, -7, 7), rigidBody.velocity.y);
    }

    public void RotateBasedOnHorizontalInput(float veloX)
    {
        wheelArtHolder.transform.rotation = Quaternion.Lerp(wheelArtHolder.transform.rotation, Quaternion.Euler(0, 0, -veloX*rotationAmount), rotationSpeed); //(Quaternion.Euler(0, 0, -veloX * (5 - timeActive));
        //Vector3 eulerAngles = new Vector3(0, 0, -veloX * 30 * (5 - timeActive));
        //Vector3 eulerAngles = new Vector3(0, 0, artHolder.transform.rotation.z);
        //currentRotation.eulerAngles = eulerAngles;

        //wheelArtHolder.transform.rotation = Quaternion.Lerp(currentRotation, (Quaternion.Euler(0, 0, -veloX * 30*(5 - timeActive))), .5f); //(Quaternion.Euler(0, 0, -veloX * (5 - timeActive));

        //artHolder.transform.rotation = Quaternion.Lerp(currentRotation, (Quaternion.Euler(0, 0, -veloX * 30 * (5 - timeActive))), .5f); //(Quaternion.Euler(0, 0, -veloX * (5 - timeActive));
        //artHolder.transform.DORotate(new Vector3(0, 0, -veloX * 30 * (5 - timeActive)), 0.2f);//.SetEase();
        //wheelArtHolder.transform.rotation = currentRotation;
    }
    
    private void Jump()
    {
        if (onGround && Input.GetButtonDown("Jump") || actuallyTouchingGround && Input.GetButtonDown("Jump"))
        {
            Sound.Instance.SoundRandomized(Sound.Instance.jumpVoice, .6f, .3f,.2f);
            float jumpVelocity = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpVelocity);
            isJumping = true;

            Invoke(nameof(ShouldAddBounceForceTrue), .2f);
            savedVelocityForBounce = rigidBody.velocity.y;
        }
    }

    void ShouldAddBounceForceTrue()
    {
        shouldAddBounceForce = true;
    }
}
