using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class EnemyShoot : WeaponBase
{
    [SerializeField] protected float bulletDamage, bulletSpeed;
    [SerializeField] protected GameObject enemyBullet;
    [SerializeField] protected Vector2 spawnBulletPos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var i = Random.Range(0, 25);
        if(i == 8)
        {
            Shoot();
        }

    }
    public void Shoot()
    {
        GameObject weaponBullet = Instantiate(enemyBullet, gunPoint.position, gunPoint.rotation);
        //Sound.Instance.EnemyNotTakingDamage();
        Rigidbody2D bulletRigidbody = weaponBullet.GetComponent<Rigidbody2D>();
        //bulletRigidbody.velocity = bulletSpeed  * (-weaponBullet.transform.right) ;
        //weaponBullet.GetComponent<Rigidbody2D>().velocity = weaponBullet.transform.right * bulletSpeed *Time.deltaTi;
        Destroy(weaponBullet, 10);  
    }

}
