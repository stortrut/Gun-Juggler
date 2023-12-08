using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }

    public GameObject player;
    public int nextScene = 0;

    private AsyncOperation asyncOperation;



    private void Awake()
    {
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        Instance = this;

        if (SceneManager.GetActiveScene().name == "WinScene")
        {
            LoadNextLevel();
            Invoke(nameof(ProceedToNextLevel), 1);
        }
    }


    private void Start()
    {
        if(player == null) { Debug.Log("No player dragged into ManagerManager's Game Manager"); }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            LoadNextLevel();
            ProceedToNextLevel();
        }
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LoadSceneAsyncProcess());
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ProceedToNextLevel()
    {
        Debug.Log("Allowed Scene Activation");
        asyncOperation.allowSceneActivation = true; // error
    }





    public IEnumerator LoadSceneAsyncProcess()
    {
        // Begin to load the Scene you have specified.
        asyncOperation = SceneManager.LoadSceneAsync(nextScene);

        // Don't let the Scene activate until you allow it to.
       asyncOperation.allowSceneActivation = false;

        while (asyncOperation.isDone)
        {
            Debug.Log($"[scene]:{nextScene} [load progress]: {asyncOperation.progress}");

            yield return null;
        }
    }
}