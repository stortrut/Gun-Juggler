using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    public int health;
    private EnemyProtection ProtectionScript;
    private bool isProtected;
   
    public EnemyProtection Parent;
    private SpriteRenderer spriteRenderer;
    private bool colorischanged;
    

    public bool hasProtection { get {return isProtected; } set { isProtected = value; } }
    
     
    void Awake()
    {
        ProtectionScript = GetComponent<EnemyProtection>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        
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

    public void HasParent()
    {
        if (gameObject.transform.parent != null)
        {
            //keep in mind the enemy has to be the ROOT parent for this to actually work
            Parent = transform.root.GetComponentInParent<EnemyProtection>();
            Parent.RemoveProtection(1);

        }
    }
    public void OnTrigger()
    {
        ColorChange(1);
        Invoke(nameof(ColorChange),0.3f);
    }
   

    private void ColorChange(int color)
    {
        if (colorischanged==false)
        {
        spriteRenderer.color = Color.red;
        }
        if (isProtected == true)
        {
            spriteRenderer.color = Color.blue;
        }

        colorischanged = true;
     
    }
    private void ColorChange()
    {
        spriteRenderer.color = Color.white;
        colorischanged = false;
    }
}
