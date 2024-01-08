using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class HoolaHoop : MonoBehaviour
{
    [SerializeField] Animator enemyAnimator;
    [SerializeField] WaveManager waveManager;
    [SerializeField] CurtainMovement curtainMovement;
    bool on = false;
    const string WAVE = "wave";
    //bool fightHasStarted;


    [SerializeField] GameObject wavesClearedSign;



    private void Start()
    {
        if (wavesClearedSign != null)
            wavesClearedSign.SetActive(false);
    }

    public void StartWave()
    {
        enemyAnimator.SetBool(WAVE, true);
        Invoke(nameof(SoundHoopFire), .2f);
    }
    private void SoundHoopFire()
    {
        Sound.Instance.SoundSet(Sound.Instance.hoopFire, 0);
    }

    public void EndWave()
    {
        Sound.Instance.ChangeBackgroundMusic(false);
        Lights.Instance.FightLightOn(false);

        enemyAnimator.SetBool(WAVE, false);

        if (wavesClearedSign != null)
            wavesClearedSign.SetActive(true);

        StartCoroutine(nameof(EndWaveCorotine));
    }


    IEnumerator EndWaveCorotine()
    {
        yield return new WaitForSeconds(3);


        // FollowPlayer.Instance.FindPlayer();
        StartCoroutine(FollowPlayer.Instance.SmoothCamera(50, new Vector3(5, 5.75f, 31.7999992f), true));
        PlayerJuggle.Instance.DropAllWeaponsOnGround();
        PlayerJuggle.Instance.pauseJuggling = true;
        PlayerJuggle.Instance.FightEnd();
        //FollowPlayer.Instance.lockOn = true;
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && on == false)
        {
            curtainMovement.ActivateCurtains();
            Sound.Instance.SoundSet(Sound.Instance.murmuring, 0, 1f);
            Sound.Instance.SoundSet(Sound.Instance.windWhoosh, 0, 1f);
            Debug.Log("fight started");

            Lights.Instance.FightLightOn(true);
            Sound.Instance.ChangeBackgroundMusic(true);
            //StartCoroutine(FollowPlayer.Instance.SmoothCamera(400, new Vector3(29.7999992f, -7.50287676f, 12.6000004f))); 
            StartCoroutine(FollowPlayer.Instance.SmoothCamera(100, transform.position + new Vector3(17f, 0 ,5), false));
            // new Vector3(9.27000046f, 7.05999994f, 16.2999992f)
            on = true;
            Invoke(nameof(StartWave), 5);
            StartCoroutine(DelayStart());
            PlayerJuggle.Instance.FightStart();
            // Respawn.Instance.waveStart.Invoke();
        }
    }
    private IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(5);
        AudienceSatisfaction.Instance.ActStarted();
        waveManager.StartWave();
        Sound.Instance.SoundSet(Sound.Instance.murmuring, 0, 1f);
    }
}
