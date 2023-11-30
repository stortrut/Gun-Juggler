using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAnimationHandler : MonoBehaviour
{
    [SerializeField] Animator gunCatcherArm;
    [SerializeField] Animator gunHolderArm;


    public IEnumerator PlayThrowUpWeaponAnimation()
    {
        gunHolderArm.SetBool("ThrowUp", true);
        yield return new WaitForEndOfFrame();
        gunHolderArm.SetBool("ThrowUp", false);
    }
    public IEnumerator PlayCatchWeaponAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        gunCatcherArm.SetBool("Catch", true);
        yield return new WaitForEndOfFrame();
        gunCatcherArm.SetBool("Catch", false);
    }




}
