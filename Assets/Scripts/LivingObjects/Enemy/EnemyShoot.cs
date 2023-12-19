using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class EnemyShoot : MonoBehaviour, IStunnable
{
    [SerializeField] protected GameObject enemyBullet;
    [SerializeField] protected Transform spawnBulletPos;
    protected GameObject player;
    [HideInInspector] EnemyHealth enemyHealth;
    //[HideInInspector] EnemyAnimator enemyAnimator;
    EnemyAnimator animator;
    [HideInInspector] private Vector3 aim;
    [HideInInspector] public bool isStunned = false;
    private float nextShootTime = 0;
    private bool once = true;
    public bool isStunnable { get { return isStunned; } set { isStunned = value; } }

    void Awake()
    {
        //enemyAnimator = GetComponent<EnemyAnimator>();
        enemyHealth = GetComponent<EnemyHealth>();
    }
    void Update()
    {
        if (player == null)
        {
            player = Manager.Instance.player;
            AdjustAim();
        }

        if (isStunned == true) 
        {
            if(once == true)
            {
                 animator.Stunned(true);
                once = false;
            }
                 return; 
        }
        else
        {
            if (once == false)
            {
                animator.Stunned(false);
                once = true;    
            } 
        }

        if (spawnBulletPos.position.x - player.transform.position.x < 18)
        {
            if(once == false)
            {
                AdjustAim();
                once = true;

            }
            float currentTime = Time.time;
            if (currentTime > nextShootTime)
            {
                // Set a new random interval between 1 and 2 seconds
                nextShootTime = currentTime + Random.Range(0.6f, 2f);

                // Perform the shooting and aim adjustment
                StartCoroutine(nameof(Shoot));
                if (enemyHealth.died)
                { return; }
                AdjustAim();
            }
        }
    }
            //    float currentTime = Time.time;
            //int currentSecond = (int)currentTime;

            //if (currentSecond > lastSecond)
            //{
            //    lastSecond = currentSecond;
            //    Shoot();
            //    AdjustAim();


            //}
    
    IEnumerator Shoot()
    {
        animator = GetComponent<EnemyAnimator>();
        animator.Attacking();
        if (GetComponent<EnemyAnimator>().enemyType == EnemyType.PieClown)
        {
            yield return new WaitForSeconds(0.5f);
        }

        GameObject weaponBullet = Instantiate(enemyBullet, spawnBulletPos.position, spawnBulletPos.rotation);
        //Sound.Instance.EnemyNotTakingDamage();
        Rigidbody2D bulletRigidbody = weaponBullet.GetComponent<Rigidbody2D>();
        var bullet = weaponBullet.GetComponentInChildren<IAim>();
        bullet.AimInfo(aim);

        //PIE
        if(weaponBullet.GetComponent<Bullet>() != null)
            weaponBullet.GetComponent<Bullet>().bulletDamage = 75f;
        else
        {
            weaponBullet.GetComponentInChildren<Bullet>().bulletDamage = 75f;
        }


        //bulletRigidbody.velocity = bulletSpeed  * (-weaponBullet.transform.right) ;
        //weaponBullet.GetComponent<Rigidbody2D>().velocity = weaponBullet.transform.right * bulletSpeed *Time.deltaTi;
        Destroy(weaponBullet, 10);
        //if (enemyAnimator == null) { return; }

        yield return new WaitForSeconds(0.1f);
    }
    private void AdjustAim()
    {
        if (player == null) { return; }

        aim = spawnBulletPos.position - player.transform.position;
        aim.z = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
        //Debug.Log("I am aiming" + aim);
    }
}
