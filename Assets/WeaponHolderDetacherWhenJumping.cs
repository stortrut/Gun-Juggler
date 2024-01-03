using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolderDetacherWhenJumping : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;


    [SerializeField] Transform weaponHolder;

    [SerializeField] Transform wheelArtHolderThatHoldsWeaponHolder;
    [SerializeField] Transform notMovingWeaponHolder;


    private Vector2 basePositionWhenNotJumping;


    private void Start()
    {
        basePositionWhenNotJumping = weaponHolder.transform.localPosition;
    }



    private void Update()
    {
        if (playerMovement.rigidBody.velocity.y > 0)
        {
            weaponHolder.SetParent(notMovingWeaponHolder);
        }
        else
        {
            weaponHolder.SetParent(wheelArtHolderThatHoldsWeaponHolder);
            weaponHolder.transform.localPosition = basePositionWhenNotJumping;
        }
    }

}
