using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyShoot : WeaponBase
{
    [SerializeField] protected float bulletDamage, bulletSpeed;
    [SerializeField] protected GameObject enemyBullet;
    [SerializeField] protected Vector2 spawnBulletPos;
    [SerializeField] protected Transform player;
    [HideInInspector] private Vector3 aim;
    // Start is called before the first frame update
    void Start()
    {
        AdjustAim();
    }

    // Update is called once per frame
    void Update()
    {
        if(gunPoint.position.x - player.position.x<18)
        {
            var i = Random.Range(0, 200);
            if(i % 99 == 0)
            {
                Shoot();
            }
            if(i == 100)
            {
                AdjustAim();
            }
        }
    }
    public void Shoot()
    {
        GameObject weaponBullet = Instantiate(enemyBullet, gunPoint.position, gunPoint.rotation);
        //Sound.Instance.EnemyNotTakingDamage();
        Rigidbody2D bulletRigidbody = weaponBullet.GetComponent<Rigidbody2D>();
        var bullet = weaponBullet.GetComponentInChildren<IAim>();
        bullet.AimInfo(aim);
        //bulletRigidbody.velocity = bulletSpeed  * (-weaponBullet.transform.right) ;
        //weaponBullet.GetComponent<Rigidbody2D>().velocity = weaponBullet.transform.right * bulletSpeed *Time.deltaTi;
        Destroy(weaponBullet, 10);  
    }
    private void AdjustAim()
    {
        aim = gunPoint.position - player.position;
        aim.z = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
        Debug.Log("I am aiming" + aim);
       
    }
}
