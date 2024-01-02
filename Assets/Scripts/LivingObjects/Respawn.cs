using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public static Respawn Instance;
    public UnityEvent respawnAir;
    public UnityEvent respawnGround;
    public UnityEvent waveStart;
    public UnityEvent <EnemyType> boo;
    [ReadOnly] public int spotNumber;

    private void Start ()
    {
        Instance = this;
    }
}

   
