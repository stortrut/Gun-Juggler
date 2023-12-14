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
    void Start()
    {
        player = Manager.Instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x - player.transform.position.x < 18)
        {
        }
        float currentTime = Time.time;
        if (currentTime > lastSpawn)
        {
            firstTime = false;
            lastSpawn = currentTime + Random.Range(2, 3);
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
