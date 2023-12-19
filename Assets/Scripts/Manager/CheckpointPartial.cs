using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointPartial : MonoBehaviour
{
    [SerializeField] private SpawnManagerScriptableObject checkpointData;

    private void Start()
    {
        var weaponsCurrentlyinJuggleLoop = checkpointData.weapons;
    }
}
