using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DotweenPlayer : MonoBehaviour
{
    [Header("Playerinfo")]
    [HideInInspector] bool movingRight;
    [HideInInspector] float movingSpeed;
    [HideInInspector] float acceleration;

    [Header("References")]
    [HideInInspector] PlayerMovement playerMovement;
    private bool isBouncing = false;
    Rigidbody2D rb;
    private float jumpHeight = -.2f;
    public Ease currentEase;
    Vector3 startPos;
    private float horizontalInput;

    private Tweener tweener;

    //Tweenercontroller variables:
    Vector2 normaliceThisToGetSwerveDirection;
    Vector2 direction;
    [SerializeField] private Tween swerveActionBodyFirst;
    [SerializeField] private Tween swerveActionWheelFirst;

    float savedSpeedChange;

    public bool hasStarted = false;
    float savedInputData = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        //Assign();
        //SwerveBodyFirst();
        //TweenerController();
    }

    private void Update()
    {

        //if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) 
        //{
        //    Swerve();
        //}

        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    SwerveBodyFirst();
        //}
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    SwerveBodyFirst();
        //}
        //if (Input.GetKeyUp(KeyCode.D))
        //{
        //    SwerveBodyFirst();
        //}
        //if (Input.GetKeyUp(KeyCode.A))
        //{
        //    SwerveBodyFirst();
        //}
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    FlipTween();
        //}
        if (Input.GetKeyDown(KeyCode.B))
        {
            //BounceTween();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            startPos = transform.position + new Vector3(0, 2.8f);
            //BounceTween2();
        }
        if (Input.GetKeyUp(KeyCode.A) || (Input.GetKeyUp(KeyCode.D)))
        {

        }
    }

    protected void TweenerController()
    {
        float currentSpeedChange = playerMovement.velocityToAddX;
        if (savedSpeedChange != currentSpeedChange)             //at accelerating (or deaccelerating) speed the body should move first 
        {
            SwerveBodyFirst();
            Debug.Log("called body, acc. Current: " + currentSpeedChange + " saved: " + savedSpeedChange);
        }
        else if (savedSpeedChange == currentSpeedChange)
        {
            SwerveWheelFirst();                               //at constant speed the wheel should move before the body
            Debug.Log("called wheel, same as before. Current: " + currentSpeedChange + " saved: " + savedSpeedChange);
        }
    }

    public IEnumerator SwerveEnumerator()
    {
        hasStarted = true;
        while (playerMovement.horizontalInput > .2f)
        transform.DORotate(new Vector3(0, 0, -horizontalInput *3), 0.3f).SetEase(Ease.Linear).OnComplete(() => {   //SetEase(Ease.OutBack)
            savedInputData = playerMovement.horizontalInput;
        });
        yield return null;
        hasStarted = false;
    }
    public void SwerveBodyFirst()
    {
        transform.DOKill();
        savedSpeedChange = playerMovement.velocityToAddX;

        transform.DORotate(new Vector3(0, 0, -savedSpeedChange), 0.3f).SetEase(Ease.Linear).OnComplete(() => {   //SetEase(Ease.OutBack)
            TweenerController();
        });
    }

    private void SwerveWheelFirst()
    {
        transform.DOKill();
        savedSpeedChange = playerMovement.velocityToAddX;

        transform.DORotate(new Vector3(0, 0, savedSpeedChange), 0.3f).SetEase(Ease.Linear).OnComplete(() => {    //SetEase(Ease.OutBack).
            TweenerController();
        });
    }

    //private void LatestVersionBodyFirst()
    //{
    //    savedSpeedChange = playerMovement.velocityToAddX;
    //    Debug.Log("body, saved" + savedSpeedChange);
    //    swerveActionBodyFirst = transform.DORotate(new Vector3(0, 0, Mathf.Abs(savedSpeedChange) * -direction.x), .5f)
    //        .SetEase(Ease.OutBack)
    //        .OnComplete(() =>
    //        {
    //            TweenerController();
    //        });
    //}

    //private void LatestVersionWheelFirst()
    //{
    //    savedSpeedChange = playerMovement.velocityToAddX;
    //    Debug.Log("wheel, saved" + savedSpeedChange);
    //    swerveActionWheelFirst = transform.DORotate(new Vector3(0, 0, Mathf.Abs(savedSpeedChange) * -direction.x), .5f)
    //        .SetEase(Ease.OutBack)
    //        .OnComplete(() =>
    //        {
    //            TweenerController();
    //        });
    //}


    //SetLoops(2, LoopType.Yoyo).

    //void SwerveActionBodyFirst(Action<float> callback)
    //{
    //    savedSpeed = playerMovement.horizontalInput * playerMovement.speed;
    //    normaliseThisToGetSwerveDirection = new Vector2(savedSpeed, 0);
    //    direction = normaliseThisToGetSwerveDirection.normalized;

    //    savedSpeed = playerMovement.velocityToAddX;
    //    Debug.Log("body save: " + savedSpeed);

    //}









    //protected void TweenerController()
    //{
    //    float savedSpeed = playerMovement.horizontalInput * playerMovement.speed;
    //    normaliceThisToGetSwerveDirection = new Vector2(savedSpeed, 0);
    //    direction = normaliceThisToGetSwerveDirection.normalized;
    //    Debug.Log("directionX: " + direction);
    //    if (!playerMovement.onGround)
    //    { return;}

    //    swerveActionBodyFirst = () => transform.DORotate(new Vector3(0, 0, Mathf.Abs(savedSpeed) * -direction.x), 0.2f).SetEase(Ease.OutQuad).OnComplete(() => {
    //        if (playerMovement.horizontalInput * playerMovement.speed > savedSpeed)
    //        {
    //            SwerveBodyFirst();                                          
    //            Debug.Log("velocity in x increased");
    //        }
    //        else if (playerMovement.horizontalInput == savedSpeed)
    //        {
    //            SwerveWheelFirst();
    //            Debug.Log("velocity in x is constant");
    //        }
    //        else if (playerMovement.horizontalInput < savedSpeed)
    //        {
    //            SwerveBodyFirst();
    //            Debug.Log("velocity in x decreased");
    //        }
    //    });


    //    swerveActionWheelFirst = () => transform.DORotate(new Vector3(0, 0, Mathf.Abs(savedSpeed) * direction.x), 0.2f).SetEase(Ease.OutQuad).OnComplete(() =>
    //    {
    //        if (playerMovement.horizontalInput < savedSpeed)
    //        {
    //            SwerveBodyFirst();
    //            Debug.Log("velocity in x decreased");
    //        }
    //        else if (playerMovement.horizontalInput == savedSpeed)
    //        {
    //            SwerveWheelFirst();
    //            Debug.Log("velocity in x is constant");
    //        }
    //        else if (playerMovement.horizontalInput < savedSpeed)
    //        {
    //            SwerveBodyFirst();
    //            Debug.Log("velocity in x is constant");
    //        }
    //        //SwerveIdle();
    //    });

    //}


    //    private void SetSwerveAction()
    //    {
    //    }

    //    private void InvokeSwerveAction()
    //    {
    //        if (swerveActionBodyFirst != null)
    //        {
    //            swerveActionBodyFirst.Invoke();
    //        }
    //    }


    //    private void SwerveIdle()
    //    {

    //    }

    //    public void FlipTween()
    //    {
    //        transform.DORotate(new Vector3(0, 200, 0), 2, RotateMode.Fast).SetLoops(-1).SetEase(Ease.OutBounce);
    //    }
    //    public void SwerveRight()
    //    {
    //        transform.DORotate(new Vector3(0, 0, -5), 0.5f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutQuad);
    //    }
    //    public void BounceTween()
    //    {

    //        //just wrong
    //        //startPos = transform.position;
    //        //transform.position = startPos;
    //        //transform.DOMoveX(7, 2).SetEase(currentEase);
    //        //transform.DOScale(Vector3.one * 1.1f, 2).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);

    //        //wrong direction
    //        transform.DOLocalMoveY(jumpHeight, 1.0f).SetEase(Ease.OutBounce);

    //        //fjäder bounce
    //        //transform.DOShakePosition(2.0f, strength: new Vector3(0, 2, 0), vibrato: 5, randomness: 1, snapping: false, fadeOut: true);
    //    }

    //    public void BounceTween2()
    //    {
    //        transform.position = startPos;
    //        transform.DOLocalMoveY(jumpHeight, .8f).SetEase(Ease.OutBounce);
    //    }

    private void OnDisable()
    {
        transform.DOKill();
    }

}


