using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloatingBalloon : MonoBehaviour
{
    Tween X;
    Tween Y;
    Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        body =GetComponent<Rigidbody2D>();
        var timeItTakes = Random.Range(2,5);
        
         X = body.DOMoveX(transform.position.x + 10, 2).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);
         Y = body.DOMoveY(transform.position.y + 6, timeItTakes).OnComplete(KillTweenX);
       
    }

    void KillTweenX()
    {
      X.Kill();
      Y = body.DOMoveY(transform.position.y + 1, 1).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
