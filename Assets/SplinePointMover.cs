using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SplinePointMover : MonoBehaviour
{
    public SpriteShapeController splineController; // Reference to the Sprite Shape Controller
    public float yOffset = 3.0f; // Amplitude of up and down movement

    private List<Vector3> originalPointPositions = new();

    void Start()
    {
        // Store the original position of the point in the spline

        for (int i = 0; i < splineController.spline.GetPointCount(); i++)
        {
            originalPointPositions.Add(splineController.spline.GetPosition(i));
        }

        //originalPointPosition = splineController.spline.GetPosition(pointIndex);
    }

    void Update()
    {
        // Make the point move up and down
        float yOffsetValue = Mathf.Sin(Time.time) * yOffset;

        for (int i = 0; i < splineController.spline.GetPointCount(); i+=2)
        {
            Vector3 newPosition = originalPointPositions[i] + Vector3.up * yOffsetValue;

            // Modify the position of the point in the spline
            splineController.spline.SetPosition(i, newPosition);
        }

        for (int i = 1; i < splineController.spline.GetPointCount(); i += 2)
        {
            Vector3 newPosition = originalPointPositions[i] + Vector3.up * -yOffsetValue;

            // Modify the position of the point in the spline
            splineController.spline.SetPosition(i, newPosition);
        }
    }

}
