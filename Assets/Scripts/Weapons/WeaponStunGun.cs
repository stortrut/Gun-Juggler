using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStunGun : Gun
{
    [SerializeField] private StunZone stunZone;
    

    //[SerializeField] private GameObject StunZone;

    IEnumerator UnFreeze(float timeStunned, IStunnable stunnable)
   {
     yield return new WaitForSeconds(timeStunned);
        stunnable.isStunnable = false;
    }
    //tryck för att spela: deflecta bullets och stunna enemies
    //raycast, stun interface, deflect script
  
    private void Update()
    {
        fireRateTimer -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && fireRateTimer < 0)
        {
            if (this.weaponEquipped)
            {
                
                ReflectStun();
            }
        }
    }
    private void ReflectStun()
        
    {
        foreach (GameObject obj in stunZone.objectsInField)
        {
            if (obj.CompareTag("EnemyBullet"))
            {
                var enemyBullet = obj.GetComponent<EnemyBullet>();
                enemyBullet.Deflected();
                //add bool so that enemy bullets now can damage enemies,
            }
            else if (obj.CompareTag("Enemy"))
            {
                var stunnable = obj.GetComponent<IStunnable>();
                stunnable.isStunnable = true;
                StartCoroutine(UnFreeze(2,stunnable));
                Debug.Log(obj);
            }
            
        }
    }
}

