using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeapons : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("WeaponBeingJuggled"))
        {
            Debug.Log("Picked up weapon");
        }
    }

}
