using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenCollideGround : MonoBehaviour
{
    [SerializeField] GameObject holder;


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("HITTTT");
            Destroy(holder);
        }
    }





}
