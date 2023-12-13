using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCanFlyManager : MonoBehaviour
{
    [SerializeField] bool canFly;
    [SerializeField] Rigidbody2D rb2D;

    private void Start()
    {
        if (canFly)
        {
            rb2D.bodyType = RigidbodyType2D.Kinematic;
        }
    }


}
