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
            Debug.Log("the trigger happens :D");
            if (hasProtection == false)
            {
                Sound.Instance.SoundRandomized(Sound.Instance.enemyTakingDamageSounds);
                ApplyDamage(1);
            }
            else if (hasProtection == true)
            {
                Sound.Instance.SoundRandomized(Sound.Instance.enemyNotTakingDamageSounds);
            }

        }
    }
}
