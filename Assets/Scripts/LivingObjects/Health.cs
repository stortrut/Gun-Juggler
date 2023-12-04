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

    [SerializeField] private EnemyProtection protection;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private HealthUI healthImage;
    private bool colorischanged;
    private bool oneShot = true;
    private bool isProtected;
    public bool hasProtection { get {return isProtected; } set { isProtected = value; } }

    void Awake()
    {
        maxHealth = health;
        protection = GetComponent<EnemyProtection>();
    }

    public void ApplyDamage(int amount)
    {
        if(oneShot == true)
        { 
            BoolChange();                   //so that multible bullets only make one damage (so that number of protection dont get less than the actual number of protaction)
            Invoke(nameof(BoolChange), .2f);
            health -= amount;
            health = Mathf.Clamp(health, 0, maxHealth);
            if(healthImage != null)
            {
                healthImage.UpdateHealth(health , maxHealth);
            }   
            if (health < -2)
            {
                //Destroy(gameObject);
            }
        }
    }
   
    public void OnTrigger()
    {
        ColorChange(1);
        Invoke(nameof(ColorChange), 0.3f);
    }

    private void ColorChange(int color)
    {
        if (colorischanged == false)
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