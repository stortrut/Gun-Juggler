using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionHealth : Health
{
    private bool hasFunctionBeenCalled = false;
    private EnemyProtection parent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            ApplyDamage(1);
            if (health == 0)
            {
                Death();
            }
            Destroy(other.gameObject);

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
        if (hasFunctionBeenCalled == false)
        {


            hasFunctionBeenCalled = true;
            //Sound.Instance.SoundRandomized(Sound.Instance.balloonPop);
            EffectAnimations.Instance.BalloonPop(this.gameObject.transform.position);
            //Debug.Log(this.gameObject.transform.position);

            HasParent();

            Destroy(this.gameObject);
        }
        else
            Debug.Log("");
    }
    public void HasParent()
    {
        if (gameObject.transform.parent != null)
        {
            //keep in  mind the enemy has to be the ROOT parent for this to actually work
            parent = GetComponentInParent<EnemyProtection>();
            //if (oneShot == false)
            //{
                parent.RemoveProtection(1);
            //}
        }
    }
}
