using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int health;
    private EnemyProtection ProtectionScript;
    public bool isProtected;
    private EnemyProtection Parent;

    public bool hasProtection { get { return isProtected; } set {    } }


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
            Parent = transform.root.GetComponentInParent<EnemyProtection>();
            Debug.Log(Parent.name);
            Parent.RemoveProtection(1);

        }
    }
}
