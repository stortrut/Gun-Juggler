using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class EnemyShoot : WeaponBase
{
    [SerializeField] protected float bulletDamage, bulletSpeed;
    [SerializeField] protected GameObject bulletSmall;
    [SerializeField] protected Vector2 spawnBulletPos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Shoot()
    {
        GameObject weaponBullet = Instantiate(bulletSmall, gunPoint.position, gunPoint.rotation);
        //Sound.Instance.EnemyNotTakingDamage();
        weaponBullet.GetComponent<Bullet>().SetColor(GetComponent<SpriteRenderer>().color);
        Rigidbody2D bulletRigidbody = weaponBullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = bulletSpeed * Time.deltaTime * (-weaponBullet.transform.right) ;
        //weaponBullet.GetComponent<Rigidbody2D>().velocity = weaponBullet.transform.right * bulletSpeed *Time.deltaTi;
        Destroy(weaponBullet, 1.75f);
    }

}
