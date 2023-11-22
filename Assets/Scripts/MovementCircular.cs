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
   


    void Update()
    {
        x = Mathf.Cos(Time.time*frequency)*amplitude;
        y = Mathf.Sin(Time.time*frequency)*amplitude;
        z = transform.position.z;
        transform.position = new Vector3(AttachedEnemy.transform.position.x+x, AttachedEnemy.transform.position.y+y, z);
    }
}
