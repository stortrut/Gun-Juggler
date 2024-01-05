using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HamsterMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    
    private void Start()
    {
        rigidBody.DOMoveY(transform.position.y + 1, 1).SetLoops(2).OnComplete(MoveUp);
    }

    private void MoveUp()
    {
        rigidBody.DOMoveY(transform.position.y + 40, 10).SetEase(Ease.InSine).OnComplete(Destroy);
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
