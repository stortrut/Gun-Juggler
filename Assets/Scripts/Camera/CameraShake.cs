using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //public static CameraShake instance {  get; private set; }

    //private Camera cam;
    //private void Awake()
    //{
    //    instance = this; 
    //    cam = GetComponent<Camera>();
    //}
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Camera Shake");
            //ScreenShake();
            //StartCoroutine(ShakingRandomly(0.4f, .4f, .2f, 1));

            StartCoroutine(Shake(0.2f, .1f));
        }
    }

    public void BasicCameraShake()
    {
        StartCoroutine(Shake(0.2f, .1f));

    }


    IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 OriginalPos = transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {

            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, OriginalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = OriginalPos;

    }



    public IEnumerator ShakingRandomly(float duration, float magnitudeX, float magnitudeY, int timesToShake)
    {
        Vector3 initialPosition = transform.localPosition;
        float elapsedTime = 0f;
        bool didJustShake = false;
        float shakeAtThisTime = duration / timesToShake;
        int i = 0;

        while (elapsedTime < duration)
        {
            if ((Time.time > shakeAtThisTime - Time.deltaTime || Time.time < shakeAtThisTime + Time.deltaTime) && didJustShake == false)
            {
                float newX = initialPosition.x + Random.Range(-1f, 1f) * magnitudeX;
                float newY = initialPosition.y + Random.Range(-1f, 1f) * magnitudeY;

                transform.localPosition = new Vector3(newX, newY, initialPosition.z);
                didJustShake = true;
                i++;
                //Debug.Log("camerashake");
            }

            elapsedTime += Time.deltaTime;

            if (Time.time  > ((shakeAtThisTime *i)+ Time.deltaTime*3))
            {
                didJustShake = false;
                //Debug.Log("ska callas tv� ggr?");
            }
            yield return null; 
        }

        transform.localPosition = initialPosition;
    }
    //void ScreenShake()
    //{
    //    float margin = Random.Range(0.0f, 0.3f);
    //    cam.rect = new Rect(margin, 0.0f, 1.0f - margin * 2.0f, 1.0f);
    //}

}
