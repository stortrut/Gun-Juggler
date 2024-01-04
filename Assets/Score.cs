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
    private float score = 0;
    private float totalScore;
    public TextMeshProUGUI elephantHitText;
    public TextMeshProUGUI giraffeHitText;
    public TextMeshProUGUI monkeyHitText;
    public TextMeshProUGUI pieClownHitText;
    public TextMeshProUGUI bulletsShotText;
    public TextMeshProUGUI bulletsHitText;

    public GameObject scoreCanvas;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }
    public void WhatEnemyHit(EnemyType enemy)
    {
        if (enemy == EnemyType.Elephant)
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
    public void ActScore(float addScore)
    {
        score += addScore;
        totalScore++;

    }
    public int EndScore()
    {
        var finalScore = score / totalScore * 100;
        return (int)finalScore;


    }
    public void DisplayScore()
    {
        scoreCanvas.SetActive(true);
        elephantHitText.text = "";
        giraffeHitText.text = "";
        monkeyHitText.text = "";
        pieClownHitText.text = "";
        bulletsShotText.text = "";
        bulletsHitText.text = "";
        //
        elephantHitText.text = "Babar hit: "+elephantHit;
        giraffeHitText.text = "Savanna hit: "+giraffeHit;
        monkeyHitText.text = "Boris hit: "+monkeyHit;
        pieClownHitText.text = "PieBob hit: "+pieClownHit;
        bulletsShotText.text = "Bullets shot: "+bulletsShot;
        bulletsHitText.text="Bullets hit: "+bulletsHit;

        scoreText.text = "Performance Rating: " + EndScore() + "%";

    }
    public void DontDisplayscore()
    {
        scoreCanvas.SetActive(false);
    }
}
