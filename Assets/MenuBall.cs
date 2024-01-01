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
    [SerializeField] private float timeToCompleteLoop;
    [SerializeField] private float amplitude;
    [SerializeField] private RectTransform button;
    [SerializeField] private RectTransform button2;
    [SerializeField] private RectTransform button3;
    [SerializeField] private RectTransform button4;
    public Vector3[] path;
    bool on;

    void Start()
    {
        //var x = Mathf.Cos((Time.time ) * frequency) * amplitude;
        //var y = Mathf.Sin((Time.time) * frequency) * amplitude;
        ball.DOLocalPath(path, timeToCompleteLoop, PathType.CatmullRom).SetLoops(-1);
        StartCoroutine(OtherBalls());
    }
    public IEnumerator OtherBalls()
    {
        yield return new WaitForSeconds(timeToCompleteLoop/ 4);
        ball2.DOLocalPath(path, timeToCompleteLoop, PathType.CatmullRom).SetLoops(-1);
        yield return new WaitForSeconds(timeToCompleteLoop/ 4);
        ball3.DOLocalPath(path, timeToCompleteLoop, PathType.CatmullRom).SetLoops(-1);
        yield return new WaitForSeconds(timeToCompleteLoop/ 4);
        ball4.DOLocalPath(path, timeToCompleteLoop, PathType.CatmullRom).SetLoops(-1);


    }
    private void OnDestroy()
    {
        transform.DOKill();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (!on)
            {
                button.DOAnchorPos(path[0], 0.001f);
                button.DOLocalPath(path, timeToCompleteLoop, PathType.CatmullRom).SetLoops(-1);

                StartCoroutine(Button());
                on = !on;

            }
            else
            {
                transform.DOKill();
                //button.DOMove(startPos, 1);
                on = !on;
            }


        }
    }
    public IEnumerator Button()
    {
        yield return new WaitForSeconds(timeToCompleteLoop/ 4);
        button2.DOAnchorPos(path[0], 0.001f);
        button2.DOLocalPath(path,timeToCompleteLoop, PathType.CatmullRom).SetLoops(-1);
        yield return new WaitForSeconds(timeToCompleteLoop/ 4);
        button3.DOAnchorPos(path[0], 0.001f);
        button3.DOLocalPath(path, timeToCompleteLoop, PathType.CatmullRom).SetLoops(-1);
        yield return new WaitForSeconds(timeToCompleteLoop/ 4);
        button4.DOAnchorPos(path[0], 0.001f);
        button4.DOLocalPath(path, timeToCompleteLoop, PathType.CatmullRom).SetLoops(-1);


    }
}
