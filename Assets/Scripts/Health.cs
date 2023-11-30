using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, IDamageable
{
    private int maxHealth;
    public int health;
    private EnemyProtection ProtectionScript;
    [SerializeField]private HealthUI healthImage; 
    private bool isProtected;
    public EnemyProtection parent;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool colorischanged;
    public bool oneShot = true;
    public bool hasProtection { get {return isProtected; } set { isProtected = value; } }

    void Awake()
    {
        maxHealth = health;
        ProtectionScript = GetComponent<EnemyProtection>();
    }

    public void ApplyDamage(int amount)
    {
        if(oneShot == true)
        { 
            BoolChange();
            Invoke(nameof(BoolChange), 0.3f);
            health -= amount;
            Mathf.Clamp(health, 0, maxHealth);
            if(healthImage!=null)
            {
                healthImage.UpdateHealth(health , maxHealth);
            }
            if (health == 0)
            {
                Destroy(gameObject);
            }
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
    private void BoolChange()
    {
        oneShot = !oneShot;
    }
}
