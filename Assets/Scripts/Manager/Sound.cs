using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class Sound : MonoBehaviour
{
    public static Sound Instance { get; private set; }

    public AudioSource backgroundSource;
    [SerializeField] private GameObject effectAudioSourceGameObject;

    [Header("BackgroundMusic")]
    [SerializeField] private AudioClip[] backgroundMusicSetStartEndEtc;
    [SerializeField] private AudioClip[] backgroundMusicLevelsInBetween;
    [SerializeField] private AudioClip[] fightBackgroundMusicLevels;

    [Header("Audience")]
    [SerializeField] public AudioClip[] audienceApplauding;
    [SerializeField] public AudioClip[] audienceBoo;
    [SerializeField] public AudioClip[] onePersonDissapointed;

    [Header("Weapon")]
    [SerializeField] public AudioClip[] catchWeaponWeapontypeEnumOrder;
    [SerializeField] public AudioClip[] weaponShootingEnumOrder;
    [SerializeField] public AudioClip[][] weaponShootingRandomInEnumOrder;
    [SerializeField] public AudioClip[] equipNewWeapon;
    [SerializeField] public AudioClip[] throwUpWeapon;

    [Header("Enemy")]
    [SerializeField] public AudioClip[] enemyTakingDamageEnumOrder;
    [SerializeField] public AudioClip[] enemyNotTakingDamageEnumOrder;

    [Header("Player")]
    [SerializeField] public AudioClip[] playerTakingDamageSounds;
    [SerializeField] public AudioClip[] ohNo;
    [SerializeField] public AudioClip[] jumpVoice;

    [Header("Splash")]
    [SerializeField] public AudioClip[] pieSplash;
    [SerializeField] public AudioClip[] waterSplash;

    [Header("Effectsounds (pop, pof etc)")]
    [SerializeField] public AudioClip[] balloonFirePop;
    [SerializeField] public AudioClip[] hoopFire;

    [SerializeField] public AudioClip[] balloonPop;
    [SerializeField] public AudioClip[] balloonSqueek;
    [SerializeField] public AudioClip[] poof;
    [SerializeField] public AudioClip[] explosion;

    [SerializeField] public AudioClip[] clowny;

    [SerializeField] public AudioClip[] buttonClick;
    [SerializeField] public AudioClip[] spotLightOn;

    [SerializeField] public AudioClip[] landingWithBike;
    [SerializeField] public AudioClip[] hampter;

    [Header("Other")]
    [SerializeField] private float backgroundMusicFadeOutOrInTime;
    [HideInInspector] float backgroundMusicTimeStamp = 0f;

    //Volume
    [HideInInspector] Slider volumeSlider;
    private float soundVolume;
    [ReadOnly] int maxAmountOfSoundsPlayingAtSameTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        //Debug.Log("buildindex"+SceneManager.GetActiveScene().buildIndex);

        if ((SceneManager.GetActiveScene().buildIndex == 0)) 
        {
            backgroundSource.clip = backgroundMusicSetStartEndEtc[SceneManager.GetActiveScene().buildIndex];
            maxAmountOfSoundsPlayingAtSameTime = 1;
        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            backgroundSource.clip = backgroundMusicSetStartEndEtc[SceneManager.GetActiveScene().buildIndex];
            maxAmountOfSoundsPlayingAtSameTime = 2;
        }

        else if (SceneManager.GetActiveScene().name == "WinScene")
            backgroundSource.clip = backgroundMusicSetStartEndEtc[backgroundMusicSetStartEndEtc.Length - 1];

        else if (SceneManager.GetActiveScene().name == "End")
            backgroundSource.clip = backgroundMusicSetStartEndEtc[backgroundMusicSetStartEndEtc.Length];

        else if (backgroundSource.clip == null)
        {
            maxAmountOfSoundsPlayingAtSameTime = 3;
            int randomNum = Random.Range(0, backgroundMusicLevelsInBetween.Length);
            backgroundSource.clip = backgroundMusicLevelsInBetween[randomNum];     //level random music
        }
        
        if (backgroundSource.clip != null)
        {
            backgroundSource.Play();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ChangeBackgroundMusic(true);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            ChangeBackgroundMusic(false);
        }
    }

    public void ChangeBackgroundMusic(bool playFightMusic) 
    {
        StartCoroutine(PlayFightMusic(playFightMusic));
    }

    private IEnumerator PlayFightMusic(bool playFightMusic)
    {
        if (playFightMusic)
        {
            DOTweenVolumeFade(0f, backgroundMusicFadeOutOrInTime);
            yield return new WaitForSeconds(backgroundMusicFadeOutOrInTime);

            backgroundMusicTimeStamp = backgroundSource.time;
            backgroundSource.time = 0;
            int randomNum = Random.Range(0, fightBackgroundMusicLevels.Length);
            backgroundSource.clip = fightBackgroundMusicLevels[randomNum];
            backgroundSource.Play();

            DOTweenVolumeFade(1f, backgroundMusicFadeOutOrInTime/4);
            yield return new WaitForSeconds(backgroundMusicFadeOutOrInTime/4);
            yield return null;
        }

        else if (!playFightMusic)
        {
            DOTweenVolumeFade(0f, backgroundMusicFadeOutOrInTime);
            yield return new WaitForSeconds(backgroundMusicFadeOutOrInTime);

            int randomNum = Random.Range(0, backgroundMusicLevelsInBetween.Length);
            backgroundSource.clip = backgroundMusicLevelsInBetween[randomNum];
            backgroundSource.time = backgroundMusicTimeStamp;
            backgroundSource.Play();

            DOTweenVolumeFade(1f, backgroundMusicFadeOutOrInTime);
            yield return new WaitForSeconds(backgroundMusicFadeOutOrInTime);
            yield return null;
        }
    }

    public void DOTweenVolumeFade(float targetVolume, float fadeDuration)
    {
        backgroundSource.DOFade(targetVolume, fadeDuration);
    }

    public void SoundRandomized(AudioClip[] currentsound, float volume = 1f, float randomRange = 0f)  //float volume)
    {
        GameObject instantiatedEffectAudioSourceGameObject = Instantiate(effectAudioSourceGameObject, new Vector3(0, 0, 0), Quaternion.identity);
        AudioSource audioSource = instantiatedEffectAudioSourceGameObject.GetComponent<AudioSource>();

        int i = Random.Range(0, currentsound.Length);
        audioSource.clip = currentsound[i];
        audioSource.volume = volume;

        float y = Random.Range(-randomRange/2, randomRange/2);
        audioSource.pitch = 1 + y;
        //input volume * set effect volume value from slider

        GameObject[] soundEffectInstantiatedObjects = GameObject.FindGameObjectsWithTag("SoundEffectObject");
        int o = 0;

        foreach (GameObject obj in soundEffectInstantiatedObjects)
        {
            if (obj.GetComponent<AudioSource>().clip.name == audioSource.clip.name)
            {
                o++;
            }
            if (o >= maxAmountOfSoundsPlayingAtSameTime)
            {
                Destroy(audioSource.gameObject); return;
            }
        }

        if (audioSource.clip != null)
        {
            float clipLength = audioSource.clip.length;

            Destroy(audioSource.gameObject, clipLength);
            audioSource.Play();
        }
        else
        {
            Destroy(audioSource.gameObject);
        }
    }

    public void SoundSet(AudioClip[] currentsound, int orderedNumber, float volume = 1, float randomRange = 0f)
    {
        GameObject instantiatedEffectAudioSourceGameObject = Instantiate(effectAudioSourceGameObject, new Vector3(0, 0, 0), Quaternion.identity);
        AudioSource audioSource = instantiatedEffectAudioSourceGameObject.GetComponent<AudioSource>();

        int i = orderedNumber;
        audioSource.clip = currentsound[i];
        //input volume * set effect volume value from slider
        audioSource.volume = volume;

        float y = Random.Range(-randomRange / 2, randomRange / 2);
        audioSource.pitch = 1 + y;

        GameObject[] soundEffectInstantiatedObjects = GameObject.FindGameObjectsWithTag("SoundEffectObject");

        int o = 0;

        foreach (GameObject obj in soundEffectInstantiatedObjects)
        {
            if (obj.GetComponent<AudioSource>().clip.name == audioSource.clip.name)
            {
                o++;
            }
            if (o >= maxAmountOfSoundsPlayingAtSameTime)
            {
                Destroy(audioSource.gameObject); return;
            }
        }

        if (audioSource.clip != null)
        {
            audioSource.Play();

            float clipLength = audioSource.clip.length;

            Destroy(audioSource.gameObject, clipLength);
        }
        else
        {
            Destroy(audioSource.gameObject);
        }
    }


    //public void ChangeVolume()
    //{
    //    soundeffectSourcePrefab.volume = volumeSlider.value;
    //    Debug.Log(volumeSlider.value);
    //    Save(); 
    //}

    //void Save()
    //{
    //    PlayerPrefs.SetFloat("musicvolume",volumeSlider.value);
    //}
}
