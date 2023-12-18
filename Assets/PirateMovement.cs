using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PirateMovement : MonoBehaviour
{
    Tween Ytween;
    Tween Xtween;

    // Start is called before the first frame update
    void Start()
    {
        var startposition = transform.position;
        Ytween = transform.DOMoveY(startposition.y+4,1).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        //Xtween = transform.DOMoveX(transform.position.x - 2, 0.5f).OnComplete(DoDoTween);
        DoDoTween();
    }   
    private void DoDoTween()
    {
        Xtween = transform.DOMoveX(transform.position.x - 2, 0.5f).SetEase(Ease.InOutSine).OnComplete(DoDoTween);
    }
}
