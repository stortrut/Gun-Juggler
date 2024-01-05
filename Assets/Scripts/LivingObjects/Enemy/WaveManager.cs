using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{


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
    [SerializeField] private List<GameObject> curtain;
    [SerializeField] private int numberOfWaves;
    [SerializeField] private bool mixedWave;
    [SerializeField] private List<Sprite> numbers;
    [SerializeField] private SpriteRenderer firstNumber;
    [SerializeField] private SpriteRenderer secondNumber;
    [SerializeField] private SpriteRenderer slash;
    [SerializeField] private SpriteRenderer waveImage;
    private bool even;
    private int spawnedEnemies;
    private int waveNumber;
    private void EnemyDied()
    {
        spawnedEnemies--;
        if(spawnedEnemies <= 0)
        {
            spawnedEnemies = 0;
            if(waveNumber < numberOfWaves)
                StartWave();
            else
            {
                hoolaHoop.EndWave();
                clownAnimator.Attacking();
                EffectAnimations.Instance.BigExplosion(clownAnimator.gameObject.transform.position, Vector3.one);
                Sound.Instance.SoundSet(Sound.Instance.explosion, 0, .7f);
                if (AudienceSatisfaction.Instance.audienceSatisfaction.fillAmount < 0.15f)
                {
                    PlayerHealth.Instance.KillPlayer();
                    PlayerHealth.Instance.isDead = true; 
                }
                else
                {
                    Score.Instance.ActScore(AudienceSatisfaction.Instance.audienceSatisfaction.fillAmount);
                    AudienceSatisfaction.Instance.audienceSatisfaction.fillAmount = 0.3f;
                }
                  
                Destroy(clownAnimator.gameObject);
                foreach (var bla in curtain)
                {
                    bla.SetActive(false);
                }
            }

        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Cube();
        }
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    StartWave();    
        //}
    }
    public void StartWave()
    {
        

        firstNumber.sprite = numbers[waveNumber+1];
        secondNumber.sprite = numbers[numberOfWaves];
        slash.enabled = true;
        waveImage.enabled = true;
        waveNumber++;
        Spawn();
        if (mixedWave == false)
        {
            if (!even)
            {
                StartCoroutine(PoofAir(airSpawnSpots));
            }
            else
            {
                StartCoroutine(PoofAir(groundSpawnSpots));
            }

        }

        else
        {
            StartCoroutine(PoofAir(airSpawnSpots));
            StartCoroutine(PoofAir(groundSpawnSpots));
        }

    }
    public IEnumerator PoofAir(List<Transform> spawnSpots)
    {
        yield return new WaitForSeconds(1);
        cannonAnimator.CannonAttack();
        Sound.Instance.SoundSet(Sound.Instance.explosion, 0, .3f);
        yield return new WaitForSeconds(0.5f);
        var i = 0;
        foreach (var enemy in spawnSpots)
        {
            var a = spawnSpots.ElementAt(i);
            var pos = new Vector2(a.position.x, a.position.y);
            EffectAnimations.Instance.EnemyPoof(pos);
            i++;
            Sound.Instance.SoundSet(Sound.Instance.poof, 0, .2f);
        }
    }
    public IEnumerator DelayedSpawn()
    {

        clownAnimator.CannonAttack();
        yield return new WaitForSeconds(2);
        if (mixedWave == false)
        {
            even = !even;
            if (even)
            {
                SpawnEnemies(presetAirWave, airSpawnSpots);
            }
            else
            {
                SpawnEnemies(presetGroundWave, groundSpawnSpots);
            }
        }
        else
        {
            SpawnEnemies(presetAirWave, airSpawnSpots);
            SpawnEnemies(presetGroundWave, groundSpawnSpots);
        }
        yield return new WaitForSeconds(15);
        //Spawn();

    }
    private void Spawn()
    {
        StartCoroutine(DelayedSpawn());
        foreach (var bla in curtain)
        {
            bla.GetComponent<EdgeCollider2D>().enabled = true;
        }

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
                i = 0; break;
            }
            
            var spawnedEnemy = Instantiate(availableEnemies.ElementAt(index), spawn.ElementAt(i));
            spawnedEnemies++;
            if (spawnedEnemy.TryGetComponent<Health>(out Health health))
                health.died += EnemyDied;
            else
                spawnedEnemy.GetComponentInChildren<Health>().died += EnemyDied;
            i++;
            waveImage.enabled = false;
            slash.enabled = false;
            firstNumber.sprite = null;
            secondNumber.sprite = null;
        }
    }

    private void Cube()
    {
        var shapeSize = 0;

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
                    else if (even == false)
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
                    else if (even == false)
                    {
                        yValue += numberOnLine;
                    }
                    even ^= true;
                }


            }
            Debug.Log("X " + xValue + "Y" + yValue + "numberBAllon " + i);

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

