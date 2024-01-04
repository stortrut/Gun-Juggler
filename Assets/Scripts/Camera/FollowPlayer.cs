using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using System.Linq;
using UnityEngine.SceneManagement;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] bool doNotDoPathAtStart;
    Vector3 basicOffset = new Vector3(5, 5.75f, 31.7999992f);


    public static FollowPlayer Instance;
    [HideInInspector] public PlayerMovement playerToFollow;
    private GameObject player;

    [SerializeField] bool useOffsetUnderneathThis;
    [SerializeField] public Vector3 offset = new Vector3(5, 2, -100);
    [SerializeField] private Vector3[] path;
    Vector3 targetPos = Vector3.zero;
    public bool yAxisLocked = false;
    float followPosSave;
    bool axisShouldStayUnlockedTilItReachesTarget = false;
    [SerializeField] private float smoothnessFactor = 3;
    [ReadOnly] public bool lockOn;
    

    private void Awake()
    {
        Instance = this;
        player = FindObjectOfType<PlayerMovement>()?.gameObject;
        smoothnessFactor = 9;
        //transform.parent = null;
    }

    private void Start()
    {

        if (PlayerPrefs.GetInt("cameraPan") == 0 && !doNotDoPathAtStart)
            transform.DOPath(path, 10, PathType.CatmullRom,PathMode.Sidescroller2D).OnComplete(PathBack);
        else 
        {
            transform.DOKill();
            FindPlayer();
        }
    }
    private void PathBack()
    {
        Vector3[] pathBack = path.Reverse().ToArray();
        transform.DOPath(pathBack, 1, PathType.CatmullRom, PathMode.Sidescroller2D).OnComplete(FindPlayer);
    }
    public void FindPlayer()
    {
        playerToFollow = player.GetComponent<PlayerMovement>();
        var vector = offset;
        StartCoroutine(SmoothCamera(200, vector, true));  

        if (!useOffsetUnderneathThis) ;
            //offset = new Vector3(5, 5.75f, 31.7999992f);
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
    public IEnumerator SmoothCamera(float p, Vector3 vector, bool onPlayer)
    {
        Vector3 target = vector;
        lockOn = false;
        var startpos = transform.position;
        if(onPlayer == true)
        {
            lockOn = true;
            target = vector + playerToFollow.transform.position;
        }
        for (float i = 0; i < p; i++) 
        {
            yield return new WaitForSeconds(200/20000);
            transform.position = Vector3.Lerp(startpos, target, (i+1)/p);
            offset = vector;
        }


       
        
    }

}
