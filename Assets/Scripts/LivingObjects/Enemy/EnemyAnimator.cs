using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeaponBase;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] Animator enemyAnimator;
    [SerializeField] SpriteRenderer dummySpriterenderer;

    [SerializeField] public EnemyType enemyType;

    const string IDLE = "idle";
    const string TAKING_DAMAGE = "taking_damage";
    const string DYING = "dying";
    const string ATTACK1 = "attack1"; 

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.V)) 
        //{
        //    enemyAnimator.SetBool(ENEMY_TAKING_DAMAGE, true);
        //    Invoke(nameof(EnemyIdle), .2f);
        //}
    }

    public void Idle()
    {
        enemyAnimator.SetBool(TAKING_DAMAGE, false);
    }

    public void TakingDamage()
    {
        enemyAnimator.SetBool(TAKING_DAMAGE, true);
        Invoke(nameof(Idle), .1f);
    }

    public void Dying()
    {
        enemyAnimator.SetBool(DYING, true);
    }

    public void Attacking()
    {
        enemyAnimator.SetBool(ATTACK1, true);
    }

}

public enum EnemyType
{
    Dummy,
    Giraffe,
    PieClown,
}
