using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int health;
    private EnemyProtection ProtectionScript;
    private int protection=0;
    private Health Parent;
    
    

    [HideInInspector] public int Protection
    {
        get {
            if (ProtectionScript == null)
            {
                return protection;
            }
                return ProtectionScript.numberOfProtection; 
        }
        set { if (value < 0)
            {
                ProtectionScript.numberOfProtection = 0; 
            }
                }
    }
    void Start()
    {
        ProtectionScript = GetComponent<EnemyProtection>();
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


        ProtectionScript.numberOfProtection -= amount;
        Debug.Log(Protection+gameObject.name);
    }
}
