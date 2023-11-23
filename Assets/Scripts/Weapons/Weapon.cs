using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : WeaponBase
{
    public void Shoot()
    {
        Debug.Log("Bullet created");
        GameObject weaponBullet = Instantiate(bullet, gunPoint.position, gunPoint.rotation);
        weaponBullet.GetComponent<Bullet>().SetColor(GetComponent<SpriteRenderer>().color);
        //Vector2 direction = 
        weaponBullet.GetComponent<Rigidbody2D>().velocity = weaponBullet.transform.right * bulletSpeed;
        Destroy(weaponBullet,3);
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
