using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    Animator enemyAnimator;

    private void Start()
    {
        enemyAnimator = this.GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) 
        {
            enemyAnimator.SetBool("Enemy_damage_0", true);

        }
    }

    public void EnemyTakeDamage()
    {
        Debug.Log("enemy takes damage");
        enemyAnimator.SetBool("Enemy_damage_0", true);
        Invoke(nameof(EnemyIdle), .2f);
    }

    public void EnemyIdle()
    {
        enemyAnimator.SetBool("Enemy_damage_0", false);
    }



}
