using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject optionsPanel;
    [SerializeField] bool optionsPanelActive = false;
    private void Start()
    {
        optionsPanel.SetActive(false);  
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (optionsPanelActive)
            {
                optionsPanel.SetActive(false);
            }
        }
    }
    public void StartButton()
    {
        Debug.Log("start");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitButton()
    {
        Debug.Log("quit");
        Application.Quit();
    }
    public void OptionsButton()
    {
        optionsPanel.SetActive(true);
        optionsPanelActive = true;
    }
}
