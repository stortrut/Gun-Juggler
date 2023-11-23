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
            HasParent();
            Destroy(gameObject);
        }
    }

    private void HasParent()
    {
        if (gameObject.transform.parent != null)
        {

            EnemyProtection enemyProtection = gameObject.GetComponentInParent<EnemyProtection>();
            Debug.Log(enemyProtection);
            enemyProtection.ChildDied();
        }
    }

    public void RemoveProtection(int protection)
    {

    }
}
