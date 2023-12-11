using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    float elapsedTime;
    float magnitudeX;
    float magnitudeY;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            ShakingRandomly(2f, 4, 4);
           
        }
    }

    IEnumerator ShakingRandomly(float duration, float magnitudeX, float magnitudeY)
    {
        Vector2 initialPosition = transform.localPosition;
        elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            transform.position = new Vector3(initialPosition.x + Random.Range(-1,1)* magnitudeX, initialPosition.y + Random.Range(-1, 1) * magnitudeY);   
        }
        transform.localPosition = initialPosition;
        yield return null;
    }
}
