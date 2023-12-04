using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementPatrolling : MonoBehaviour,IStunnable
{
    private float originalPosition;
    [SerializeField]private SliderJoint2D sliderJoint;
    [SerializeField] private float startSpeed;
    private bool once=true;
    JointMotor2D jointMotor;

    [HideInInspector] public bool isStunned = false;
    [HideInInspector] public bool timeStop;
    [HideInInspector] public float timeStun;

    public bool isStunnable { get { return isStunned; } set { isStunned = value; } }
    public float timeStunned { get { return timeStun; } set { timeStun = value; } }
    public bool timeStopped { get { return timeStop; } set { timeStop = value; } }

    void Start()
    {
        originalPosition = transform.position.x;
        Debug.Log(sliderJoint.limits.min + "min");  
        Debug.Log(sliderJoint.limits.max + "max");
        jointMotor = sliderJoint.motor;
        jointMotor.motorSpeed =startSpeed;
        sliderJoint.motor = jointMotor;

    }
    void Update()
    {
        if (isStunned)
        {
            jointMotor.motorSpeed = 0;
            return;
        }
        //if(Input.GetKeyDown(KeyCode.T)) 
        //{
        //    Debug.Log(transform.localPosition.x+"Local");
        //    Debug.Log(transform.position.x + "global");
        //}
        if(((int)transform.localPosition.x == (int)sliderJoint.limits.min ||(int)transform.localPosition.x == (int)sliderJoint.limits.max) && once==true)
        {
            Turn();
        }
    }

    public void Turn()
    {
        BoolChange();
        jointMotor = sliderJoint.motor;
        jointMotor.motorSpeed=startSpeed;
        jointMotor.motorSpeed *= -1;
        sliderJoint.motor = jointMotor;
        Invoke(nameof(BoolChange), 1f);
    }

    private void BoolChange()
    {
        once = !once;
    }   
}
