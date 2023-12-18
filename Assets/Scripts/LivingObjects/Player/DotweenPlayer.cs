using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Sources;
using UnityEngine;

public class DotweenPlayer : MonoBehaviour
{
    public static DotweenPlayer Instance;
    private bool isBouncing = false;
    Rigidbody2D rb;
    private float jumpHeight = -.2f;
    public Ease currentEase;
    Vector3 startPos;
    private float idlingValue = 2;
    private Vector3 startRotation;
    public bool isRunning = false;
    public Tween noInput;
    private Tween idle;
    private Tween idleFlip;

    private void Start()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        startRotation = transform.rotation.eulerAngles;
        
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    SwerveLeft();
        //}
        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    SwerveRight();
        //}
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    FlipTween();
        //}
        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    BounceTween();
        //}
        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    startPos = transform.position + new Vector3(0, 2.8f);
        //    BounceTween2();
        //}
        
    }
    public void FlipTween()
    {
        transform.DORotate( new Vector3(0, 200, 0), 2, RotateMode.Fast).SetLoops(-1).SetEase(Ease.OutBounce);
    }

    public void RotateFromSpeed(float veloX)
    {
        transform.rotation = Quaternion.Euler(0,0,veloX);
    }

    public void SwerveLeft()
    {
        transform.DORotate(startRotation + new Vector3(0, 0, -2.5f), 0.5f).SetEase(Ease.OutQuad);
        //transform.DORotate(new Vector3(0, 0, 15), 0.5f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutQuad);
    }

    public void SwerveRight()
    {
      var thing =  transform.DORotate(startRotation + new Vector3(0, 0, -5 * 4), 0.5f).SetEase(Ease.OutQuad);
        //transform.DORotate(new Vector3(0, 0, -15), 0.5f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutQuad);
    }
    public void Idle() 
    {
         idle = transform.DORotate(startRotation + new Vector3(0, 0, idlingValue), 0.5f).SetEase(Ease.InSine).OnComplete(Flip);
    }
    private void Flip()
    {
       idleFlip = transform.DORotate(startRotation + new Vector3(0, 0, -idlingValue), 0.5f).SetEase(Ease.InSine).OnComplete(Idle);
        //idlingValue = -idlingValue;
    }
    public void NoInput()
    {
        noInput = transform.DORotate(startRotation,1).OnComplete(Idle);
        isRunning = true;
    }
    public void Input()
    {
        noInput.Kill();
        idle.Kill();
        idleFlip.Kill();
        isRunning = false;
    }
    public void BounceTween()
    {

        //just wrong
        //startPos = transform.position;
        //transform.position = startPos;
        //transform.DOMoveX(7, 2).SetEase(currentEase);
        //transform.DOScale(Vector3.one * 1.1f, 2).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);

        //wrong direction
        transform.DOLocalMoveY(jumpHeight, 1.0f).SetEase(Ease.OutBounce);

        //fjäder bounce
        //transform.DOShakePosition(2.0f, strength: new Vector3(0, 2, 0), vibrato: 5, randomness: 1, snapping: false, fadeOut: true);
    }

    public void BounceTween2()
    {
        transform.position = startPos;
        transform.DOLocalMoveY(jumpHeight, .8f).SetEase(Ease.OutBounce);
    }

    private void OnDisable()
    {
        transform.DOKill();
    }
   
}
