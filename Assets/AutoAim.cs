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
    private Quaternion bulletRotation;

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
            bulletRotation = Quaternion.Euler(0, 0, aimDirection);
        }
    }
    private void Update()
    {
     Aim();

    }
}
