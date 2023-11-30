using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : WeaponBase
{
    [SerializeField] protected float fireRateTimer;
    [SerializeField] protected GameObject bulletSmall;
    [SerializeField] protected Vector2 spawnBulletPos;
    public float rotationAngle;
    //public float radius = 10f; //how far from gunpoint the bullets spawn
    
    public void Shoot()
    {
        GameObject weaponBullet = Instantiate(bullet, gunPoint.position, gunPoint.rotation);
        //Sound.Instance.EnemyNotTakingDamage();
        weaponBullet.GetComponent<Bullet>().SetColor(GetComponent<SpriteRenderer>().color);
        //weaponBullet.GetComponent<Rigidbody2D>().velocity = weaponBullet.transform.right * bulletSpeed *Time.deltaTi;
        Destroy(weaponBullet,3);
    }
    public void ShootWideSpread()
    {
        for (int i = 0; i < 5; i++)
        {
            Debug.Log("gunPoint" + gunPoint.transform.localPosition);
            rotationAngle = Random.Range(-45, 45);
            //spawnBulletPos.x = gunPoint.position.x + radius * Mathf.Sin(rotationAngle * Mathf.Deg2Rad);
            //spawnBulletPos.y = gunPoint.position.y + radius * Mathf.Cos(rotationAngle * Mathf.Deg2Rad);
            Quaternion offsetRotation = Quaternion.Euler(0, 0, rotationAngle);
            GameObject weaponBullet = Instantiate(bulletSmall, gunPoint.position, offsetRotation);
            weaponBullet.GetComponent<Bullet>().SetColor(GetComponent<SpriteRenderer>().color);
            Destroy(weaponBullet, 3);
        }
    }
}
//weapon scriptet kallar på spelaren, ref rigidbody, funktion direktion, power, force - trygetcomponent
//gamemanager, singleton?

//if (weaponBullet.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb2d))
//{
//    rb2d.velocity = bulletVelocity;
//    Debug.Log("Bullet spawned with rotation: " + rotationAngle);
//}
