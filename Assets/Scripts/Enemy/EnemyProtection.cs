using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProtection : MonoBehaviour
{
    [SerializeField] private GameObject Protection;
    public int numberOfProtection = 1;

    private GameObject CurrentProtection;
    public void Start()
    {
        for (int i = 1; i < numberOfProtection+1; i++) 
        { 
          CurrentProtection=Instantiate(Protection,new Vector2(i,i),Quaternion.identity,gameObject.transform);
            
        }
    }
}
