using DG.Tweening;
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
            UpgradeCombo.Instance.hitSinceShot = true;
            UpgradeCombo.Instance.comboTween.Kill();
            ApplyDamage(1);
            if (health == 0)
            {
                Death();
            }
            Destroy(other.gameObject);
        }
    }

    public override void Death()
    {
        if (hasFunctionBeenCalled == false)
        {
            hasFunctionBeenCalled = true;
            //Sound.Instance.SoundRandomized(Sound.Instance.balloonPop);
            EffectAnimations.Instance.BalloonPop(this.gameObject.transform.position);
            HasParent();
            Destroy(this.gameObject);
        }
        else
        {
        }
    }
    public void HasParent()
    {
        if (gameObject.transform.parent != null)
        {
            parent = GetComponentInParent<EnemyProtection>();
            if(parent != null)
                parent.RemoveProtection(1);
            //}
        }
    }
}
