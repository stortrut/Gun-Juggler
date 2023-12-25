using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;


public class BalloonHealth : Health, IStunnable
{

    [HideInInspector] public bool isStunned = false;
    public bool isStunnable { get { return isStunned; } set { isStunned = value; } }

    [Header("Drag in")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            ApplyDamage(1);
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

    Color orange = new Color(1, 0.47f, 0, 1);
    Color yellow = new Color(1, 0.8f, 0, 1);
    Color blue = Color.blue;
    Color green= Color.green;   

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
        EffectAnimations.Instance.BalloonPop(transform.position);
        transform.localScale *= 0.9f;
        spriteRenderer.color = currentColor;
    }
    public override void Death()
    {
        EffectAnimations.Instance.BalloonPop(transform.position);
        Destroy(gameObject);
    }
  
  
}
