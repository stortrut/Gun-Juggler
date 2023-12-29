using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;


public class BalloonHealth : Health
{

  
    [Header("Drag in")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Start()
    {
        if(health == 4)
        {
            spriteRenderer.color = blue;
        }
        if (health == 3)
        {
            spriteRenderer.color = orange;
        }
        if (health == 2)
        {
            spriteRenderer.color = yellow;
        }
        if (health == 1)
        {
            spriteRenderer.color = green;
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            ApplyDamage(1);
            AudienceSatisfaction.Instance.AudienceHappiness(1);
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("HoolaHoop"))
        {
            AudienceSatisfaction.Instance.AudienceHappiness(-health * 2);
            Destroy(gameObject);
            // CUSTOM FAIL DEATH;
        }
    }
    public override void ApplyDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        PopLayer();
        if (isDead = health == 0)
        {
            Death();
        }
    }

    Color orange = new Color(1, 0.52f, 0.24f, 1);
    Color yellow = new Color(0.8f, 0.8f, 0.3f, 1);
    Color blue = new Color(0.07f, 0.56f, 0.81f, 1);
    Color green = new Color(0.2f, 0.82f, 0.37f, 1);

    private void PopLayer()
    {
        var currentColor = Color.white;
        if(health == 3)
        {
            currentColor = orange;
        }
        if (health == 2)
        {
            currentColor = yellow;
        }
        if (health == 1)
        {   
            currentColor = green;
        }
        if(this != null)
        { 
        EffectAnimations.instance.BalloonPop(transform.position);
        transform.localScale *= 0.9f;
        spriteRenderer.color = currentColor;
        }
    }
    public override void Death()
    {
        if(this != null)
        { 
        EffectAnimations.instance.BalloonPop(transform.position);
        Destroy(gameObject);
        }
    }
  
  
}
