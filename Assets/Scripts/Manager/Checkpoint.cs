using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
   
    private float timeStart=200;
  

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timeStart = Time.time;
            Debug.Log("Started Scene Preloading");

            // Start scene preloading.
            Manager.Instance.LoadNextLevel();


        }
    }
    private void Update()
    {
        if ((int)Time.time == (int)timeStart + 1)
        {
            //Debug.Log("Allowed Scene Activation");

            Manager.Instance.ProceedToNextLevel();
        }
        //Manager.Instance.Invoke("ProceedToNextLevel", .1f);
    }
}


