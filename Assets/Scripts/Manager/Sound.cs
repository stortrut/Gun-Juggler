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
    [SerializeField] public AudioClip[] otherPositiveReactions;
    [SerializeField] public AudioClip[] audienceBoo;
    [SerializeField] public AudioClip[] murmuring;
    [SerializeField] public AudioClip[] dissapointment;
    [SerializeField] public AudioClip[] randomPositiveReactions;
    [SerializeField] public AudioClip[] randomNegativeReactions;

    [Header("Weapon")]
    [SerializeField] public AudioClip[] catchWeaponWeapontypeEnumOrder;
    [SerializeField] public AudioClip[] weaponShootingEnumOrder;
    [SerializeField] public AudioClip[][] weaponShootingRandomInEnumOrder;
    [SerializeField] public AudioClip[] equipNewWeapon;
    [SerializeField] public AudioClip[] throwUpWeapon;

    [Header("Enemy")]
    [SerializeField] public AudioClip[] enemyTakingDamageEnumOrder;
    [SerializeField] public AudioClip[] enemyNotTakingDamageEnumOrder;
    [SerializeField] public AudioClip[] dizzyPieClown;

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
    [SerializeField] public AudioClip[] poster;
    [SerializeField] public AudioClip[] windWhoosh;

    [SerializeField] public AudioClip[] shortPopping;

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
    [HideInInspector] private float backgroundMusicFadeOutOrInTime =.3f;
    [HideInInspector] float backgroundMusicTimeStamp = 0f;
    [HideInInspector] int backgroundMusicIndex;
    [HideInInspector] float fightMusicVolume = .8f;
    [HideInInspector] float backgroundMusicVolume = .7f;


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
            maxAmountOfSoundsPlayingAtSameTime = 4;
            Lights.Instance.MenuLightsOn();
        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            backgroundSource.clip = backgroundMusicSetStartEndEtc[SceneManager.GetActiveScene().buildIndex];
            maxAmountOfSoundsPlayingAtSameTime = 3;
            Lights.Instance.NormalLightsOn();
        }

        else if (SceneManager.GetActiveScene().name == "WinScene")
            backgroundSource.clip = backgroundMusicSetStartEndEtc[backgroundMusicSetStartEndEtc.Length - 1];

        else if (SceneManager.GetActiveScene().name == "End")
            backgroundSource.clip = backgroundMusicSetStartEndEtc[backgroundMusicSetStartEndEtc.Length];

        else if (backgroundSource.clip == null)
        {
            Lights.Instance.NormalLightsOff();
            maxAmountOfSoundsPlayingAtSameTime = 3;
            int randomNum = Random.Range(0, backgroundMusicLevelsInBetween.Length);
            backgroundSource.clip = backgroundMusicLevelsInBetween[randomNum];     //level random music
            backgroundMusicIndex = randomNum;   
            SoundSet(murmuring, 0, 1f);
        }
        
        if (backgroundSource.clip != null)
        {
            backgroundSource.Play();
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

            DOTweenVolumeFade(fightMusicVolume, backgroundMusicFadeOutOrInTime/4);
            yield return new WaitForSeconds(backgroundMusicFadeOutOrInTime/4);
            yield return null;
        }

        else if (!playFightMusic)
        {
            DOTweenVolumeFade(0f, backgroundMusicFadeOutOrInTime);
            yield return new WaitForSeconds(backgroundMusicFadeOutOrInTime);

            backgroundSource.clip = backgroundMusicLevelsInBetween[backgroundMusicIndex];
            backgroundSource.time = backgroundMusicTimeStamp;
            backgroundSource.Play();

            DOTweenVolumeFade(backgroundMusicVolume, backgroundMusicFadeOutOrInTime);
            yield return new WaitForSeconds(backgroundMusicFadeOutOrInTime);
            yield return null;
        }
    }

    public void DOTweenVolumeFade(float targetVolume, float fadeDuration)
    {
        backgroundSource.DOFade(targetVolume, fadeDuration);
    }

    public void SoundRandomized(AudioClip[] currentsound, float volume = 1f, float randomPitchRange = 0f, float randomVolumeRange = 0f)  //float volume)
    {
        GameObject instantiatedEffectAudioSourceGameObject = Instantiate(effectAudioSourceGameObject, new Vector3(0, 0, 0), Quaternion.identity);
        AudioSource audioSource = instantiatedEffectAudioSourceGameObject.GetComponent<AudioSource>();

        int i = Random.Range(0, currentsound.Length);
        audioSource.clip = currentsound[i];
        audioSource.volume = volume;

        float y = Random.Range(-randomPitchRange/2, randomPitchRange/2);
        audioSource.pitch = 1 + y;

        float u = Random.Range(-randomVolumeRange / 2, randomVolumeRange / 2);
        audioSource.volume = volume + u;
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
