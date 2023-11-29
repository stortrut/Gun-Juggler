using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : Health
    
{
    
  
    private void OnTriggerEnter2D(Collider2D other)
    {
        
    
        if (other.gameObject.CompareTag("Bullet"))
        {
            OnTrigger();
            Debug.Log("the BIG trigger happens :D");
            if (hasProtection == false)
            {
                Sound.Instance.SoundRandomized(Sound.Instance.enemyTakingDamageSounds);
                ApplyDamage(1);
            }
            else if (hasProtection == true)
            {
                Sound.Instance.SoundRandomized(Sound.Instance.enemyNotTakingDamageSounds);
            }
          
                if (TryGetComponent(out Knockback knockbackComponent))
                {
                    Debug.Log("enemy knockback");
                    knockbackComponent.KnockBackMyself(15, 10, .4f, transform.position);
                }
            }
    }

}
    

