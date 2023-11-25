using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sound : MonoBehaviour
{
    public static Sound Instance { get; private set; }

    private AudioSource source;
    [SerializeField] private AudioClip[] backgroundMusic;
    [SerializeField] public AudioClip[] shootingSounds;
    [SerializeField] public AudioClip[] enemyTakingDamageSounds;
    [SerializeField] public AudioClip[] enemyNotTakingDamageSounds;

    private void Awake()
    {
        Instance = this;
        source = GetComponent<AudioSource>();
        //backgroundMusic = GetComponent<AudioClip[]>();
        //kaboom = GetComponent<AudioClip[]>();
    }

    private void Start()
    {
        source.clip = backgroundMusic[SceneManager.GetActiveScene().buildIndex-1];
        source.volume = source.volume * 0.2f;
        source.PlayOneShot(source.clip);
    }

    public void EnemyTakingDamage()
    {
        int i = Random.Range(0, enemyTakingDamageSounds.Length);
        source.clip = enemyTakingDamageSounds[i];
        source.volume = source.volume * 1.2f;
        source.PlayOneShot(source.clip);
    }

    public void EnemyNotTakingDamage()
    {
        int i = Random.Range(0, enemyNotTakingDamageSounds.Length);
        source.clip = enemyNotTakingDamageSounds[i];
        source.PlayOneShot(source.clip);
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
