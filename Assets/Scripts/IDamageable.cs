using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDamageable : MonoBehaviour
{
    public float currentHealth;

    public void ApplyDamage(float amount)
    {
        currentHealth -= amount;
    }
}
