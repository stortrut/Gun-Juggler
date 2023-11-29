using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionHealth : Health
{
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            OnTrigger();

            if (hasProtection == false)
            {
                ApplyDamage(1);
                Sound.Instance.SoundRandomized(Sound.Instance.enemyTakingDamageSounds);
                
            }
            else if (hasProtection == true)
            {
                Sound.Instance.SoundRandomized(Sound.Instance.enemyNotTakingDamageSounds);
            }

        }
    }
}
