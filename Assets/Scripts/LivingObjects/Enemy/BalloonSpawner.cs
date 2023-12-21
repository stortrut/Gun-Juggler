using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    [SerializeField] private GameObject balloonSpawn;


    private GameObject player;
    private float lastSpawn;
    private bool firstTime = true;
    public int maxNumberOfGiraffes;
    private int numberOfGiraffes;

    [SerializeField] bool spawnAllAtOnce;

    void Start()
    {
        player = FindObjectOfType<PlayerJuggle>()?.gameObject;

        if (spawnAllAtOnce)
        {
            for (int i = 0; i < maxNumberOfGiraffes; i++)
            {
                Instantiate(balloonSpawn, transform.position + new Vector3((i * 2f) - 2, Random.Range(-1, 2f), 0), Quaternion.identity, transform);
            }
        }
    }

    void Update()
    {
        if (spawnAllAtOnce) { return; }

        if (transform.position.x - player.transform.position.x < 25)
        {
        }
        float currentTime = Time.time;
        if (currentTime > lastSpawn)
        {
            firstTime = false;
            lastSpawn = currentTime + Random.Range(1.5f, 2.5f);
           // for (float i = -((float)maxNumberOfGiraffes/2); i < maxNumberOfGiraffes; i += 2)
          //  {
            if(numberOfGiraffes < maxNumberOfGiraffes)
            {
                Instantiate(balloonSpawn, transform.position + new Vector3(numberOfGiraffes - 2, 0, 0), Quaternion.identity,transform);
                numberOfGiraffes++;
            }
                
            //}
            
        }
    }
}
