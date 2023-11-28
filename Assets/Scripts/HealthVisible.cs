using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HealthVisible : MonoBehaviour
{
    [SerializeField] private Sprite Shield;
    [SerializeField] private Sprite Full;
    [SerializeField] private Sprite Half;
    [SerializeField] private Sprite Empty;
    public SpriteRenderer Sprite;
    Health health;
    float currentHealth;
    int i;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponentInParent<Health>();
        Sprite.sprite = Shield;
        Sprite.color = Color.blue;
        currentHealth = health.health;
    }

    // Update is called once per frame
    void Update()
    {
        if (health.health == 2 && Sprite.sprite != Full && health.hasProtection==false)
        {
            Sprite.color = Color.white;
            Sprite.sprite = Full;   
        }
        if (health.health == 1 && Sprite.sprite != Half)
        {
            Sprite.sprite = Half;
        }
        else if (health.health == 0 && Sprite.sprite != Empty)
        {
            Sprite.sprite = Empty;
        }
    }


    //    if (health.health < currentHealth)
    //    {
    //        currentHealth = health.health;
    //        i =(i+1) % 3;
    //    switch (i)
    //    {
    //        case 1:
    //                  Sprite.sprite = Half;
    //            break;

    //        case 2:
    //                Sprite.sprite = Empty;
    //                break; 
    //        case 0:
    //            break;
    //    }



    //    }

}
