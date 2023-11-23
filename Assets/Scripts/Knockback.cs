using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    Rigidbody2D rb2D;
    [SerializeField] float knockbackSpeedX, knockbackSpeedY, knockbackDuration;

    private float knockbackStart;
    private bool knockback;
    private Vector2 currentVelocity, knockbackForce;
    private Vector2 knockbackDirection = new Vector2(0f,0f);

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        CheckKnockback();


        if (Input.GetKeyDown(KeyCode.K))
        {
            Vector2 testPos = new Vector2(transform.position.x + 2, transform.position.y + 2);

            KnockBackMyself(testPos);
        }
    }
    public void KnockBackMyself(Vector2 referenceTransformPosition)  //referenceTransformPosition is the thing that not gets knocked back
    {
        knockback = true;
        knockbackStart = Time.time;


        currentVelocity = rb2D.velocity;        //save velocity before knockback

        Vector2 distanceToKnockbackCauser = new Vector2(transform.position.x - referenceTransformPosition.x, 0);
        distanceToKnockbackCauser.Normalize();
        knockbackDirection.x = -distanceToKnockbackCauser.x; //direction is negative (-1) right now bacause the weapon is on the right side of the player
        //Vector2 knockbackDirection = (transform.position - (Vector3)referenceTransformPosition).normalized;

        knockbackForce.x += knockbackSpeedX * distanceToKnockbackCauser.x + 20;
        //rb2D.AddForce(new Vector2(knockbackForce.x, knockbackForce.y));


        rb2D.velocity = new Vector2(currentVelocity.x + knockbackDirection.x * knockbackSpeedX, knockbackSpeedY);

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
