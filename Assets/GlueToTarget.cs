using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueToTarget : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public float valueY;
    void Update()
    {
        //
        this.transform.position = new Vector3(target.position.x + offset.x, valueY + offset.y, target.position.z + offset.x);
        
    }
}
