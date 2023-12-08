using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseWeaponInputStopper : MonoBehaviour
{
    [SerializeField] bool tutorialLevelStop;

    [HideInInspector] public bool isAbleToUseWeapon;

    private void Start()
    {
        if (tutorialLevelStop)
        {
            isAbleToUseWeapon = false;
        }
    }
}
