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
    [SerializeField] public AudioClip[] hittingSounds;

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
        source.PlayOneShot(source.clip);
    }

    private void Update()
    {

    }

    //public void Kaboom()
    //{
    //    //randomize a soundclip from the kaboom list and play without interruption when called
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
