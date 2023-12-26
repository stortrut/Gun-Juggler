using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public static Sound instance { get; private set; }

    public AudioSource backgroundSource;
    [SerializeField] private AudioSource instantiatedEffectAudioSourceGameObject;

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
    [SerializeField] public AudioClip[] notCatchingWeapon;
    [SerializeField] public AudioClip[] equipNewWeapon;


    [Header("Enemy")]
    [SerializeField] public AudioClip[] enemyTakingDamage;
    [SerializeField] public AudioClip[] enemyNotTakingDamage;

    [Header("Player")]
    [SerializeField] public AudioClip[] playerTakingDamageSounds;

    [Header("Effectsounds (pop, pof, splash etc)")]
    [SerializeField] public AudioClip[] balloonPop;
    [SerializeField] public AudioClip[] pof;
    [SerializeField] public AudioClip[] clowny;
    [SerializeField] public AudioClip[] pieSplash;
    [SerializeField] public AudioClip[] buttonClick;
    [SerializeField] public AudioClip[] spotLightOn;

    //Volume
    [HideInInspector] Slider volumeSlider;
    private float soundVolume;


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
            Debug.Log(backgroundSource.clip);
        }

        else if (SceneManager.GetActiveScene().name == "WinScene")
            backgroundSource.clip = backgroundMusicSetStartEndEtc[backgroundMusicSetStartEndEtc.Length - 1];

        else if (SceneManager.GetActiveScene().name == "End")
            backgroundSource.clip = backgroundMusicSetStartEndEtc[backgroundMusicSetStartEndEtc.Length];

        else if (backgroundSource.clip == null)
            backgroundSource.clip = backgroundMusicSetStartEndEtc[0];               //default music     
    }


    public void SoundRandomized(AudioClip[] currentsound)  //float volume)
    {
        AudioSource audioSource = Instantiate(instantiatedEffectAudioSourceGameObject, transform.position, Quaternion.identity);

        int i = Random.Range(0, currentsound.Length);
        audioSource.clip = currentsound[i];
        //input volume * set effect volume value from slider
        instantiatedEffectAudioSourceGameObject.clip = currentsound[i];
        GameObject[] soundEffectInstantiatedObjects = GameObject.FindGameObjectsWithTag("SoundEffectObject");

        //foreach (GameObject obj in soundEffectInstantiatedObjects)
        //{
        //    if (obj.GetComponent<AudioSource>().GetComponent<AudioClip[]>() == currentsound)
        //    {
        //        Destroy(audioSource.gameObject); break;
        //    }
        //}

        if (instantiatedEffectAudioSourceGameObject.clip != null)
        {
            instantiatedEffectAudioSourceGameObject.PlayOneShot(instantiatedEffectAudioSourceGameObject.clip);

            float clipLength = instantiatedEffectAudioSourceGameObject.clip.length;

            Destroy(audioSource.gameObject, clipLength);
        }
    }

    public void SoundSet(AudioClip[] currentsound, int orderedNumber)
    {
        AudioSource audioSource = Instantiate(instantiatedEffectAudioSourceGameObject, transform.position, Quaternion.identity);

        int i = orderedNumber;
        audioSource.clip = currentsound[i];
        //input volume * set effect volume value from slider
        instantiatedEffectAudioSourceGameObject.clip = currentsound[i];
        GameObject[] soundEffectInstantiatedObjects = GameObject.FindGameObjectsWithTag("SoundEffectObject");

        //foreach (GameObject obj in soundEffectInstantiatedObjects)
        //{
        //    if (obj.GetComponent<AudioSource>().GetComponent<AudioClip[]>() == currentsound)
        //    {
        //        Destroy(audioSource.gameObject); break;
        //    }
        //}

        if (instantiatedEffectAudioSourceGameObject.clip != null)
        {
            instantiatedEffectAudioSourceGameObject.PlayOneShot(instantiatedEffectAudioSourceGameObject.clip);

            float clipLength = instantiatedEffectAudioSourceGameObject.clip.length;

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
