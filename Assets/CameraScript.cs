using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    [SerializeField] private GameObject playerToFollow;
    Vector3 offset;

    private void Awake()
    {
        offset = transform.localPosition;
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 followPos;
        followPos.x = playerToFollow.transform.position.x + offset.x;
        followPos.y = playerToFollow.transform.position.y + offset.y;
        followPos.z = offset.z;

        transform.position = followPos;
    }
}
