using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        
    
        if (other.gameObject.CompareTag("Bullet"))
        {
            OnTrigger();
            Debug.Log("the trigger happens :D");
            if (hasProtection == false)
            {
                Sound.Instance.EnemyTakingDamage();
                ApplyDamage(1);
            }
            else if (hasProtection == true)
            {
                Sound.Instance.EnemyNotTakingDamage();
            }
          
                if (TryGetComponent(out Knockback knockbackComponent))
                {
                    Debug.Log("enemy knockback");
                    knockbackComponent.KnockBackMyself(15, 10, .4f, transform.position);
                }
            }
    }

}
    

