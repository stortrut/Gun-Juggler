using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunJuggleMovement : MonoBehaviour
{

    [SerializeField] AnimationCurve gunThrowAnimationCurveY;
    [SerializeField] AnimationCurve gunThrowAnimationCurveX;
    [SerializeField] private float speed = 100.0f;
    [SerializeField] private float yOffset;

    private float curveDeltaTime = 0.0f;


    private void Start()
    {
        curveDeltaTime += Random.Range(0f, 2f);


        Keyframe[] keys1 = gunThrowAnimationCurveY.keys;
        Keyframe keyFrame = keys1[1];
        keyFrame.value = Random.Range(1f, 4f);
        keys1[1] = keyFrame;
        gunThrowAnimationCurveY.keys = keys1; // This is copying the keys back into the AnimationCurve's array.





    }

    void Update()
    {
        // Get the current position of the sphere
        Vector3 currentPosition = transform.localPosition;
        // Call evaluate on that time   
        curveDeltaTime += Time.deltaTime;

        currentPosition.y = gunThrowAnimationCurveY.Evaluate(curveDeltaTime);
        currentPosition.x = gunThrowAnimationCurveX.Evaluate(curveDeltaTime);


        // Update the current position of the sphere
        transform.localPosition = currentPosition;

    }





}
