using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAim : MonoBehaviour
{
    private PlayerJuggle playerJuggle;

    private Vector2 closestEnemy=Vector2.one*100;
    private Vector2 currentEnemy;   

    private Transform target;
    private float aimDirection;

    [HideInInspector] public Quaternion bulletRotation;

    public List<Transform> objectsInField = new();


    private void Start()
    {
        playerJuggle = FindObjectOfType<PlayerJuggle>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.TryGetComponent(out TargetableByPlayerAutoAim newTarget))
        {
            objectsInField.Add(newTarget.GetAimPos());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.TryGetComponent(out TargetableByPlayerAutoAim newTarget))
        {
            objectsInField.Remove(newTarget.GetAimPos());
        }
    }

    public void Aim()
    {
        closestEnemy = Vector2.one * 100;
        
        foreach(Transform obj in objectsInField)
        {
            if(playerJuggle.weaponInHand == null) { return; }

            currentEnemy = obj.position - playerJuggle.weaponInHand.weaponBase.gunPoint.transform.position;
            //currentEnemy = new Vector2(Mathf.Abs(currentEnemy.x),Mathf.Abs(currentEnemy.y));

            if (currentEnemy.sqrMagnitude<closestEnemy.sqrMagnitude)
            {
                 target = obj;
                 closestEnemy = currentEnemy;
            }

            //aimDirection = Vector3.Angle(Target.transform.position, player.transform.position);
            aimDirection = Mathf.Atan2(closestEnemy.y, closestEnemy.x) * Mathf.Rad2Deg;
            bulletRotation = Quaternion.Euler(0, 0, aimDirection);
        }

        if(objectsInField.Count == 0)
        {
            bulletRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void Update()
    {
        Aim();
    }
}
