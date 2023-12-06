using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pie : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private float speed;
    [SerializeField] private float height;
    private float timeThrow;
    private float timeHit;
    public float rotationSpeed = 106.5f;
    private Quaternion startRotation;
    Vector2 startPosition;
    void Start()

    {
        startRotation = transform.rotation;
        startPosition = transform.position;
        rb2D.velocity = -Vector3.right * speed;
        rb2D.AddForce(Vector2.up * height);
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion targetRotation = Quaternion.Euler(0, 0, -180f);

        // Use RotateTowards to smoothly rotate the object
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.T))
        {
            timeThrow = Time.time;
            transform.position = startPosition;
            transform.rotation = startRotation;
            rb2D.velocity = -Vector3.right * speed;
            rb2D.AddForce(Vector2.up * height);
           



        }

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            timeHit = Time.time;
            Debug.Log(timeHit - timeThrow);
        }
    }
}
   

  
