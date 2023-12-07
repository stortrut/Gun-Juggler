using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerToFollow;
    Vector3 offset = new Vector3(1, 2, -34);

    public bool yAxisLocked = false;
    float followPosSave;

    private void Awake()
    {
        playerToFollow = FindObjectOfType<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        if (playerToFollow.isJumping)
        {
            yAxisLocked = true;
            Debug.Log(yAxisLocked);

        }
        else if (!playerToFollow.isJumping)
        {
            yAxisLocked = false;
            Debug.Log(yAxisLocked);
        }
        if (playerToFollow == null) { return; }

        Vector3 followPos = Vector3.zero; 

        followPos.x = playerToFollow.transform.position.x + offset.x;
        if (!yAxisLocked)
        {
            followPos.y = playerToFollow.transform.position.y + offset.y;
            followPosSave = followPos.y;
        }
        else if (yAxisLocked)
        {
            followPos.y = followPosSave;
        }
        //followPos.y = playerToFollow.transform.position.y + offset.y;
        followPos.z = offset.z;

        transform.position = followPos;
    }

    public void StopCameraFollowInYAxis()
    {
        yAxisLocked = true;
        Debug.Log(yAxisLocked);
    }
    public void AllowCameraFollowInYAxis()
    {
        yAxisLocked = false;
        Debug.Log(yAxisLocked);
    }
}
