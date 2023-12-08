using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePlayerAbleToUseWeaponCollider : MonoBehaviour
{

    [SerializeField] bool setWeaponUseToTrue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerUseWeaponInputStopper>().isAbleToUseWeapon = setWeaponUseToTrue;
        }
    }


}
