//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ArchivedWeaponBaseAndWeaponTest : MonoBehaviour
//{
//    public float Damage { get; set; }
//    public float Speed { get; set; }
//    public float FireRate { get; set; }

//    public GameObject bulletPrefab;

//    public float timer;

//    public ArchivedWeaponBaseAndWeaponTest(float damage, float speed, float fireRate) //constructor, method that is called when creating the object and initializes it
//    {
//        this.Damage = damage;
//        this.Speed = speed;
//        this.FireRate = fireRate;
//    }

//    public void Fire()
//    {
//        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
//        //add velocity
//    }
//}
//public class Weapon : ArchivedWeaponBaseAndWeaponTest
//{
//    public Weapon(float damage, float speed, float fireRate)
//        : base(damage, speed, fireRate)
//    {
//    }
//    private void Update()
//    {
//        timer -= Time.deltaTime;
//        if (timer < 0)
//        {
//            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
//            timer = FireRate;
//            ArchivedWeaponBaseAndWeaponTest weaponBaseInstance = this;

//            weaponBaseInstance.Fire();
//        }
//    }
//}
//public class RunThis : MonoBehaviour
//{
//    private void Start()
//    {
//        Weapon revolver = new Weapon(10, 10, 1);
//    }
//}