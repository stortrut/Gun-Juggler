using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : WeaponBase
{
    [SerializeField] protected float bulletDamage, bulletSpeed;
    [SerializeField] protected int currentWeaponLevel = 0;

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
        Rigidbody2D bulletRigidbody = weaponBullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = weaponBullet.transform.right * bulletSpeed *Time.deltaTime;
        //weaponBullet.GetComponent<Rigidbody2D>().velocity = weaponBullet.transform.right * bulletSpeed *Time.deltaTi;
        Destroy(weaponBullet, 1.75f);
    }
    public void ShootWideSpread(int bulletCount)
    {
        GameObject weaponBullet = Instantiate(bulletSmall, gunPoint.position, Quaternion.identity);
        weaponBullet.GetComponent<Bullet>().SetColor(GetComponent<SpriteRenderer>().color);
        Destroy(weaponBullet, 1);

        EdgeBullets(1);
        EdgeBullets(-1);
        ShootWideSpreadRandom();

        void EdgeBullets(int bulletSpreadMultiplier)
        {
            rotationAngle = bulletSpread*bulletSpreadMultiplier;
            Quaternion width = Quaternion.Euler(0, 0, rotationAngle);
            GameObject weaponBullet = Instantiate(bulletSmall, gunPoint.position, width);
            weaponBullet.GetComponent<Bullet>().SetColor(GetComponent<SpriteRenderer>().color);
            Destroy(weaponBullet, 1);
        }

        void ShootWideSpreadRandom()
        {
            for (int i = 0; i < bulletCount; i++)
            {
                float rotationAngle = Random.Range(5, 40);
                //spawnBulletPos.x = gunPoint.position.x + radius * Mathf.Sin(rotationAngle * Mathf.Deg2Rad);
                //spawnBulletPos.y = gunPoint.position.y + radius * Mathf.Cos(rotationAngle * Mathf.Deg2Rad);
                Quaternion offsetRotation = Quaternion.Euler(0, 0, rotationAngle);
                GameObject weaponBullet = Instantiate(bulletSmall, gunPoint.position, offsetRotation);
                weaponBullet.GetComponent<Bullet>().SetColor(GetComponent<SpriteRenderer>().color);
                Destroy(weaponBullet, 1);

                Quaternion offsetRotationMirrored = Quaternion.Euler(0, 0, -rotationAngle);
                GameObject weaponBullet2 = Instantiate(bulletSmall, gunPoint.position, offsetRotationMirrored);
                weaponBullet2.GetComponent<Bullet>().SetColor(GetComponent<SpriteRenderer>().color);
                Destroy(weaponBullet2, 1);
            }
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
