using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : WeaponBase
{
    [SerializeField]
    WeaponBase weaponBase;
    private void Start()
    {
    }
    private void Update()
    {
    }
    public void Shoot(float damage, float speed, float fireRate, Vector3 pos)
    {
        GameObject weaponBullet = Instantiate(Bullet, pos, Quaternion.identity);
        weaponBullet.GetComponent<Rigidbody>().velocity = pos * speed;
    }
}
