using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Camera))]
public class CameraShakeRobert : MonoBehaviour
{
    public static CameraShakeRobert instance {  get; private set; }

    // Based on this video: https://www.youtube.com/watch?v=tu-Qe66AvtY

    [Range(0f, 1f)]
    public float trauma;

    [Range(2, 3)]
    public int powerOf = 2;         //if we use trauma^2 or trauma^3

    public float scale = 64;        //agressiveness of the perlin noice, perlin noice is so that camerashake works with slowmo.

    public float rotation = 10;     //Camera shake rotation
    public float translation = 2;   //Camera shake translation, don't use in 3d.
    public bool debug;


    Transform parent;               //Our parent
    bool orthographic;              //true for 3D camera, false for 2D
    bool shaking;                   //is the camera shaking right now

    [SerializeField] Camera camera;

    void Start()
    {
        //If we don't have a parent, we create one.
        if (transform.parent == null)
        {
            var CameraHolder = new GameObject("CameraHolder");
            CameraHolder.transform.position = transform.position;
            CameraHolder.transform.rotation = transform.rotation;
            parent = CameraHolder.transform;
            transform.parent = parent;
        }
        else
        {
            parent = transform.parent;
        }

        //if (!camera.orthographic)
        //{
        //    translation = 0;    //we dont want to move a 3d camera.
        //}
    }


    void Update()
    {
        if (debug)
        {
            //Some Debug keys for testing.
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                AddTrauma(1f);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                AddTrauma(0.5f);
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                AddTrauma(0.2f);
            }

            //Slowmo test
            if (Input.GetKeyDown(KeyCode.S))
            {
                Time.timeScale = 0.2f;
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                Time.timeScale = 1f;
            }
        }
    }


    public void AddTrauma(float newTrauma)
    {
        trauma += newTrauma;
        trauma = Mathf.Clamp01(trauma); //make sure we dont go over max

        if (!shaking)                   //Start shaking if we are not already doing it.
        {
            StartCoroutine(NewShake());
        }
    }


    IEnumerator NewShake()
    {
        shaking = true;

        while (trauma > 0)
        {
            Shake();
            trauma -= Time.deltaTime;
            yield return null;
        }

        shaking = false;
    }


    public void Shake()
    {
        float shake = Mathf.Pow(trauma, powerOf);

        float angle = rotation * shake * (Mathf.PerlinNoise(0, Time.time * scale) * 2 - 1); //*2 -1 to make sure we are between -1 and +1

        /* for 3D, rotation
		 * 
		 * Basic version
		float yaw = maxYaw * shake * Random.Range(-1f, 1f);
		float pitch = maxPitch * shake * Random.Range(-1f, 1f);
		float roll = maxRoll * shake * Random.Range(-1f, 1f);
		 * 
		 * Perlin version x = seed

		float yaw = maxYaw * shake * Mathf.PerlinNoise(0, Time.time);
		float pitch = maxPitch * shake * Mathf.PerlinNoise(1, Time.time);
		float roll = maxRoll * shake * Mathf.PerlinNoise(2, Time.time);
		float z = translation * shake * Mathf.PerlinNoise(3, Time.time);
		*/

        float x = translation * shake * (Mathf.PerlinNoise(1, Time.time * scale) * 2 - 1);
        float y = translation * shake * (Mathf.PerlinNoise(2, Time.time * scale) * 2 - 1);

        transform.position = parent.position + new Vector3(x, y, 0);
        transform.localEulerAngles = parent.localEulerAngles + new Vector3(0, 0, angle);
    }
}

