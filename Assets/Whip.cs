using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Whip : MonoBehaviour
{
    Tween whip;
    [SerializeField] private Rigidbody2D[] body;
    [SerializeField] private HingeJoint2D[] hingeJoints;
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
        whip = transform.DORotate(transform.rotation.eulerAngles - new Vector3(0, 0, 360), 1, RotateMode.FastBeyond360).SetLoops(-1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
