using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using System.Linq;

public class FollowPlayer : MonoBehaviour
{
    [HideInInspector] private PlayerMovement playerToFollow;
    private GameObject player;
    [SerializeField] Vector3 offset = new Vector3(5, 2, -100);
    [SerializeField] private Vector3[] path;
    Vector3 targetPos = Vector3.zero;
    public bool yAxisLocked = false;
    float followPosSave;
    bool axisShouldStayUnlockedTilItReachesTarget = false;
    [SerializeField] private float smoothnessFactor = 3;
    bool lockOn;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>()?.gameObject;
        smoothnessFactor = 9;
        //transform.parent = null;
    }

    private void Start()
    {
        transform.DOPath(path, 10, PathType.CatmullRom,PathMode.Sidescroller2D).OnComplete(PathBack);
    }
    private void PathBack()
    {
        Vector3[] pathBack = path.Reverse().ToArray();
        transform.DOPath(pathBack, 1, PathType.CatmullRom, PathMode.Sidescroller2D).OnComplete(FindPlayer);
    }
    private void FindPlayer()
    {
        playerToFollow = player.GetComponent<PlayerMovement>();
        lockOn = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            transform.DOKill();
            FindPlayer();
        }
        if(lockOn == false || player == null) { return; }
        if (!playerToFollow.onGround)
        {
            if (axisShouldStayUnlockedTilItReachesTarget)
            {
                //Debug.Log("return");
                return;
            }
            yAxisLocked = true;
            //Debug.Log(Mathf.Abs(playerToFollow.transform.position.y - transform.position.y));
            if (Mathf.Abs(playerToFollow.transform.position.y - transform.position.y) > 2.5f)
            {
                yAxisLocked = false;
                axisShouldStayUnlockedTilItReachesTarget = true;
            }
        }
        else if (playerToFollow.onGround)
        {
            yAxisLocked = false;
            axisShouldStayUnlockedTilItReachesTarget = false;
        }
    }
    private void FixedUpdate()
    {
        if (lockOn == false || player == null) { return; }
        targetPos.x = playerToFollow.transform.position.x + offset.x;
        if (!yAxisLocked)
        {
            targetPos.y = playerToFollow.transform.position.y + offset.y;
            followPosSave = transform.position.y;
        }
        else if (yAxisLocked)
        {
            targetPos.y = followPosSave;
        }
        //followPos.y = playerToFollow.transform.position.y + offset.y;
        targetPos.z = offset.z;
        Vector3 posY = Vector3.Lerp(transform.position, targetPos, smoothnessFactor * Time.deltaTime);
        Vector3 posX = new Vector3(playerToFollow.transform.position.x + offset.x, 0, 0);
        transform.position = new Vector3(posX.x, posY.y, offset.z);
        //transform.position = Vector3.Lerp(transform.position, targetPos, smoothnessFactor * Time.deltaTime);
        //transform.position = new Vector3(playerToFollow.transform.position.x + offset.x, 0, 0);
    }
}
