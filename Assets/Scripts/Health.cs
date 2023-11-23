using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int health;
    [SerializeField] private int protection;
    private Health Parent;


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
            //keep in mind the enemy has to be the ROOT parent for this to actually work
            Parent = transform.root.GetComponentInParent<Health>();
            Debug.Log(Parent.name);
            Parent.RemoveProtection(1);
            
        }
    }

    public void RemoveProtection(int amount)
    {

        
        protection -= amount;
        Debug.Log(Protection+gameObject.name);
    }
}
