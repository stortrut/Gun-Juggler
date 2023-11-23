using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProtection : MonoBehaviour
{
    [SerializeField] private GameObject Protection;
    [SerializeField] private Health healthScript;
    public int numberOfProtection = 1;

    private GameObject CurrentProtection;
    public void Start()
    {
        for (int i = 0; i < numberOfProtection; i++) 
        { 
          CurrentProtection=Instantiate(Protection,Vector3.one/numberOfProtection,Quaternion.identity,gameObject.transform);
            //CurrentProtection.GetComponent<>
        }
    }
}
