using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Splines.SplineInstantiate;

public class DummyManager : MonoBehaviour
{
    private GameObject Dummy;
    private GameObject Monkey;
    private GameObject Giraffe;
    private GameObject Elephant;
    private GameObject PieClown;
    private GameObject CannonClown;

    // List containing all the enemies for the current wave
    [SerializeField] private List<EnemyType> presetAirWave;
    [SerializeField] private List<EnemyType> presetGroundWave;
    [SerializeField] private List<Transform> airSpawnSpots;
    [SerializeField] private List<Transform> groundSpawnSpots;
    [SerializeField] private List<GameObject> availableEnemies;
    private EnemyType currentEnemy;
    
   
    private void Start()
    {
       SpawnAir();
       SpawnGround();
    }
    public void SpawnAir()
    {
        StartCoroutine(DelayedSpawn(presetAirWave, airSpawnSpots));
    }
    public void SpawnGround()
    {
        
        StartCoroutine(DelayedSpawn(presetGroundWave, groundSpawnSpots));
    }
        private void SpawnEnemies(List<EnemyType> enemies, List<Transform> spawn)
    {
        var i = 0;
        foreach (var enemy in enemies)
        {
            currentEnemy = enemy;
            var index = (int)currentEnemy;


            var spawnedEnemy = Instantiate(availableEnemies.ElementAt(index), spawn.ElementAt(i));
            spawnedEnemy.GetComponent<DummyObjects>().spot = i;
            i++;
        }
    }
    private IEnumerator DelayedSpawn(List<EnemyType> enemies, List<Transform> spawn)
    {
        yield return new WaitForSeconds(0.5f);
        SpawnEnemies(enemies, spawn);
    }
    public void SpawnSpecificAir()
    {
        
    }
    public void SpawnSpecificGround(EnemyType enemy)
    {
        StartCoroutine(DelayedSpecific(enemy));
    }
    private IEnumerator DelayedSpecific(EnemyType enemy)
    {
        
        yield return new WaitForSeconds(0.5f);
        //if(air == true)
        {
            var index = (int)enemy;
            var spawnedEnemy = Instantiate(availableEnemies.ElementAt(index), airSpawnSpots.ElementAt(Respawn.Instance.spotNumber));
        }
            

    }
}


