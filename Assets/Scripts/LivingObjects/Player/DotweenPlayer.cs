using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotweenPlayer : MonoBehaviour
{
    private bool isBouncing = false;
    Rigidbody2D rb;
    private float jumpHeight = 2;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            JumpTween();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            FlipTween();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            BounceTween();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            //BounceWithEaseOutBounce();
        }
        
    }
    public void FlipTween()
    {
        transform.DORotate(new Vector3(0, 200, 0), 2, RotateMode.Fast).SetLoops(-1).SetEase(Ease.OutBounce);
    }

    public void JumpTween()
    {
        transform.DORotate(new Vector3(0, 0, 15), 0.5f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutQuad);
    }
    public void BounceTween()
    {
        transform.DOLocalMoveY(jumpHeight, 1.0f).SetEase(Ease.OutBounce);

        //transform.DOShakePosition(2.0f, strength: new Vector3(0, 2, 0), vibrato: 5, randomness: 1, snapping: false, fadeOut: true);
    }

    private void OnDisable()
    {
        transform.DOKill();
    }
   
}
