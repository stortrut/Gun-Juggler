using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class FollowPlayer : MonoBehaviour
{
    [HideInInspector] private PlayerMovement playerToFollow;
    private GameObject player;
    Vector3 offset = new Vector3(5, 2, -34);
    Vector3 targetPos = Vector3.zero;
    public bool yAxisLocked = false;
    float followPosSave;
    bool axisShouldStayUnlockedTilItReachesTarget = false;
    [SerializeField] private float smoothnessFactor = 3;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>()?.gameObject;
        if(player != null )
        {
            playerToFollow = player.GetComponent<PlayerMovement>();

        }
        smoothnessFactor = 9;
        //transform.parent = null;
    }

    private void Start()
    {
    }

    private void Update()
    {
        if(player == null) { return; }
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
        if (player == null) { return; }
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
        transform.position = new Vector3(posX.x, posY.y, -34);
        //transform.position = Vector3.Lerp(transform.position, targetPos, smoothnessFactor * Time.deltaTime);
        //transform.position = new Vector3(playerToFollow.transform.position.x + offset.x, 0, 0);
    }
}
