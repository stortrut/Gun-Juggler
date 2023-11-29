using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionHealth : Health
{
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Bullet"))
        {
            OnTrigger();

            if (hasProtection == false)
            {
                Sound.Instance.EnemyTakingDamage();
                ApplyDamage(1);
            }
            else if (hasProtection == true)
            {
                Sound.Instance.EnemyNotTakingDamage();
            }

        }
    }
}
