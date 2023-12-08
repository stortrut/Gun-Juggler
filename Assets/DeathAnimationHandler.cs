using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimationHandler : MonoBehaviour
{
    [SerializeField] Animator body;

    [SerializeField] SpriteRenderer leftArm;
    [SerializeField] SpriteRenderer rightArm;
    [SerializeField] SpriteRenderer legs;
    [SerializeField] SpriteRenderer juggleCatchCircle;

    public void TriggerDeathAnimation()
    {
        body.SetBool("Death", true);

        leftArm.enabled = false;
        rightArm.enabled = false;
        legs.enabled = false;
        juggleCatchCircle.enabled = false;
    }
}
