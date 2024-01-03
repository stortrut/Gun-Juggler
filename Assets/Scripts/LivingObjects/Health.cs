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
   
    protected bool oneShot = true;
    protected int isDying = -1;
    private bool isProtected;
    public bool hasProtection { get {return isProtected; } set { isProtected = value; } }


    public delegate void Died();
    public Died died;


    private void Awake()
    {
        protection = GetComponent<EnemyProtection>();
    }

    public virtual void ApplyDamage(float amount)
    {
        if (oneShot)
        {
            health -= amount;
            health = Mathf.Clamp(health, 0, maxHealth);
            if (isDead = health == 0)
            {
                Death();
            }
        }
    }

    public virtual void Death()
    {
        died?.Invoke();
    }
}
