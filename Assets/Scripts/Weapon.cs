using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : WeaponBase
{
    public void Shoot()
    {
        GameObject weaponBullet = Instantiate(bullet, gunPoint.position, gunPoint.rotation);
        Vector2 direction = 
        weaponBullet.GetComponent<Rigidbody2D>().velocity = weaponBullet.transform.right * bulletSpeed;
    }

    public void Knockback()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var rb = player.GetComponent<Rigidbody2D>();
        //rb.AddForce()
    }

}
//weapon scriptet kallar på spelaren, ref rigidbody, funktion direktion, power, force - trygetcomponent
//gamemanager, singleton?
