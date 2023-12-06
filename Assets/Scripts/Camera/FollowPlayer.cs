using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject playerToFollow;
    Vector3 offset;

    public bool YAxisLocked = false;
    float followPosSave;

    private void Awake()
    {
        offset = transform.localPosition;
    }

    private void FixedUpdate()
    {
        if (playerToFollow == null) { return; }

        Vector3 followPos;
        followPos.x = playerToFollow.transform.position.x + offset.x;
        //if (!YAxisLocked)
        //{
        //    followPos.y = playerToFollow.transform.position.y + offset.y;
        //    followPosSave = followPos.y;
        //}
        //else if (YAxisLocked)
        //{
        //    followPos.y = followPosSave;
        //}
        followPos.y = playerToFollow.transform.position.y + offset.y;
        followPos.z = offset.z;

        transform.position = followPos;
    }


    public void StopCameraFollowInYAxis()
    {

    }
}
