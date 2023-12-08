using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class EnemyShoot : MonoBehaviour, IStunnable
{
    [SerializeField] protected float bulletDamage, bulletSpeed;
    [SerializeField] protected GameObject enemyBullet;
    [SerializeField] protected Transform spawnBulletPos;
    protected GameObject player;
    [HideInInspector] private Vector3 aim;

    [HideInInspector] public bool isStunned = false;
    [HideInInspector] public bool timeStop;
    private float lastSecond = 0;
    public bool isStunnable { get { return isStunned; } set { isStunned = value; } }
    public bool timeStopped { get { return timeStop; } set { timeStop = value; } }

    void Update()
    {
        if (player == null)
        {
            player = Manager.Instance.player;
            AdjustAim();
        }

        if (isStunned == true) { return; }

        if (spawnBulletPos.position.x - player.transform.position.x < 14)
        {
            
            float currentTime = Time.time;
            int currentSecond = (int)currentTime;

            if (currentSecond > lastSecond)
            {
                lastSecond = currentSecond;
                var i = 1; // Random.Range(0, 2);
                if (i == 1)
                {
                    Shoot();
                }
                
            }
            if (currentSecond % 4 == 0)
            {
                AdjustAim();
            }
        }
    }
    public void Shoot()
    {
        GameObject weaponBullet = Instantiate(enemyBullet, spawnBulletPos.position, spawnBulletPos.rotation);
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
        if (player == null) { return; }

        aim = spawnBulletPos.position - player.transform.position;
        aim.z = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
        //Debug.Log("I am aiming" + aim);
    }
}
