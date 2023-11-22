using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : WeaponBase
{
    private void Start()
    {
        bulletSpeed = 20f;
        Debug.Log(gunPoint, gameObject);

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    public void Shoot()
    {
        GameObject weaponBullet = Instantiate(bullet, gunPoint.position, gunPoint.rotation);
        weaponBullet.GetComponent<Rigidbody2D>().velocity = weaponBullet.transform.right * bulletSpeed;
    }
}
