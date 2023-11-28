using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [SerializeField] Camera menuCamera;
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().name == "End")
        {
            menuCamera.enabled = true;
            Debug.Log("camera enabled");
        }
        else
        {
            menuCamera.enabled = false;
            Debug.Log("camera not enabled");
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            ProceedToNextLevel();
        }
    }
    void ProceedToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
