using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAim : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private List<GameObject> objectsInField=new List<GameObject>();
    private Vector2 closestEnemy=Vector2.one*100;
    private Vector2 currentEnemy;
    [HideInInspector]public GameObject Target;
    [SerializeField] private float aimDirection;
    public GameObject bullet;
    public Transform gunPoint;
    private Quaternion BulletRotation;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            objectsInField.Add(other.gameObject);
            
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            objectsInField.Remove(other.gameObject);
        }
 
    }

    public void Aim()
    {
        closestEnemy = Vector2.one * 100;
        Debug.Log("Tried to aim");
        foreach(GameObject obj in objectsInField)
        {
            currentEnemy = obj.transform.position-player.transform.position;
            //currentEnemy = new Vector2(Mathf.Abs(currentEnemy.x),Mathf.Abs(currentEnemy.y));

            if (currentEnemy.sqrMagnitude<closestEnemy.sqrMagnitude)
            {
                 Target = obj;
                 closestEnemy = currentEnemy;
            }
            //aimDirection = Vector3.Angle(Target.transform.position, player.transform.position);
            aimDirection = Mathf.Atan2(closestEnemy.y, closestEnemy.x) * Mathf.Rad2Deg;
            BulletRotation = Quaternion.Euler(0, 0, aimDirection);
        }
    }
    private void Update()
    {
        if(Target!= null)
        {
            Debug.DrawLine(player.transform.position, Target.transform.position, Color.yellow);
        }
     Aim();
       if(Input.GetKeyDown(KeyCode.T))
        {
            Shoot();
            Debug.Log(aimDirection);
        }
    }
    public void Shoot()
    {
        
        GameObject weaponBullet = Instantiate(bullet, gunPoint.position, BulletRotation);
        //Sound.Instance.EnemyNotTakingDamage();

        Bullet bulletScript = weaponBullet.GetComponent<Bullet>();
   

        weaponBullet.GetComponent<Rigidbody2D>().velocity = weaponBullet.transform.right * 5;
        
    }
}
