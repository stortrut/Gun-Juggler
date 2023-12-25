using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveManager: MonoBehaviour
{


    //[Header("Airborne Enemies (Type)")]
    //[SerializeField] EnemyType enemyAir1;
    //[SerializeField] EnemyType enemyAir2;
    //[SerializeField] EnemyType enemyAir3;

    //[Header("Grounded Enemies (Type)")]
    //[SerializeField] EnemyType enemyGround1;
    //[SerializeField] EnemyType enemyGround2;
    //[SerializeField] EnemyType enemyGround3;

    //[Header("Airborne Enemy Spawnpoints")]
    //[SerializeField] Transform spawnSpotAir1;
    //[SerializeField] Transform spawnSpotAir2;
    //[SerializeField] Transform spawnSpotAir3;

    //[Header("Grounded Enemy Spawnpoints")]
    //[SerializeField] Transform spawnSpotGround1;
    //[SerializeField] Transform spawnSpotGround2;
    //[SerializeField] Transform spawnSpotGround3;

    // Different Prefabs
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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            Spawn();
        }
    }
    private void MoveSpawnPoints()
    {
       
    }
    private void Spawn()
    {
        SpawnEnemies(presetAirWave, airSpawnSpots);
        SpawnEnemies(presetGroundWave, groundSpawnSpots);   
    }


    private void SpawnEnemies(List<EnemyType> enemies, List<Transform> spawn)
    {
        var i = 0;
        foreach (var enemy in enemies)
        { 
          currentEnemy = enemy;
        var index = (int)currentEnemy;


        var spawnedEnemy = Instantiate(availableEnemies.ElementAt(index),spawn.ElementAt(i));
        i++;
        }
    }

}


