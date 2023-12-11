using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    Rigidbody2D rb2D;
    IStunnable stunnable;

    [Header("Test Values")]
    [SerializeField] float knockbackSpeedX, knockbackSpeedY, knockbackDuration;

    private float knockbackStart;
    private bool knockback;
    private Vector2 knockbackForce;
    private float knockbackDirection;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        stunnable = GetComponent<IStunnable>();
    }

    private void Update()
    {
        CheckKnockback();
    }
    public void KnockBackMyself(float knockbackSpeedX, float knockbackSpeedY, float knockbackDurationInput, Vector2 referenceTransformPosition)  //referenceTransformPosition is the thing that not gets knocked back
    {
        stunnable.isStunnable = true;
        knockback = true;
        knockbackStart = Time.time;
        knockbackDuration = knockbackDurationInput;

        float distanceToKnockbackCauser = referenceTransformPosition.x - transform.position.x;  //knockbackdirection
        if (distanceToKnockbackCauser > 0)
        { knockbackDirection = -1f; }
        else 
        { knockbackDirection = 1f;  }

        knockbackForce.x = knockbackSpeedX * knockbackDirection;
        knockbackForce.y = knockbackSpeedY;
        rb2D.velocity = knockbackForce;
       
        
    }
        public void KnockBackMyself(float knockbackSpeedX, float knockbackSpeedY, float knockbackDurationInput, Transform referenceTransform)  //referenceTransformPosition is the thing that not gets knocked back
        {
            stunnable.isStunnable = true;
            knockback = true;
            knockbackStart = Time.time;
            knockbackDuration = knockbackDurationInput;

            Quaternion oppositeRotation = Quaternion.Inverse(referenceTransform.rotation);
            Vector3 euler = oppositeRotation.eulerAngles;
             
       
        //Quaternion oppositeX = Quaternion.Euler(0, 180, 0);

        // Alternatively, if you want to flip only along the Y-axis:

        knockbackForce = new Vector2(knockbackSpeedX, knockbackSpeedY);
            // Rotate the knockbackForce vector based on the opposite rotation
            knockbackForce= oppositeRotation * knockbackForce;
            knockbackForce = new Vector2(-knockbackForce.x,knockbackForce.y);//new Vector2(knockbackForceX.x, knockbackForceY.y);
        if (euler.z > 85 && euler.z < 90)
        {
            knockbackForce.x = 0;
        }
        if (euler.z < 90f && euler.z > 45f)
        {
            knockbackForce = new Vector2(-knockbackForce.x, knockbackForce.y);
        }
       
        rb2D.velocity = knockbackForce;


        }

    private void CheckKnockback()
    {
        if (Time.time >= knockbackStart + knockbackDuration && knockback)
        {
            rb2D.velocity = Vector2.zero;
            rb2D.AddForce(new Vector2(0, -50f));
            Invoke(nameof(NoForce), .2f);
            Invoke(nameof(AllowMovement), .2f);
        }
    }
    private void AllowMovement()
    {

        stunnable.isStunnable = false;
        knockback = false;
  
       
    }

    private void NoForce()
    {
        rb2D.AddForce(new Vector2(0, 0));
    }

}
