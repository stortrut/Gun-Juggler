using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
          if(hasProtection==true)
            {
                Sound.Instance.EnemyNotTakingDamage();
            }
          else
            {
                Sound.Instance.EnemyTakingDamage();
                ApplyDamage(1);

                if (TryGetComponent(out Knockback knockbackComponent))
                {
                    Debug.Log("enemy knockback");
                    knockbackComponent.KnockBackMyself(15, 10, .4f, transform.position);
                }
            }
        }
    }
}
