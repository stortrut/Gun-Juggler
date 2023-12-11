using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance {  get; private set; }

    private void Awake()
    {
        instance = this; 
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Camera Shake");
            StartCoroutine(ShakingRandomly(1f, .8f, .5f));
        }
    }

    public IEnumerator ShakingRandomly(float duration, float magnitudeX, float magnitudeY)
    {
        Vector3 initialPosition = transform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float newX = initialPosition.x + Random.Range(-1f, 1f) * magnitudeX;
            float newY = initialPosition.y + Random.Range(-1f, 1f) * magnitudeY;

            transform.localPosition = new Vector3(newX, newY, initialPosition.z);

            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        transform.localPosition = initialPosition;
    }
}
