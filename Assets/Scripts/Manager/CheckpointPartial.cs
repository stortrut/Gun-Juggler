using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckpointPartial : MonoBehaviour
{
    [Header("Write the level and checkpoint number")]
    public int level;
    public int checkPointNumber;

    private void Start()
    {
        // This activates on the checkpoint which has the corresponding value that is saved in playerprefs
        if (checkPointNumber == PlayerPrefs.GetInt("checkPointNumber" + level))
        {
            Debug.Log("checkpoint");
            Debug.Log(checkPointNumber);
            GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;
            
        }
    }

    // Checks when the collider triggers the checkpoint and saves it in playerprefs data
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("checkpoint");
            Debug.Log(checkPointNumber);
            PlayerPrefs.SetInt("checkPointNumber" + level, checkPointNumber);
        }
        // Confettigun = 1 , SmallGun = 0 , Trumpet = 2
        string test = "CTTSST";

        for (int i = 0; i < test.Length; i++)
        {
            switch (test[i])
            {
                case 'C':
                   var weaponToAdd = WeaponType.SmallGun;
                    //Do stuff
                    break;
                case 'S':
                    //Do stuff
                    break;
                default:
                    //somthing went wrong
                    break;
            }
            //add weaponToAdd = 

            if (test[i] == 'C')
            {
                
            }
        }


    }
}