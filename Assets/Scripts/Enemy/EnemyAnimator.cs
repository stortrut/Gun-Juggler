using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : EnemyHealth
{
    [SerializeField] Animator enemyAnimator;

    private void Start()
    {
        //enemyAnimator = GetComponent<Animator>();
    }

    void Update()
    {
    }

    //public void EnemyTakeDamage() { 
    //    : base .EnemyTakeDamage()

    //    enemyAnimator.SetBool("damage_0", true);
    //}
}
