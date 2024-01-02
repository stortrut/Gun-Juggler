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
    [ReadOnly] public bool air;

    private void Start ()
    {
        Instance = this;
    }
    public void Spot(int spot)
    {
        StartCoroutine(SpotNumber(spot));
    }
    public IEnumerator SpotNumber(int spot)
    {
        yield return new WaitForSeconds(0.4f);
        Respawn.Instance.spotNumber = spot;
    }
}

   
