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
        transform.position = new Vector3(target.position.x + offset.x, target.position.y + offset.y, target.position.z + offset.x);
        
    }
}
