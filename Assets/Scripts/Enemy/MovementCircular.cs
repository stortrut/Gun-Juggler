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
        offSet = transform.position.z;
    }
    

    void Update()
    {
        x = Mathf.Cos((Time.time + offSet)* frequency) * amplitude;
        y = Mathf.Sin((Time.time + offSet) * frequency) * amplitude;

        transform.position = new Vector3 (transform.parent.position.x + x , transform.parent.position.y + y,0);
    }   
}   
    