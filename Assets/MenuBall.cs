using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBall : MonoBehaviour
{
    [SerializeField] private Transform ball;
    [SerializeField] private Transform ball2;
    [SerializeField] private Transform ball3;
    [SerializeField] private Transform ball4;
    [SerializeField] private float frequency;
    [SerializeField] private float amplitude;
    public Vector3[] path;

    void Start()
    {
        //var x = Mathf.Cos((Time.time ) * frequency) * amplitude;
        //var y = Mathf.Sin((Time.time) * frequency) * amplitude;
        ball.DOLocalPath(path, 3.2f, PathType.CatmullRom).SetLoops(-1);
        StartCoroutine(OtherBalls());
    }
    public IEnumerator OtherBalls()
    {
        yield return new WaitForSeconds(0.8f);
        ball2.DOLocalPath(path, 3.2f, PathType.CatmullRom).SetLoops(-1);
        yield return new WaitForSeconds(0.8f);
        ball3.DOLocalPath(path, 3.2f, PathType.CatmullRom).SetLoops(-1);
        yield return new WaitForSeconds(0.8f);
        ball4.DOLocalPath(path, 3.2f, PathType.CatmullRom).SetLoops(-1);


    }
    private void OnDestroy()
    {
        transform.DOKill();
    }
}
