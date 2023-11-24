using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuggleCatchCircle : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Color canCatchWeaponColor;
    [SerializeField] Color waitForWeaponColor;


    private void Start()
    {
        spriteRenderer.color = waitForWeaponColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    public void WeaponEnteredCatchZone()
    {
        spriteRenderer.color = canCatchWeaponColor;
    }
}
