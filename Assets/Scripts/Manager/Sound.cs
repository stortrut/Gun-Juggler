using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public static Sound instance { get; private set; }

    public AudioSource backgroundSource;
    public AudioSource soundeffectSource;

    [Header("BackgroundMusic")]
    [SerializeField] private AudioClip[] backgroundMusicSetStartEndEtc;
    [SerializeField] private AudioClip[] backgroundMusicLevelsRandom;

    [Header("Audience")]
    [SerializeField] public AudioClip[] audience;

    [Header("Weapon")]
    [SerializeField] public AudioClip[] equipWeaponSoundsWeapontypeEnumOrder;
    [SerializeField] public AudioClip[] weaponShootingSoundsEnumOrder;
    [SerializeField] public AudioClip[][] weaponShootingSoundsRandomInEnumOrder;
    [HideInInspector] public AudioClip[] notCatchingWeaponSounds;

    [Header("Enemy")]
    [SerializeField] public AudioClip[] enemyTakingDamageSounds;
    [SerializeField] public AudioClip[] enemyNotTakingDamageSounds;

    [Header("Player")]
    [SerializeField] public AudioClip[] playerTakingDamageSounds;

    [Header("Effectsounds (pop, pof, splash etc)")]
    [SerializeField] public AudioClip[] balloonPop;
    [SerializeField] public AudioClip[] pof;
    [SerializeField] public AudioClip[] clownySounds;
    [SerializeField] public AudioClip[] pieSplash;
    [SerializeField] public AudioClip[] buttonClick;
    [SerializeField] public AudioClip[] spotLightOn;

    //Volume
    [HideInInspector] Slider volumeSlider;
    private float soundVolume;


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
        if(SceneManager.GetActiveScene().buildIndex < backgroundMusicSetStartEndEtc.Length)
        {
            backgroundSource.clip = backgroundMusicSetStartEndEtc[SceneManager.GetActiveScene().buildIndex];
        }

        if (backgroundSource.clip == null)
        { backgroundSource.clip = backgroundMusicSetStartEndEtc[0]; }      //default music
        backgroundSource.PlayOneShot(soundeffectSource.clip);    
    }

    public void ChangeVolume()
    {
        soundeffectSource.volume = volumeSlider.value;
        Debug.Log(volumeSlider.value);
        Save(); 
    }

    void Save()
    {
        PlayerPrefs.SetFloat("musicvolume",volumeSlider.value);
    }

    public void SoundRandomized(AudioClip[] currentsound)
    {
        int i = Random.Range(0, currentsound.Length);
        soundeffectSource.clip = currentsound[i];
        if (soundeffectSource.clip != null)
        {
            soundeffectSource.PlayOneShot(soundeffectSource.clip);
        }
    }

    public void SoundSet(AudioClip[] currentsound, int orderedNumber)
    {
        int i = orderedNumber;
        soundeffectSource.clip = currentsound[i];
        if (soundeffectSource.clip != null)
        {
            soundeffectSource.PlayOneShot(soundeffectSource.clip);
        }
    }
}
