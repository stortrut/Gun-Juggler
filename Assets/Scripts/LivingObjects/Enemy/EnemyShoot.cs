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
     protected GameObject player;
    [HideInInspector] private Vector3 aim;
    // Start is called before the first frame update
    void Start()

    {
        player = PlayerHealth.s_player;
       
        AdjustAim();
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null) { return; }

        if(gunPoint.position.x - player.transform.position.x<18)
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
        if(player == null) { return; }

        aim = gunPoint.position - player.transform.position;
        aim.z = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
        Debug.Log("I am aiming" + aim);
    }
}
