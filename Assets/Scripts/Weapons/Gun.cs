using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : WeaponBase
{
    [SerializeField] protected float fireRateTimer;
    public void Shoot()
    {
        GameObject weaponBullet = Instantiate(bullet, gunPoint.position, gunPoint.rotation);
        //Sound.Instance.EnemyNotTakingDamage();
        weaponBullet.GetComponent<Bullet>().SetColor(GetComponent<SpriteRenderer>().color);
        //Vector2 direction = 
        //weaponBullet.GetComponent<Rigidbody2D>().velocity = weaponBullet.transform.right * bulletSpeed *Time.deltaTi;
        Destroy(weaponBullet,3);
    }
}
//weapon scriptet kallar på spelaren, ref rigidbody, funktion direktion, power, force - trygetcomponent
//gamemanager, singleton?
