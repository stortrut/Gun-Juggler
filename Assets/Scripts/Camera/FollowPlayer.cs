using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject playerToFollow;
    Vector3 offset;

    private void Awake()
    {
        offset = transform.localPosition;
    }

    private void FixedUpdate()
    {
        if (playerToFollow == null) { return; }

        Vector3 followPos;
        followPos.x = playerToFollow.transform.position.x + offset.x;
        followPos.y = playerToFollow.transform.position.y + offset.y;
        followPos.z = offset.z;

        transform.position = followPos;
    }

}
