using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Started Scene Preloading");
            AudienceSatisfaction.Instance.ActStarted();
            // Start scene preloading.
            Score.Instance.DisplayScore();
            
        }
    }
}


