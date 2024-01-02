using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveManager : MonoBehaviour
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

    [SerializeField] private EnemyAnimator clownAnimator;
    [SerializeField] private EnemyAnimator cannonAnimator;
    [SerializeField] private HoolaHoop hoolaHoop;
    // List containing all the enemies for the current wave
    [SerializeField] private List<EnemyType> presetAirWave;
    [SerializeField] private List<EnemyType> presetGroundWave;
    [SerializeField] private List<Transform> airSpawnSpots;
    [SerializeField] private List<Transform> groundSpawnSpots;
    [SerializeField] private List<GameObject> availableEnemies;
    private EnemyType currentEnemy;
    [ReadOnly][SerializeField] private List<Vector3> setShape;
    [SerializeField] private int shapeSize;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Cube();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Spawn();
            StartCoroutine(PoofAir());
        }
    }
    public void StartWave()
    {
        Spawn();
        StartCoroutine(PoofAir());
    }
    public IEnumerator PoofAir()
    {
        yield return new WaitForSeconds(1);
        cannonAnimator.CannonAttack();
        yield return new WaitForSeconds(0.5f);
        var i = 0;
        foreach (var enemy in airSpawnSpots)
        {
            var a = airSpawnSpots.ElementAt(i);
            var pos = new Vector2(a.position.x, a.position.y);
            EffectAnimations.instance.EnemyPoof(pos);
            i++;
        }
    }
    public IEnumerator DelayedSpawn()
    {

        clownAnimator.CannonAttack();
        yield return new WaitForSeconds(2);

        SpawnEnemies(presetAirWave, airSpawnSpots);
        SpawnEnemies(presetGroundWave, groundSpawnSpots);
    }
    private void Spawn()
    {
        hoolaHoop.StartWave();
        StartCoroutine(DelayedSpawn());

    }


    private void SpawnEnemies(List<EnemyType> enemies, List<Transform> spawn)
    {
        var i = 0;
        foreach (var enemy in enemies)
        {
            currentEnemy = enemy;
            var index = (int)currentEnemy;
            if (i >= spawn.Count)
            {
                i = 0;
            }

            var spawnedEnemy = Instantiate(availableEnemies.ElementAt(index), spawn.ElementAt(i));
            i++;
        }
    }

    private void Cube()
    {
       

        var basePos = airSpawnSpots[0].position;
        setShape.Clear();
        for (var i = 1; i < presetAirWave.Count + 1; i++)
        {
            float xValue = 0;
            float yValue = 0;
            float numberOnLine = ((float)i / presetAirWave.Count) * (shapeSize * 4);
            Debug.Log("numberONLINE " + numberOnLine + "i " + i);
            bool even = true;
            float f = 5;
            for (var j = 0; j < 3; j++)
            {
                numberOnLine = Mathf.Abs(numberOnLine);
                f -= 2; 
                 if (j * shapeSize >= numberOnLine)
                {
                    numberOnLine *= f / (Mathf.Abs(f));
                    if (even == true)
                    {
                        xValue += numberOnLine;
                    }   
                    else if(even == false)
                    {
                        yValue += numberOnLine;
                    }
                    even ^= true;
                }
                else if (j * shapeSize < numberOnLine)
                {
                    Debug.Log("IncomingNUMBERONLINE " + numberOnLine + "numberBAllon " + i + "J" + j);
                    numberOnLine -= shapeSize;
                    numberOnLine *= f / (Mathf.Abs(f));
                    if (even == true)
                    {
                        Debug.Log("IncomingX " + xValue + "numberBAllon " + i + "J" + j);
                        xValue += numberOnLine;
                        Debug.Log("EarlyX " + xValue + "numberBAllon " + i);
                        Debug.Log(f + "after X value");
                    }
                    else if(even == false) 
                    { 
                        yValue += numberOnLine;
                    }
                    even ^= true;
                }   
              
              
            }
            Debug.Log("X " + xValue + "Y"+ yValue + "numberBAllon " + i);
            
            // 4 ballonger 5 , 10 , 15 ,20
            // 20
            setShape.Add(new Vector3((basePos.x - (shapeSize / 2)) + xValue, (basePos.y - (shapeSize / 2)) + yValue, 0));
        }
        AlternativeSpawn(presetAirWave);
    }
    private void Circle()
    {

    }
    private void Diamond()
    {

    }
    private void Geese()
    {

    }
    private void AlternativeSpawn(List<EnemyType> enemies)
    {
        var i = 0;
        foreach (var enemy in enemies)
        {
            currentEnemy = enemy;
            var index = (int)currentEnemy;
            //if (i >= setShape.Count)
            //{
            //    i = 0;
            //}

            var spawnedEnemy = Instantiate(availableEnemies.ElementAt(index), setShape.ElementAt(i), Quaternion.identity);
            i++;
        }
    }
}
public enum WaveShape
{
    Circle,
    Diamond,
    Geese,
    Cube,

}

