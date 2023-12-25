using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BalloonMovement : MonoBehaviour
{
    [Header("Drag in")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Specific for each animal")]
    [SerializeField] private float speed;
    [SerializeField] private float tweenHeight;
    [SerializeField] private float tweenLoopDuration;

    private Tween movementY;
    void Start()
    {
        movementY = transform.DOMoveY((transform.position.y + tweenHeight), tweenLoopDuration).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.InOutSine);

        rb.velocity = Vector2.left * speed;
    }

    void Update()
    {

    }
    private void OnDestroy()
    {
        movementY.Kill();
    }
}
