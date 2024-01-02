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
    [SerializeField] GameObject popcornPopping;
    [SerializeField] GameObject ultReadyPopcornPopcorn;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
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
        var pop = Instantiate(balloonPop, pos, Quaternion.identity);
        Destroy(pop, 1);
    }

    public void EnemyPoof(Vector2 pos)
    {
        GameObject pof = Instantiate(enemyPoof, pos, Quaternion.identity);
        Destroy(pof, .3f);
    }

    public void ConfettiExplosion(Vector2 pos)
    {
        var explosion = Instantiate(confettiExplosion, pos, Quaternion.identity);
        Destroy(explosion,1);
    }

    public void PieExplosionGround(Vector2 pos)
    {
       var pieExplode = Instantiate(pieExplosionGround, pos, Quaternion.identity);
        Destroy(pieExplode, 1);
    }

    public void PieExplosion(Vector2 pos)
    {
        var pieExplode = Instantiate(pieExplosion, pos, Quaternion.identity);
        Destroy(pieExplode, 1);
    }

    public void PopcornPopping(Vector2 pos)
    {
        var popping = Instantiate(popcornPopping, pos, Quaternion.identity, FindObjectOfType<HudPopcornFill>().transform);
        //popping.AddComponent(typeof(RectTransform));
        //popping.transform.localScale += new Vector3(8, 8);
        //popping.transform.position = new Vector3(20, FindObjectOfType<HudPopcornFill>().popcornFillTopPos.y);
        Destroy(popping, 1);
    }

    public void PopcornPoppingUltReady(Vector2 pos)
    {
        var ultready = Instantiate(ultReadyPopcornPopcorn, pos, Quaternion.identity, FindObjectOfType<HudPopcornFill>().transform);
        Destroy(ultready, 1);
    }
}
    