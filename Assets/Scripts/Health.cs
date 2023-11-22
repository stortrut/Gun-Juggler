using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health:MonoBehaviour,IDamageable
{
    [SerializeField] private int health;
    public void ApplyDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
        
}
