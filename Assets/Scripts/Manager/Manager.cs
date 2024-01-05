using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }

    [HideInInspector] public GameObject player;

    public int nextScene = 0;
    [SerializeField] private Texture2D cursorImage;
    private AsyncOperation asyncOperation;
    [SerializeField] GameObject fadingPanel;
    private Image fadingPanelImage;
    private Color panelColor;

    private void Awake()
    {
        Instance = this;
    }
        
    private void Start()
    {
        //  player = FindObjectOfType<PlayerJuggle>()?.gameObject;
        Cursor.SetCursor(cursorImage, new Vector2(cursorImage.width / 2, cursorImage.height / 2), CursorMode.Auto);
       
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        //if (SceneManager.GetActiveScene().name == "WinScene")
        //{
        //    LoadNextLevel();
        //    Invoke(nameof(ProceedToNextLevel), 10);
        //}

        //Debug.Log("buildindex" + SceneManager.GetActiveScene().buildIndex);
        if ((SceneManager.GetActiveScene().buildIndex == 0))
        {
            Debug.Log("player prefs is set, build 0");
            PlayerPrefs.SetFloat("fadein", 1f);
        }

        if (player == null) { Debug.Log("Did not find a player ERROR"); }


        fadingPanelImage = fadingPanel.GetComponentInChildren<Image>();
        Debug.Log("image component"+  fadingPanelImage);
        //fade in music and light
        FadeInOrOutSoundAndLight(true, PlayerPrefs.GetFloat("fadein"));
    }


    private void Update()
    {
        if (player == null)
            player = FindObjectOfType<PlayerJuggle>()?.gameObject;

        if(player == null) { Debug.Log("DOES STILL NOT FIND PLAYER ERROR!"); }


        if (Input.GetKeyDown(KeyCode.N))
        {
            LoadNextSceneNoTransition();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            LoadNextSceneNoTransition();
        }
    }

    void FadeInOrOutSoundAndLight(bool fadingIn, float fadeInDuration)
    {
        float targetAlpha;
        float startAlpha;
        if (fadingIn)
        { 
            targetAlpha = 0;  startAlpha = 1;
        }
        else
        { 
            targetAlpha = 1; startAlpha = 0;
            Debug.Log("fadeout");
        }

        Sound.Instance.DOTweenVolumeFade(startAlpha, fadeInDuration);

        StartCoroutine(FadeAlpha(targetAlpha, startAlpha, fadeInDuration));
        //colorToFade.DOFade(targetAlpha, fadeInDuration);
    }

    private IEnumerator FadeAlpha(float targetAlpha, float startAlpha, float duration)
    {
        for (float i = 0; i < 200; i++)
        {
            yield return new WaitForSeconds(0.001f);


            if(fadingPanelImage != null)
                fadingPanelImage.color = new Color(0, 0, 0, Mathf.Lerp(startAlpha, targetAlpha, (i / 200)));
        }
        yield return new WaitForSeconds(2);



        //currentAlpha.DOFade
        //currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, fadeinDuration);

        //fadingPanelImage.color.Lerp(currentAlpha, targetAlpha, fadeinDuration);
        //yield return new WaitForSeconds(fadeinDuration);
    }
    
    //only call these two:
    public void LoadNextSceneNoTransition()
    {
        LoadNextLevel();
        PlayerPrefs.SetFloat("fadein", 0);
        ProceedToNextLevel();
    }

    public void LoadNextSceneWithTransition(float musicTransitionDuration)
    {
        StartCoroutine(nameof(TransitionHandler), musicTransitionDuration);
    }
    

    //-------------------------------------------------------

    private IEnumerator TransitionHandler(float musicTransitionDuration)
    {
        LoadNextLevel();
        //fade out music and light
        FadeInOrOutSoundAndLight(false, 1);
        Sound.Instance.DOTweenVolumeFade(0, musicTransitionDuration);
        PlayerPrefs.SetFloat("fadein",musicTransitionDuration / 2);

        yield return new WaitForSeconds(1);

        ProceedToNextLevel();
    }

    public void LoadNextLevel()         //    fade
    {
        StartCoroutine(LoadSceneAsyncProcess());            
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.DeleteAll();
    }

    public void ProceedToNextLevel()           //      byt
    {
        //Debug.Log("Allowed Scene Activation");
        asyncOperation.allowSceneActivation = true; 
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