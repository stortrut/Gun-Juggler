using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
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
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.CompareTag("Player") && on == false)
        {
            waveManager.StartWave();
            FollowPlayer.Instance.offset.z = -75;
            on = true;
           // Respawn.Instance.waveStart.Invoke();
        }
    }
}
