using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour, IStunnable
{
    [SerializeField] GameObject optionsPanel;
    [SerializeField] bool optionsPanelActive = false;
    [SerializeField] GameObject escMenu;
    [SerializeField] bool escMenuActive = false;
    [SerializeField] IStunnable stunnable;

    public bool isStunnable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float timeStunned { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public bool timeStop { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    private void Start()
    {
        stunnable = GetComponent<IStunnable>();
        optionsPanel.SetActive(false);  
        escMenu.SetActive(false);
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
        if (Input.GetKeyDown(KeyCode.Escape) && !(SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().name == "End"))
        {
            Debug.Log(SceneManager.GetActiveScene().name);
            escMenu.SetActive(true);
            stunnable.isStunnable = true;
            stunnable.timeStop = true;
        }
    }
    public void StartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void OptionsButton()
    {
        optionsPanel.SetActive(true);
        optionsPanelActive = true;
    }
    public void BackToMenuButton()
    {
        SceneManager.LoadScene(0);
    }
    public void BackToGameButton()
    {
        escMenu.SetActive(false);
        escMenuActive = false;
        optionsPanel.SetActive(false);
        optionsPanelActive = false;
        
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
