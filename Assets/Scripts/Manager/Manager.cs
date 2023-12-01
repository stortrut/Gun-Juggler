using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }

    private void Start()
    {
        Instance = this;
        if(SceneManager.GetActiveScene().name == "WinScene" )
        {
            Invoke(nameof(ProceedToNextLevel),5);
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
