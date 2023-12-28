using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class Pie : EnemyBullet
{
    [SerializeField] private float speed;
    [SerializeField] private float heightMultiplier = 1;
    private readonly float height = 900;
    private float timeThrow;
    private float timeHit;
    public float rotationSpeed = 106.5f;
    private Quaternion startRotation;
    Vector3 startPosition;
    private bool once = false;

    void Start()
    {
        AimCorrection();
        if (transform.position.z == 1) { }

        startRotation = transform.rotation;
        startPosition = transform.position;
        transform.position = new Vector3 (startPosition.x, startPosition.y, 0);
        rb2D.velocity = direction * speed;
        rb2D.AddForce(Vector2.up * height * heightMultiplier);
    }

    void Update()
    {
        if (startPosition.z == 1)
        {


            //if((int)transform.position.x < (int)(startPosition.x - aim.x/2))
            //{
            // Calculate the interpolation factor based on the progress
            float progress = Mathf.InverseLerp(startPosition.x, startPosition.x + (aim.x * 0.5f), transform.position.x);

            // Use Mathf.Lerp to smoothly transition the gravityScale
            rb2D.gravityScale = Mathf.Lerp(1.8f, 0.1f, progress);
            //  }
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
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            //timeHit = Time.time;
            //Debug.Log(timeHit - timeThrow);
            Vector2 positionForEffectAnimationScript = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + .3f);
            EffectAnimations.instance.PieExplosionGround(positionForEffectAnimationScript);
            Sound.instance.SoundSet(Sound.instance.pieSplash, 0);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            Vector2 positionForEffectAnimationScript = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            EffectAnimations.instance.PieExplosion(positionForEffectAnimationScript);
            Sound.instance.SoundSet(Sound.instance.pieSplash, 0);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Bullet"))
        {
            Vector2 positionForEffectAnimationScript = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + .5f);
            EffectAnimations.instance.PieExplosion(positionForEffectAnimationScript);
            //Sound.Instance.SoundSet(Sound.Instance.pieSplash, 0);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            Vector2 positionForEffectAnimationScript = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            EffectAnimations.instance.PieExplosion(positionForEffectAnimationScript);
            Sound.instance.SoundSet(Sound.instance.pieSplash, 0);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    public void AimCorrection()
    {
        heightMultiplier = Mathf.Abs(aim.x) / 14;
        //startRotation = new Quaternion(0,0,aim.z,0);
        if (aim.x > 0)
        {
            bulletDirectionRight = false;
        }
        else
        {
            bulletDirectionRight = true;
        }
    }
}



