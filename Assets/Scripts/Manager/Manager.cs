using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }

    [HideInInspector] public GameObject player;
    public Camera mainCamera;

    public int nextScene = 0;
    [SerializeField] private Texture2D cursorImage;
    private AsyncOperation asyncOperation;
    [SerializeField] GameObject fadeToBlack;




    private void Awake()
    {
       
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        Cursor.SetCursor(cursorImage, new Vector2(cursorImage.width / 2, cursorImage.height / 2), CursorMode.Auto);
        player = FindObjectOfType<PlayerJuggle>()?.gameObject; 
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        if (SceneManager.GetActiveScene().name == "WinScene")
        {
            LoadNextLevel();
            Invoke(nameof(ProceedToNextLevel), 10);
        }
    }


    private void Start()
    {
        //  player = FindObjectOfType<PlayerJuggle>()?.gameObject;
        if (player == null) { Debug.Log("Did not find a player ERROR"); }
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
        fadeToBlack.SetActive(true);
        Invoke(nameof(SetActiveFalse), 7);
    }
    public void ProceedToNextLevel()
    {
        //Debug.Log("Allowed Scene Activation");
        asyncOperation.allowSceneActivation = true;
    }

    private void SetActiveFalse()
    {
        fadeToBlack.SetActive(false);
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