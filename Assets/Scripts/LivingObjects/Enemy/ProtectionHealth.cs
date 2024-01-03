using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionHealth : MonoBehaviour
{
    private bool hasFunctionBeenCalled = false;
    private EnemyProtection parent;

 
    public void OnDestroy()
    {
        if (gameObject.transform.parent != null)
        {
            parent = GetComponentInParent<EnemyProtection>();
            if(parent != null)
                parent.RemoveProtection(1);
            //}
        }
    }
}
