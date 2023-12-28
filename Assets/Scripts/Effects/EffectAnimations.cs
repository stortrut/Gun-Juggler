using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAnimations : MonoBehaviour
{
    public static EffectAnimations instance { get; private set; }
    [SerializeField] GameObject balloonPop;
    [SerializeField] GameObject enemyPoof;
    [SerializeField] GameObject confettiExplosion;
    [SerializeField] GameObject pieExplosionGround;
    [SerializeField] GameObject pieExplosion;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            BalloonPop(transform.position);
        }
    }

    public void BalloonPop(Vector2 pos)
    {
        Instantiate(balloonPop, pos, Quaternion.identity);
        Destroy(balloonPop, 1);
    }

    public void EnemyPoof(Vector2 pos)
    {
        GameObject pof = Instantiate(enemyPoof, pos, Quaternion.identity);
        Destroy(pof, .3f);
    }

    public void ConfettiExplosion(Vector2 pos)
    {
        Instantiate(confettiExplosion, pos, Quaternion.identity);
        Destroy(confettiExplosion,1);
    }

    public void PieExplosionGround(Vector2 pos)
    {
        Instantiate(pieExplosionGround, pos, Quaternion.identity);
        Destroy(pieExplosionGround, 1);
    }
    public void PieExplosion(Vector2 pos)
    {
        Instantiate(pieExplosion, pos, Quaternion.identity);
        Destroy(pieExplosion, 1);
    }
}
