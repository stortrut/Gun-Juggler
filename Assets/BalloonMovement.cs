using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BalloonMovement : MonoBehaviour, IStunnable
{
    [Header("Drag in")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Specific for each animal")]
    [SerializeField] private float speed;
    [SerializeField] private float tweenHeight;
    [SerializeField] private float tweenLoopDuration;

    [HideInInspector] public bool isStunned = false;
    public bool dummy;
    Vector3 startPos;
    public bool isStunnable { get { return isStunned; }
        set 
        {
            isStunned = value;
            if (isStunned == true)
            {
                Debug.Log("knockback");
                Knockback();
            }
        }
    }   

    private Tween movementY;
    void Start()
    {
        startPos = transform.position;
        movementY = transform.DOMoveY((transform.position.y + tweenHeight), tweenLoopDuration).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.InOutSine);
        
        rb.velocity = Vector2.left * speed;
    }
    private void Knockback()
    {
        rb.AddForce(Vector2.right * 7, ForceMode2D.Impulse);
        Invoke(nameof(ResetSpeed),1);
    }
    private void ResetSpeed()
    {
        if (dummy == true)
        {
            transform.DOMove(startPos, 1);
            

        }
            rb.velocity = Vector2.left * (speed + 1);
    }
    private void OnDestroy()
    {
        movementY.Kill();
    }
}
