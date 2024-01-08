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
    [ReadOnly] public bool zoomFinished;


    private void Awake()
    {
        Instance = this;
        player = FindObjectOfType<PlayerMovement>()?.gameObject;
        smoothnessFactor = 1.5f;
        //transform.parent = null;
    }

    private void Start()
    {

        if (/*PlayerPrefs.GetInt("cameraPan") == 0 && */!doNotDoPathAtStart)
        {
            transform.DOPath(path, 10, PathType.CatmullRom, PathMode.Sidescroller2D).OnComplete(PathBack);
            FindObjectOfType<PlayerMovement>().turnOffMovement = true;
        }
        else
        {
            transform.DOKill();
            FindPlayer();
        }
    }
    private void PathBack()
    {
        Vector3[] pathBack = path.Reverse().ToArray();
        Sound.Instance.SoundSet(Sound.Instance.windWhoosh, 0,.7f);
        transform.DOPath(pathBack, 1, PathType.CatmullRom, PathMode.Sidescroller2D).OnComplete(() => {
            FindPlayer();
            Lights.Instance.NormalLightsOn();
        }) ;
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
        if (Input.GetKeyDown(KeyCode.C))
        {
            transform.DOKill();
            FindPlayer();
        }
        if (lockOn == false || player == null) { return; }
        if (!playerToFollow.onGround)
        {
            if (axisShouldStayUnlockedTilItReachesTarget)
            {
                //Debug.Log("return");

            }
            else
            {
                yAxisLocked = true;
                targetPos.y = playerToFollow.transform.position.y + offset.y;
                followPosSave = transform.position.y;
                //Debug.Log(Mathf.Abs(playerToFollow.transform.position.y - transform.position.y));
                if (Mathf.Abs(playerToFollow.transform.position.y - transform.position.y) > 2.5f)
                {
                    yAxisLocked = false;
                    targetPos.y = followPosSave;
                    axisShouldStayUnlockedTilItReachesTarget = true;
                }
            }

        }
        else if (playerToFollow.onGround)
        {
            yAxisLocked = false;
            targetPos.y = playerToFollow.transform.position.y + offset.y;
            //targetPos.y = transform.position.y; 
            axisShouldStayUnlockedTilItReachesTarget = false;
        }
        if (trampolineJumping)
        {
            targetPos.y = playerToFollow.transform.position.y + offset.y;
        }
        targetPos.x = playerToFollow.transform.position.x + offset.x;
        targetPos.z = offset.z;
        Vector3 posY = Vector3.Lerp(transform.position, targetPos, smoothnessFactor * Time.deltaTime);
        Vector3 posX = new Vector3(playerToFollow.transform.position.x + offset.x, 0, 0);
        transform.position = new Vector3(targetPos.x, posY.y, offset.z);

       
    }
    public bool trampolineJumping;
    //private void FixedUpdate()
    //{
    //    if (lockOn == false || player == null) { return; }
    //    targetPos.x = playerToFollow.transform.position.x + offset.x;
    //    if (!yAxisLocked)
    //    {
    //        targetPos.y = playerToFollow.transform.position.y + offset.y;
    //        followPosSave = transform.position.y;
    //    }
    //    else if (yAxisLocked)
    //    {
    //        targetPos.y = followPosSave;
    //    }
    //    targetPos.z = offset.z;
    //    Vector3 posY = Vector3.Lerp(transform.position, targetPos, smoothnessFactor * Time.deltaTime);
    //    Vector3 posX = new Vector3(playerToFollow.transform.position.x + offset.x, 0, 0);
    //    transform.position = new Vector3(posX.x, posY.y, offset.z);
    //}
    public IEnumerator SmoothCamera(float p, Vector3 vector, bool onPlayer)
    {
        Vector3 target = vector;
        lockOn = false;
        var startpos = transform.position;
        if (onPlayer == true)
        {
            target = vector + playerToFollow.transform.position;
        }
        for (float i = 0; i < p; i++)
        {
            yield return new WaitForSeconds(200 / 20000);
            transform.position = Vector3.Lerp(startpos, target, (i + 1) / p);
            offset = vector;

            if (i == p - 1)
            {
                FindObjectOfType<PlayerMovement>().turnOffMovement = false;
            }

        }
        if (onPlayer == true)
        {
            lockOn = true;
        }

    }

}
