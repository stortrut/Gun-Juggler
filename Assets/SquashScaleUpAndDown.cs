using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquashScaleUpAndDown : MonoBehaviour
{


    public float squahsSpeed = 1.0f; // Speed of the floating movement
    public float squahsHeight = 0.5f; // Height of the floating
    public float damping = 1.0f; // Damping effect

    private Vector3 startScale;

    void Start()
    {
        startScale = transform.localScale;

        squahsSpeed = squahsSpeed += Random.Range(squahsSpeed - 0.1f, squahsSpeed + 0.25f);
    }

    void Update()
    {
        // Calculate the floating effect using Mathf.Sin for a smooth up and down motion
        float newY = startScale.y + Mathf.Sin(Time.time * squahsSpeed) * squahsHeight;

        // Apply damping to smooth the movement
        float smoothY = Mathf.Lerp(transform.localScale.y, newY, Time.deltaTime * damping);

        // Update the object's position
        transform.localScale = new Vector3(transform.localScale.x, smoothY, transform.localScale.z);
    }



}
