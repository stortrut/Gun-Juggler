using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, IDamageable
{
    [HideInInspector]public int maxHealth;
    public int health;

     private EnemyProtection protection;
   
    private bool oneShot = true;
    private bool isProtected;
    public bool hasProtection { get {return isProtected; } set { isProtected = value; } }

    void Awake()
    {
        
        protection = GetComponent<EnemyProtection>();
    }

    public void ApplyDamage(int amount)
    {
        if(oneShot == true)
        { 
            BoolChange();                   //so that multible bullets only make one damage (so that number of protection dont get less than the actual number of protaction)
            Invoke(nameof(BoolChange), .2f);
            health -= amount;
            health = Mathf.Clamp(health, 0, maxHealth);
           
            if (health < -2)
            {
                //Destroy(gameObject);
            }
        }
    }
    private void BoolChange()
    {
        oneShot = !oneShot;
    }
}
