using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    float posGapBetweenQueueObjects = 2.5f;
    int i = 0;
    [SerializeField] Vector3 startPos = new Vector3(0, 0, 0);
    PlayerJuggle playerJuggleScript;
    void Start()
    {
        playerJuggleScript = FindObjectOfType<PlayerJuggle>();
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Positioning()
    {
        Debug.Log(i);
        transform.position = new Vector3(startPos.x+ posGapBetweenQueueObjects * i, 0, 0);
        i++;
        if (i >= playerJuggleScript.weaponsCurrentlyInJuggleLoop.Count)
        {
            Debug.Log("nollställ");
            i = 0;
        }
    }
}
