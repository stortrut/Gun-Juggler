using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : WeaponBase
{
    [SerializeField]
    WeaponBase weaponBase;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            weaponBase.Use(10f,2f,1f,transform.position);
        }
    }
    public void Shoot(float damage, float speed, float fireRate, Vector3 pos)
    {
        GameObject weaponBullet = Instantiate(Bullet, pos, Quaternion.identity);
        weaponBullet.GetComponent<Rigidbody>().velocity = pos * speed;
    }
}
