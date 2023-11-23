using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCircular : MonoBehaviour
{   
    [SerializeField] private float frequency;
    [SerializeField] private float amplitude;
    [SerializeField] private GameObject AttachedEnemy;
    private float x;
    private float y;
    private float z;
    private float offSet;
   
    void Start()
    {
        offSet = transform.position.x;
    }
    

    void Update()
    {
        x = offSet+Mathf.Cos(Time.time* frequency)*amplitude;
        y = offSet+Mathf.Sin(Time.time* frequency)*amplitude;
        z = transform.position.z;
        transform.position = new Vector3(transform.parent.position.x+x,transform.parent.position.y+y, z);
    }   
}
