using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : Health
    
{
    private EnemyProtection enemyProtection;
    
    private void Start()
    {
        var enemyProtection= TryGetComponent(out EnemyProtection component);
        enemyProtection = component;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
    
        if (other.gameObject.CompareTag("Bullet")&&hasProtection==false)
        {
            OnTrigger();
            Debug.Log("the BIG trigger happens :D");
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
    

