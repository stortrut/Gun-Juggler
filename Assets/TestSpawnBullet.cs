using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnBullet : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Vector2 bulletVelocity = new Vector2(2,0);
    public float rotationAngle; 
    void Start()
    {
        rotationAngle = Random.Range(-45, 45);
        Quaternion offset = Quaternion.Euler(0, 0, rotationAngle); // Create a rotation based on the angle
        GameObject weaponBullet = Instantiate(bulletPrefab, transform.position, offset);

        //if (weaponBullet.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb2d))
        //{
        //    rb2d.velocity = bulletVelocity;
        //    Debug.Log("Bullet spawned with rotation: " + rotationAngle);
        //}
    }


    void Update()
    {
        
    }
}
