using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }

    public int nextScene = 0;

    private AsyncOperation _asyncOperation;



    private void Start()
    {
        nextScene= SceneManager.GetActiveScene().buildIndex+1;
        Instance = this;

        if (SceneManager.GetActiveScene().name == "WinScene")
        {
            Invoke(nameof(ProceedToNextLevel), 5);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
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

            this._asyncOperation.allowSceneActivation = true;
    }
        




public IEnumerator LoadSceneAsyncProcess()
    {
        // Begin to load the Scene you have specified.
        this._asyncOperation = SceneManager.LoadSceneAsync(nextScene);

        // Don't let the Scene activate until you allow it to.
        this._asyncOperation.allowSceneActivation = false;

        while (!this._asyncOperation.isDone)
        {
            Debug.Log($"[scene]:{nextScene} [load progress]: {this._asyncOperation.progress}");

            yield return null;
        }
    }
}