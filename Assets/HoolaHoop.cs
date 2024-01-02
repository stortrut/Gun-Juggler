using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class HoolaHoop : MonoBehaviour
{
    [SerializeField] Animator enemyAnimator;

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
        if(other.gameObject.CompareTag("Player"))
        {
            Respawn.Instance.waveStart.Invoke();
        }
    }
}
