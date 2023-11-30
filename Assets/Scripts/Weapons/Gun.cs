using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : WeaponBase
{
    [SerializeField] protected float fireRateTimer;
    [SerializeField] protected GameObject bulletSmall;
    [SerializeField] protected Vector2 spawnBulletPos;
    [SerializeField] protected float bulletSpread = 40;
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
        rotationAngle = Random.Range(-45, 45);
        //spawnBulletPos.x = gunPoint.position.x + radius * Mathf.Sin(rotationAngle * Mathf.Deg2Rad);
        //spawnBulletPos.y = gunPoint.position.y + radius * Mathf.Cos(rotationAngle * Mathf.Deg2Rad);
        Quaternion offsetRotation = Quaternion.Euler(0, 0, rotationAngle);
        GameObject weaponBullet = Instantiate(bulletSmall, gunPoint.position, offsetRotation);
        weaponBullet.GetComponent<Bullet>().SetColor(GetComponent<SpriteRenderer>().color);
        Destroy(weaponBullet, 3);

        Quaternion offsetRotationMirrored = Quaternion.Euler(0, 0, -rotationAngle);
        GameObject weaponBullet2 = Instantiate(bulletSmall, gunPoint.position, offsetRotationMirrored);
        weaponBullet2.GetComponent<Bullet>().SetColor(GetComponent<SpriteRenderer>().color);
        Destroy(weaponBullet2, 3);

        GameObject weaponBullet3 = Instantiate(bulletSmall, gunPoint.position, Quaternion.identity);
        weaponBullet3.GetComponent<Bullet>().SetColor(GetComponent<SpriteRenderer>().color);
        Destroy(weaponBullet3, 3);

        rotationAngle = bulletSpread;
        Quaternion width = Quaternion.Euler(0, 0, rotationAngle);
        GameObject weaponBullet4 = Instantiate(bulletSmall, gunPoint.position, width);
        weaponBullet4.GetComponent<Bullet>().SetColor(GetComponent<SpriteRenderer>().color);
        Destroy(weaponBullet4, 3);

        rotationAngle = -bulletSpread;
        Quaternion widthMirrored = Quaternion.Euler(0, 0, rotationAngle);
        GameObject weaponBullet5 = Instantiate(bulletSmall, gunPoint.position, widthMirrored);
        weaponBullet5.GetComponent<Bullet>().SetColor(GetComponent<SpriteRenderer>().color);
        Destroy(weaponBullet5, 3);
    }
}
//weapon scriptet kallar på spelaren, ref rigidbody, funktion direktion, power, force - trygetcomponent
//gamemanager, singleton?

//if (weaponBullet.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb2d))
//{
//    rb2d.velocity = bulletVelocity;
//    Debug.Log("Bullet spawned with rotation: " + rotationAngle);
//}
