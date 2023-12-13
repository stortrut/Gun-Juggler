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
        if (currentTime > lastSpawn &&  firstTime)
        {
            firstTime = false;
            lastSpawn = currentTime + Random.Range(2, 4);
            for (float i = -((float)maxNumberOfGiraffes/2); i < maxNumberOfGiraffes; i += 2)
            {
                Instantiate(balloonSpawn , transform.position + new Vector3(i,0,0), Quaternion.identity);
            }
            
        }
    }
}
