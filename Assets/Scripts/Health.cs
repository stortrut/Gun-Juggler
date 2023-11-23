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
    private SpriteRenderer spriteRenderer;
    private bool colorischanged;
    

    public bool hasProtection { get { return isProtected; } set {    } }


    void Start()
    {
        ProtectionScript = GetComponent<EnemyProtection>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
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
