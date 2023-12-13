using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, IDamageable
{
    [HideInInspector]public float maxHealth = 1000;
    public float health;

    [HideInInspector] public bool isDead;

    private EnemyProtection protection;
   
    private bool oneShot = true;
    private bool isProtected;
    public bool hasProtection { get {return isProtected; } set { isProtected = value; } }

    private void Awake()
    {
        
        protection = GetComponent<EnemyProtection>();
    }

    public void ApplyDamage(float amount)
    {
        if(oneShot)
        {
            
            //BoolChange();                   //so that multible bullets only make one damage (so that number of protection dont get less than the actual number of protaction)
            //Invoke(nameof(BoolChange), .2f);
            health -= amount;
            health = Mathf.Clamp(health, 0, maxHealth);
            isDead = health == 0;
        }
    }
    private void BoolChange()
    {
        oneShot = !oneShot;
    }
}
