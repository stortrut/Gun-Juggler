
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class SpawnManagerScriptableObject : ScriptableObject

{ 
    [Header("Checkpoint values")]
    public List<WeaponJuggleMovement> weapons;
    private Vector3 spawnPosition;
}
