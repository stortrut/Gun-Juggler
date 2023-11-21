using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public float Damage;
    public float Speed;
    public float FireRate;
    public GameObject Bullet;
    public List<GameObject>[] weaponsInJuggleLoop;
    public bool switchWeapon { get; set; }

    void Update()
    {
        
    }
    public void Switch()
    {

    }

}

