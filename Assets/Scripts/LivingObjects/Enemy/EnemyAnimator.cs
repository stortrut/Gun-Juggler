using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] Animator enemyAnimator;
    [SerializeField] SpriteRenderer dummySpriterenderer;
    const string ENEMY_IDLE = "enemy_idle";
    const string ENEMY_TAKING_DAMAGE = "enemy_taking_damage";
    const string ENEMY_DYING = "enemy_dying";
    const string ENEMY_ATTACKING = "enemy_attacking";

    private void Start()
    {
        //enemyAnimator = this.GetComponent<Animator>();
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.V)) 
        //{
        //    enemyAnimator.SetBool(ENEMY_TAKING_DAMAGE, true);
        //    Invoke(nameof(EnemyIdle), .2f);
        //}
        
    }

    public void EnemyTakeDamage()
    {
        enemyAnimator.SetBool(ENEMY_TAKING_DAMAGE, true);
        Invoke(nameof(EnemyIdle), .1f);
    }

    public void EnemyDying()
    {
        enemyAnimator.SetBool(ENEMY_DYING, true);
    }

    public void EnemyIdle()
    {
        enemyAnimator.SetBool(ENEMY_TAKING_DAMAGE, false);
    }

}
