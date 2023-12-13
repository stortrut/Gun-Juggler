using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloatingBalloon : MonoBehaviour
{
    Tween X;
    Tween Y;
    // Start is called before the first frame update
    void Start()
    {
        var timeItTakes = Random.Range(2,10);
         X = transform.DOLocalMoveX(transform.localPosition.x + 4, 1).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);
         Y = transform.DOLocalMoveY(transform.localPosition.y + 6, timeItTakes).OnComplete(KillTweenX);
       
    }

    void KillTweenX()
    {
      X.Kill();
      Y = transform.DOLocalMoveY(transform.localPosition.y + 1, 1).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
