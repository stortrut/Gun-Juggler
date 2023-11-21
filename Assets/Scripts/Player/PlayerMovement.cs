using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;

    [Header("Movement")]
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float speed = 7f;
    [SerializeField] private float deacceleration = 10f;

    private float horizontalInput;
    private float velocityToAddX;


    // Update is called once per frame
    void Update()
    {
        Walk();
    }


    private void Walk()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        velocityToAddX += horizontalInput * acceleration * Time.deltaTime;
        velocityToAddX = Mathf.Clamp(velocityToAddX, -speed, speed);

        if (horizontalInput == 0 || (horizontalInput < 0 == velocityToAddX > 0))
        {
            velocityToAddX *= 1 - deacceleration * Time.deltaTime;
        }

        rigidBody.velocity = new Vector2(velocityToAddX, rigidBody.velocity.y);
    }
}
