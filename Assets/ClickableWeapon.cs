using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableWeapon : MonoBehaviour , IPointerDownHandler
{
    public GameObject weapon;
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("YOU CLICKED");
        PlayerJuggle.Instance.StopJuggling();
        PlayerJuggle.Instance.AddWeaponToLoop(weapon);
    }
        


    // Update is called once per frame
    void Update()
    {
        
    }
}
