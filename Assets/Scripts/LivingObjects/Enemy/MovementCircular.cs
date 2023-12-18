using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class MovementCircular : MonoBehaviour,IStunnable
{   
    [SerializeField] private float frequency;
    [SerializeField] private float amplitude;
    [SerializeField] private GameObject AttachedEnemy;
    private float x;
    private float y;
    private float z;
    private float offSet;

    [HideInInspector] public bool isStunned = false;
    [HideInInspector] public bool timeStop;
    [HideInInspector] public float timeStunned;
    public bool isStunnable { get { return isStunned; } set { isStunned = value; } }
    IEnumerator UnFreeze(float timeStunned)
    {
        yield return new WaitForSeconds(timeStunned);
        isStunnable = false;
        Debug.Log("Unfreeze");
    }


    void Start()
    {
        offSet = transform.position.z;
    }
    

    void Update()
    {
        if (isStunned == true)
        {
            timeStunned += Time.deltaTime;
            return;
        }
        x = Mathf.Cos((Time.time + offSet-timeStunned) * frequency) * amplitude;
        y = Mathf.Sin((Time.time + offSet-timeStunned) * frequency) * amplitude;

        transform.position = new Vector3 (transform.parent.position.x + x , transform.parent.position.y + y,0);
    }   
}   
    