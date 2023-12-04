using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyCollision : MonoBehaviour
{
    [SerializeField] private Knockback knockBackScript;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            knockBackScript.KnockBackMyself(-100, 3, 1, other.transform.position);
        }
    }
}
