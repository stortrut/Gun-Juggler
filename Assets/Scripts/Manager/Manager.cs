using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }

    [SerializeField] Camera menuCamera;
    private void Start()
    {
        Instance = this;
        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().name == "End")
        {
            menuCamera.enabled = true;
        }
        else
        {
            menuCamera.enabled = false;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            ProceedToNextLevel();
        }
    }
    public void ProceedToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
