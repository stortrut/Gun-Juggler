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

            // Start scene preloading.
            Manager.Instance.LoadNextSceneWithTransition(1);
        }
    }
}


