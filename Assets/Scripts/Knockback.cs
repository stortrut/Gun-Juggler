using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    Rigidbody2D rb2D;
    [SerializeField] float knockbackSpeedX, knockbackSpeedY, knockbackDuration;

    private float knockbackStart, knockbackDirection;
    private bool knockback;
    private Vector3 currentVelocity;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        CheckKnockback();
    }
    public void KnockBackMyself(Vector3 referenceTransformPosition)  //referenceTransformPosition is the thing that not gets knocked back
    {
        Vector3 currentVelocity = rb2D.velocity;        //save velocity before knockback
        knockback = true;
        knockbackStart = Time.time;

        Vector3 distanceToKnockbackCauser = new Vector3(referenceTransformPosition.x - transform.position.x,0);     
        distanceToKnockbackCauser.Normalize();
        knockbackDirection = -distanceToKnockbackCauser.x; //direction is negative (-1) right now bacause the weapon is on the right side of the player

        Vector3 knockbackForce = new Vector3(knockbackSpeedX * distanceToKnockbackCauser.x, knockbackDuration);     //knockbackDuration = time moving upwards and Y-velocity
        knockbackForce.x += currentVelocity.x;
        rb2D.velocity = new Vector2(knockbackForce.x, knockbackForce.y);
    }

    private void CheckKnockback()
    {
        if (Time.time >= knockbackStart + knockbackDuration && knockback)
        {
            knockback = false;
            rb2D.velocity = new Vector3(0f, currentVelocity.y);
        }
    }

}
