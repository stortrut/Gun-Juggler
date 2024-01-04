    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatUpAndDown : MonoBehaviour
{
    public float floatSpeed = 1.0f; // Speed of the floating movement
    public float floatHeight = 0.5f; // Height of the floating
    public float damping = 1.0f; // Damping effect

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;

        floatSpeed = floatSpeed += Random.Range(floatSpeed - 0.2f, floatSpeed + 0.2f);
    }

    void Update()
    {
        // Calculate the floating effect using Mathf.Sin for a smooth up and down motion
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        // Apply damping to smooth the movement
        float smoothY = Mathf.Lerp(transform.position.y, newY, Time.deltaTime * damping);

        // Update the object's position
        transform.position = new Vector3(transform.position.x, smoothY, transform.position.z);
    }



}
