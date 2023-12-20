using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloatingBalloon : MonoBehaviour
{
    Tween X;
    Tween Y;
    Tween Xtrans;
    Rigidbody2D body;
    [SerializeField] private Whip whip;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        // var timeItTakes = Random.Range(2,5);
        var timeItTakes = 2;
        X = transform.DOLocalMoveX(transform.localPosition.x + 2, 1).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        // Xtrans = transform.DOLocalMoveX(transform.localPosition.x + 4, 1).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        Y = transform.DOLocalMoveY(transform.localPosition.y + 10, timeItTakes).OnComplete(KillTweenX);
        //Y = transform.DOLocalJump(new Vector3(transform.localPosition.x,transform.localPosition.y+6,0), 10, 1, timeItTakes).OnComplete(KillTweenX);

    }

    void KillTweenX()
    {
        whip.DoTheWhip();
        X.Kill();
        Xtrans.Kill();
        body.freezeRotation = true;
        Y = transform.DOLocalMoveY(transform.localPosition.y - 2, 2).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDestroy()
    {
        X.Kill();
        Y.Kill();
        Xtrans.Kill();
    }
}
