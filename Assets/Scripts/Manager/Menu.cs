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
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject endMenu;
    [SerializeField] GameObject scoreMenu;
    [SerializeField] IStunnable stunnable;

    public bool isStunnable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    private void Awake()
    {
        Time.timeScale = 1f;
        //Debug.Log(SceneManager.GetActiveScene().buildIndex);
        stunnable = GetComponent<IStunnable>();
        optionsPanel.SetActive(false);  
        escMenu.SetActive(false);
        optionsPanelActive = false;
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            startMenu.SetActive(true);
        }
        else
        {
            startMenu.SetActive(false);
        }
        if (SceneManager.GetActiveScene().name == "End")
        {
            endMenu.SetActive(true);
        }
        else if (SceneManager.GetActiveScene().name != "End")
        {
            endMenu.SetActive(false);
        }
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (optionsPanelActive && SceneManager.GetActiveScene().buildIndex == 0)
            {
                optionsPanel.SetActive(false);
                startMenu.SetActive(true);
                optionsPanelActive = false;
            }
            else if (optionsPanelActive && SceneManager.GetActiveScene().buildIndex != 0)
            {
                optionsPanel.SetActive(false);
                escMenu.SetActive(true);
                optionsPanelActive = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !(SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().name == "End"))
        {
            escMenu.SetActive(true);
            //stunnable.isStunnable = true;
            //stunnable.timeStopped = true;       
            Time.timeScale = 0f;
        }
    }
    public void StartButton()
    {
        PlayerPrefs.SetInt("cameraPan", 0);
        Manager.Instance.LoadNextSceneWithTransition(1);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void OptionsButton()
    {
        optionsPanel.SetActive(true);
        optionsPanelActive = true;
        startMenu.SetActive(false);
        escMenu.SetActive(false);
    }
    public void BackToMenuButton()
    {
        SceneManager.LoadScene(0);
        endMenu.SetActive(false);   
    }
    public void BackToGameButton()
    {
        Time.timeScale = 1f;
        escMenu.SetActive(false);
        optionsPanelActive = false;
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void ScoreButton()
    {
        PlayerPrefs.SetInt("cameraPan", 0);
        Manager.Instance.LoadNextSceneWithTransition(1);
        scoreMenu.SetActive(false);
    }
}
