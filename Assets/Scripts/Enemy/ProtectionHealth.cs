using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionHealth : Health
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Death();
            Destroy(other.gameObject);
            OnTrigger();

            //if (hasProtection == false)       Protection protection
            //{
            //    ApplyDamage(1);
                
            //}
            //else if (hasProtection == true)
            //{
            //    
            //}
        }
    }

    void Death()
    {
        //Sound.Instance.SoundRandomized(Sound.Instance.balloonPop);
        EffectAnimations.Instance.BalloonPop(this.gameObject.transform.position);
        Debug.Log(this.gameObject.transform.position);
        Destroy(this.gameObject);
    }
}
