using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetableByPlayerAutoAim : MonoBehaviour
{
    [SerializeField] Transform optionalDifferentAimPos;

    public Transform GetAimPos()
    {
        if (optionalDifferentAimPos == null)
        {
            return gameObject.transform;
        }
        else
        {
            return optionalDifferentAimPos;
        }
    }







}
