using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementFlying : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private float speed;
    [SerializeField] private GameObject AttachedEnemy;
    void Start()
    {
        rigidBody.velocity = Vector2.one*speed;
    }
    
}
