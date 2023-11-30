using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAnimations : MonoBehaviour
{
    public static EffectAnimations Instance { get; private set; }
    [SerializeField] GameObject balloonPop;
    private void Awake()
    {
        Instance = this; 
    }

    public void BalloonPop(Vector2 pos)
    {
        Instantiate(balloonPop, pos, Quaternion.identity);
    }

}
