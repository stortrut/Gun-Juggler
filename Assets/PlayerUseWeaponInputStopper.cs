using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseWeaponInputStopper : MonoBehaviour
{
    [SerializeField] bool tutorialLevelStop;

    [SerializeField] public bool isAbleToUseWeapon;

    private void Start()
    {
        isAbleToUseWeapon = true;

        if (tutorialLevelStop)
        {
            isAbleToUseWeapon = false;
        }
    }


}
