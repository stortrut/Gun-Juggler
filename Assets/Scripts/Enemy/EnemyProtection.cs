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
        for (int i = 0; i < numberOfProtection; i++) 
        { 
          CurrentProtection=Instantiate(Protection,Vector3.one,Quaternion.identity,gameObject.transform);
            //CurrentProtection.GetComponent<>
        }
    }

    public void ChildDied()
    {
        Debug.Log("It does happen");
            numberOfProtection--;
    }
}
