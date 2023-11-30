using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementPatrolling : MonoBehaviour
{
    private float originalPosition;
    [SerializeField]private SliderJoint2D sliderJoint;
    [SerializeField] private float startSpeed;
    private bool once=true;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position.x;
        Debug.Log(sliderJoint.limits.min + "min");  
        Debug.Log(sliderJoint.limits.max + "max");
        JointMotor2D jointMotor = sliderJoint.motor;
        jointMotor.motorSpeed =startSpeed;
        sliderJoint.motor = jointMotor;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)) 
        {
            Debug.Log(transform.localPosition.x+"Local");
            Debug.Log(transform.position.x + "global");
        }
        if(((int)transform.localPosition.x == (int)sliderJoint.limits.min ||(int)transform.localPosition.x == (int)sliderJoint.limits.max) && once==true)
        {
            BoolChange();
            JointMotor2D jointMotor=sliderJoint.motor;
            jointMotor.motorSpeed *= -1;
            sliderJoint.motor = jointMotor;
            Invoke(nameof(BoolChange),1f);
        }
    }
    private void BoolChange()
    {
        once = !once;
    }
}
