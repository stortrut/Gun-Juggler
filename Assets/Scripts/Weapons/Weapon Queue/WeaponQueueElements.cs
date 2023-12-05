using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponQueueElements : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject confettiGun;
    [SerializeField] GameObject smallGun;
    [SerializeField] GameObject stunGun;

    PlayerJuggle playerJuggleScript;
    private float queueLength = 40;

    void Start()
    {
        playerJuggleScript = FindObjectOfType<PlayerJuggle>();
    }

    public void InstantiateAppropriateQueueElements()
    {
        Debug.Log("loopvapen" + playerJuggleScript.weaponsCurrentlyInJuggleLoop);
    }


    void Update()
    {
        
    }
}
