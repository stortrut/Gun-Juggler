using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public static Sound instance { get; private set; }

    public AudioSource backgroundSource;
    [SerializeField] private GameObject effectAudioSourceGameObject;

    [Header("BackgroundMusic")]
    [SerializeField] private AudioClip[] backgroundMusicSetStartEndEtc;
    [SerializeField] private AudioClip[] backgroundMusicLevelsRandom;

    [Header("Audience")]
    [SerializeField] public AudioClip[] audienceApplaud;
    [SerializeField] public AudioClip[] audienceBoo;

    [Header("Weapon")]
    [SerializeField] public AudioClip[] equipWeaponWeapontypeEnumOrder;
    [SerializeField] public AudioClip[] weaponShootingEnumOrder;
    [SerializeField] public AudioClip[][] weaponShootingRandomInEnumOrder;
    [SerializeField] public AudioClip[] equipNewWeapon;

    [Header("Enemy")]
    [SerializeField] public AudioClip[] enemyTakingDamageEnumOrder;
    [SerializeField] public AudioClip[] enemyNotTakingDamageEnumOrder;

    [Header("Player")]
    [SerializeField] public AudioClip[] playerTakingDamageSounds;
    [SerializeField] public AudioClip[] ohNo;

    [Header("Effectsounds (pop, pof, splash etc)")]
    [SerializeField] public AudioClip[] balloonPop;
    [SerializeField] public AudioClip[] poof;
    [SerializeField] public AudioClip[] clowny;
    [SerializeField] public AudioClip[] pieSplash;
    [SerializeField] public AudioClip[] buttonClick;
    [SerializeField] public AudioClip[] spotLightOn;

    //Volume
    [HideInInspector] Slider volumeSlider;
    private float soundVolume;
    [SerializeField] int maxAmountOfSoundsPlayingAtSameTime = 3;


    //audiosource.time = sätt variabel till tiden i slutet av poster scenen och börja därifrån nästa gång
    //crossfade, sänk volymen på bakgrundsmusiken i slutet och övergå i nästa, ny musik vid näata wave eller level eller varje gång låten tar slut?


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Debug.Log("buildindex"+SceneManager.GetActiveScene().buildIndex);
        if ((SceneManager.GetActiveScene().buildIndex == 0) || (SceneManager.GetActiveScene().buildIndex == 1))
        {
            backgroundSource.clip = backgroundMusicSetStartEndEtc[SceneManager.GetActiveScene().buildIndex];
        }

        else if (SceneManager.GetActiveScene().name == "WinScene")
            backgroundSource.clip = backgroundMusicSetStartEndEtc[backgroundMusicSetStartEndEtc.Length - 1];

        else if (SceneManager.GetActiveScene().name == "End")
            backgroundSource.clip = backgroundMusicSetStartEndEtc[backgroundMusicSetStartEndEtc.Length];

        else if (backgroundSource.clip == null)
        {
            int randomNum = Random.Range(0, backgroundMusicLevelsRandom.Length);
            backgroundSource.clip = backgroundMusicLevelsRandom[randomNum];     //level random music
        }
        
        if (backgroundSource.clip != null)
        {
            backgroundSource.Play();
        }
    }


    public void SoundRandomized(AudioClip[] currentsound)  //float volume)
    {
        GameObject instantiatedEffectAudioSourceGameObject = Instantiate(effectAudioSourceGameObject, new Vector3(0, 0, 0), Quaternion.identity);
        AudioSource audioSource = instantiatedEffectAudioSourceGameObject.GetComponent<AudioSource>();

        int i = Random.Range(0, currentsound.Length);
        audioSource.clip = currentsound[i];
        //input volume * set effect volume value from slider
        audioSource.clip = currentsound[i];

        GameObject[] soundEffectInstantiatedObjects = GameObject.FindGameObjectsWithTag("SoundEffectObject");
        int o = 0;

        foreach (GameObject obj in soundEffectInstantiatedObjects)
        {
            if (obj.GetComponent<AudioSource>().clip.name == audioSource.clip.name)
            {
                o++;
                Debug.Log(o);
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

    public void SoundSet(AudioClip[] currentsound, int orderedNumber)
    {
        GameObject instantiatedEffectAudioSourceGameObject = Instantiate(effectAudioSourceGameObject, new Vector3(0, 0, 0), Quaternion.identity);
        AudioSource audioSource = instantiatedEffectAudioSourceGameObject.GetComponent<AudioSource>();

        int i = orderedNumber;
        audioSource.clip = currentsound[i];
        //input volume * set effect volume value from slider
        audioSource.clip = currentsound[i];
        GameObject[] soundEffectInstantiatedObjects = GameObject.FindGameObjectsWithTag("SoundEffectObject");

        int o = 0;

        foreach (GameObject obj in soundEffectInstantiatedObjects)
        {
            if (obj.GetComponent<AudioSource>().clip.name == audioSource.clip.name)
            {
                o++;
                Debug.Log(o);
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
