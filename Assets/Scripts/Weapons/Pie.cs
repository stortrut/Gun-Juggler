using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pie : EnemyBullet
{
    
    [SerializeField] private float speed;
    [SerializeField] private float heightMultiplier=1;
    private readonly float height = 300;
    private float timeThrow;
    private float timeHit;
    public float rotationSpeed = 106.5f;
    private Quaternion startRotation;
    Vector2 startPosition;
    
    

    void Start()

    {
        AimCorrection();
        startRotation = transform.rotation;
        startPosition = transform.position;
        rb2D.velocity = direction * speed;
        rb2D.AddForce(Vector2.up * height*heightMultiplier);
    }

    // Update is called once per frame
    void Update()
    {
        var rotateangle = 0;
        if (bulletDirectionRight)
        {
            rotateangle = 180;
        }
        else
        {
            rotateangle = -180;
        }
        Quaternion wantedRotation = Quaternion.Euler(0, 0, rotateangle);

        // Use RotateTowards to smoothly rotate the object
        transform.rotation = Quaternion.RotateTowards(transform.rotation, wantedRotation, rotationSpeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(aim);
            timeThrow = Time.time;
            transform.position = startPosition;
            transform.rotation = startRotation;
            rb2D.velocity = direction * speed;
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
    public void AimCorrection()
    {
       heightMultiplier = Mathf.Abs(aim.x) / 10;
       //startRotation = new Quaternion(0,0,aim.z,0);
       if(aim.x > 0)
        {
            bulletDirectionRight = false;
        }
       else
        {
            bulletDirectionRight = true;
        }
     }
}
   

  
