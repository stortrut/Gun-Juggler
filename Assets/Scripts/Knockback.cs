using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    Rigidbody2D rb2D;
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    public void KnockBackMyself(Vector3 referenceTransformPosition, float knockbackForce)  //referenceTransformPosition is the thing that not gets knocked back
    {
        Vector3 distance = new Vector3(referenceTransformPosition.x - transform.position.x, 0);
        distance.Normalize();
        Debug.Log(distance);
        rb2D.AddForce(knockbackForce * distance, ForceMode2D.Impulse);
        //transform.position += new Vector3(-knockbackDirection * 1, 0, 0);
    }
}
