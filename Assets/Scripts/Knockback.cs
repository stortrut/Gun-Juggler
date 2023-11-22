using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    Rigidbody2D rb2D;
    [SerializeField] float knockbackSpeedX, knockbackSpeedY, knockbackDuration;

    private float knockbackStart;
    private bool knockback;
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
        knockback = true;
        knockbackStart = Time.time;
        Vector3 distance = new Vector3(referenceTransformPosition.x - transform.position.x, 0);
        distance.Normalize();
        rb2D.velocity = new Vector2(knockbackSpeedX * distance.x, knockbackDuration);
    }

    private void CheckKnockback()
    {
        if ( Time.time >= knockbackStart + knockbackDuration && knockback)
        {
            knockback = false;
            rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
        }
    }
}
