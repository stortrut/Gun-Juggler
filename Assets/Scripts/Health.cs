using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int health;
    [SerializeField] private int protection;

    public int Protection
    {
        get { return protection; }
        set { if (value < 0)
            {
                protection = 0;
            }
                }
    }

    public void ApplyDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void RemoveProtection(int protection)
    {

    }
}
