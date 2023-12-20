using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Whip : MonoBehaviour, IStunnable
{
    Tween whip;
    [SerializeField] private Rigidbody2D[] body;
    [SerializeField] private HingeJoint2D[] hingeJoints;
    [SerializeField] private Transform flagBarrier;
    private bool isStunned;
    public bool isStunnable { get { return isStunned; } set { isStunned = value; Whipping(); } }
    // Start is called before the first frame update
    void Start()
    {
        //body = GetComponent<Rigidbody2D>();
        //whip = body.DORotate(0,1).SetLoops(-1);



    }

    public void DoTheWhip()
    {
        foreach (var joint in hingeJoints)
        {
            joint.enabled = false;
        }
        foreach (var body in body)
        {
            body.isKinematic = true;
            body.gravityScale = 0;
        }
        flagBarrier.gameObject.tag = "Meelee";
        Whipping();
    }
    private void Whipping()
    {
        if (isStunned == false && flagBarrier != null)
        {
            whip = flagBarrier.DORotate(transform.rotation.eulerAngles - new Vector3(0, 0, 360), 1, RotateMode.FastBeyond360).SetLoops(-1);
        }
        else
        {
            Debug.Log("stop whippng");
            whip.Kill();
        }

    }
    void BoolChange()
    {
        isStunnable = !true;
        Debug.Log("bool changed");
        Debug.Log(isStunned);
    }

    private void OnDestroy()
    {
        whip.Kill();
    }

}
