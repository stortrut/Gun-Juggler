using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CurtainMovement : MonoBehaviour
{
    public Rigidbody2D curtainLeftRigidbody;
    public Rigidbody2D curtainRightRigidbody;
    public float tweenSpeed;
    
    void Start()
    {
        
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            PlayerPrefs.DeleteAll();

            if (Input.GetKeyDown(KeyCode.S)) 
        {
            curtainLeftRigidbody.DOMoveX(curtainLeftRigidbody.transform.position.x - 23, tweenSpeed);
            curtainRightRigidbody.DOMoveX(curtainRightRigidbody.transform.position.x + 23 , tweenSpeed).OnComplete(DisableCurtain);
        }
       
    }
    private void DisableCurtain()
    {

        //gameObject.SetActive(false);
    }
}
