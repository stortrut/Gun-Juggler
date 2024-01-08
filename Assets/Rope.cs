using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FollowPlayer.Instance.trampolineJumping = false;
            Debug.Log("trampoline false");
        }
    }
}
