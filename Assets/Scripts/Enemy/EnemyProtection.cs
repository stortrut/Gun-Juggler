using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProtection : MonoBehaviour
{
    [SerializeField] private GameObject Protection;
    public int numberOfProtection;
    private GameObject CurrentProtection;
    public bool died=false;
 
    void Start()
    {
        for (int i = 0; i < numberOfProtection; i++) 
        { 
          CurrentProtection=Instantiate(Protection,Vector3.one,Quaternion.identity,gameObject.transform);
        }
    }


    void Update()
    {
        if (died) 
        {
            numberOfProtection--;
            died = false;
        }
    }
}
