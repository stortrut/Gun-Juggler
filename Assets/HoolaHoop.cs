using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class HoolaHoop : MonoBehaviour
{
    [SerializeField] Animator enemyAnimator;
    [SerializeField] WaveManager waveManager;
    bool on = false;
    const string WAVE = "wave";


    public void StartWave()
    {
        enemyAnimator.SetBool(WAVE, true);
    }
    public void EndWave()
    {
        enemyAnimator.SetBool(WAVE, false);
        Sound.instance.ChangeBackgroundMusic(false);
        FollowPlayer.Instance.FindPlayer();
        StartCoroutine(FollowPlayer.Instance.SmoothCamera(400, new Vector3(5, 5.75f, 31.7999992f), true));
        PlayerJuggle.Instance.DropAllWeaponsOnGround();
        PlayerJuggle.Instance.FightEnd();
        //FollowPlayer.Instance.lockOn = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.CompareTag("Player") && on == false)
        {
            //StartCoroutine(FollowPlayer.Instance.SmoothCamera(400, new Vector3(29.7999992f, -7.50287676f, 12.6000004f))); 
            StartCoroutine(FollowPlayer.Instance.SmoothCamera(400, transform.position + new Vector3(17f, 0 ,5), false));
            // new Vector3(9.27000046f, 7.05999994f, 16.2999992f)
            on = true;
            Invoke(nameof(StartWave), 2);
            StartCoroutine(DelayStart());
            Sound.instance.ChangeBackgroundMusic(true);
            PlayerJuggle.Instance.FightStart();
           // Respawn.Instance.waveStart.Invoke();
        }
    }
    private IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(2);
        waveManager.StartWave();
    }
}
