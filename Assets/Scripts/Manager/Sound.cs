using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public static Sound Instance { get; private set; }

    [SerializeField] private AudioSource backgroundSource;
    [SerializeField] private AudioSource source;
    private float soundVolume;
    [SerializeField] private AudioClip[] backgroundMusic;
    [SerializeField] public AudioClip[] shootingSoundsConfettiGun;
    [SerializeField] public AudioClip[] shootingSounds;
    [SerializeField] public AudioClip[] enemyTakingDamageSounds;
    [SerializeField] public AudioClip[] enemyNotTakingDamageSounds;
    [SerializeField] public AudioClip[] changingWeaponSounds;
    [SerializeField] public AudioClip[] catchingWeaponSounds;
    [SerializeField] public AudioClip[] notCatchingWeaponSounds;
    [SerializeField] public AudioClip[] equipWeaponSounds;
    [SerializeField] public AudioClip[] playerTakingDamageSounds;


    [SerializeField] Slider volumeSlider;


    private void Awake()
    {
        Instance = this;
        //backgroundSource = GetComponent<AudioSource>();
        //backgroundMusic = GetComponent<AudioClip[]>();
        //kaboom = GetComponent<AudioClip[]>();
    }

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex < backgroundMusic.Length)
        {
            backgroundSource.clip = backgroundMusic[SceneManager.GetActiveScene().buildIndex];
        }

        //source.volume = source.volume * 0.1f;
        if (backgroundSource.clip == null)
        { backgroundSource.clip = backgroundMusic[0]; }      //default music
        backgroundSource.PlayOneShot(source.clip);    
    }

    public void ChangeVolume()
    {
        source.volume = volumeSlider.value;
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
        source.clip = currentsound[i];
        if (source.clip != null)
        {
            source.PlayOneShot(source.clip);
        }
    }
    public void SoundSet(AudioClip[] currentsound, int orderedNumber)
    {
        int i = orderedNumber;
        source.clip = currentsound[i];
        if (source.clip != null)
        {
            source.PlayOneShot(source.clip);
        }
    }

    //public void Kaboom()
    //{
    //    int i = Random.Range(0, kaboom.Length);
    //    source.clip = kaboom[i];
    //    source.PlayOneShot(source.clip);
    //}

    //public void Ultimate()
    //{
    //    source.clip = ultimateSound;
    //    source.volume = source.volume * 5f;
    //    source.PlayOneShot(source.clip);
    //}
}
