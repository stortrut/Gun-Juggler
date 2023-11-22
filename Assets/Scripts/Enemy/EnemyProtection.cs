using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProtection : MonoBehaviour
{
    [SerializeField] private GameObject Protection;
    [SerializeField] private int numberOfProtection;
 
    void Start()
    {
        for (int i = 0; i < numberOfProtection; i++) 
        { 
          Instantiate(Protection,transform.parent);
        }
    }


    void Update()
    {
        
    }
}
