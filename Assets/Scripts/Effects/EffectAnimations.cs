using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class EffectAnimations : MonoBehaviour
{
    public static EffectAnimations instance { get; private set; }
    [SerializeField] GameObject balloonPop;
    [SerializeField] GameObject enemyPoof;
    [SerializeField] GameObject confettiExplosion;
    [SerializeField] GameObject pieExplosionGround;
    [SerializeField] GameObject pieExplosion;

    [SerializeField] GameObject waterSplash;
    [SerializeField] GameObject confettiBurst;
    [SerializeField] GameObject balloonFireExplosion;
    [SerializeField] GameObject bigExplosion;

    [SerializeField] Transform popPosition;
    [SerializeField] Image popcornPopping;
    [SerializeField] Image ultReadyPopcornPopcorn;

    private Image particleHolder;
    private HudPopcornFill popcornHudObject;

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

        popcornHudObject = FindObjectOfType<HudPopcornFill>();
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

    public void BalloonPop(Vector2 pos, Vector3 scale)
    {
        var pop = Instantiate(balloonPop, pos, Quaternion.identity);
        pop.transform.localScale = scale;
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
        var popping = Instantiate(popcornPopping, pos, Quaternion.identity);    //, FindObjectOfType<HudPopcornFill>().transform
        //popping.AddComponent(typeof(RectTransform));
        //popping.rectTransform.localScale += new Vector3(100,100);
        //popping.transform.position = new Vector3(20, FindObjectOfType<HudPopcornFill>().popcornFillTopPos.y);
        popping.rectTransform.anchoredPosition = popcornHudObject.popcornFillTopPos;
        popping.transform.position = popPosition.position;
        Destroy(popping, 1);
    }

    public void PopcornPoppingUltReady(Vector2 pos)
    {
        if (particleHolder != null)
        {
            Destroy(particleHolder);
        }
        particleHolder = Instantiate(ultReadyPopcornPopcorn, pos, Quaternion.identity, FindObjectOfType<HudPopcornFill>().transform);
        particleHolder.rectTransform.anchoredPosition = popcornHudObject.popcornFillTopPos;
        //particleHolder.scale
        Destroy(particleHolder, 40);
    }

    public void StunWave(Vector2 pos, Quaternion rot)
    {
        //var soundWaveObj = Instantiate(soundWave, FindObjectOfType<WeaponStunGun>().transform.position, rot);
        //soundWaveObj.transform.localScale = new Vector3(0.2f, 0.2f);
    }

    public void StunWaveUpgraded(Vector2 pos)
    {
        //var soundWaveObj = Instantiate(soundWaveUpgraded, pos, Quaternion.identity);
    }

    public void WaterSplash(Vector2 pos, Vector3 scale)
    {
        var obj = Instantiate(waterSplash, pos, Quaternion.identity);
        obj.transform.localScale = scale;
        Destroy(obj, 1);
    }

    public void ConfettiBurst(Vector2 pos, Vector3 scale)
    {
        var obj = Instantiate(confettiBurst, pos, Quaternion.identity);
        obj.transform.localScale = scale;
        Destroy(obj, 1);
    }

    public void BalloonFireExplosion(Vector2 pos, Vector3 scale)
    {
        var obj = Instantiate(balloonFireExplosion, pos, Quaternion.identity);
        obj.transform.localScale = scale;
        Destroy(obj, 1);
    }

    public void BigExplosion(Vector2 pos, Vector3 scale)
    {
        var obj = Instantiate(bigExplosion, pos, Quaternion.identity);
        obj.transform.localScale = scale;
        Destroy(obj, 1);
    }
}
    