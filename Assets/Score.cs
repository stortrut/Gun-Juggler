using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score Instance { get; private set; }
    public TextMeshProUGUI scoreText;
    [ReadOnly] public int elephantHit;
    [ReadOnly] public int giraffeHit;
    [ReadOnly] public int monkeyHit;
    [ReadOnly] public int pieClownHit;
    [ReadOnly] public int bulletsShot;
    [ReadOnly] public int bulletsHit; 
    
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(gameObject);
    }
    public void WhatEnemyHit(EnemyType enemy)
    {
        if(enemy == EnemyType.Elephant)
        {
            elephantHit++;
        }
        if (enemy == EnemyType.Giraffe)
        {
            giraffeHit++;
        }
        if (enemy == EnemyType.Monkey)
        {
            monkeyHit++;
        }
        if (enemy == EnemyType.PieClown)
        {
            pieClownHit++;
        }
    }
}
