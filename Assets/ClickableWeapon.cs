using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableWeapon : MonoBehaviour, IPointerDownHandler
{
    public GameObject weapon;

    public void OnPointerDown(PointerEventData eventData)
    {
        var player = FindObjectOfType<PlayerJuggle>();


        if (player.weaponsCurrentlyInJuggleLoop.Count > 1)
        {
            Debug.Log("Player Had No Weapons So we added one because the option was chosen");
            Debug.Log("waepons in loop" + player.weaponsCurrentlyInJuggleLoop.Count);
            for (int i = 0; i < 3; i++)
            {
                Debug.Log("Clickalbe" + i);
                player.weaponsCurrentlyInJuggleLoop[0].DropWeapon();
            }
            player.CreateAndAddWeaponToLoop(weapon);
        }
        else
        {
            player.ReplaceAllWeaponsWithAnotherWeapon(weapon);
        }



        Sound.Instance.SoundRandomized(Sound.Instance.equipNewWeapon);

        // Destroy(gameObject);
    }



    // Update is called once per frame
    void Update()
    {

    }
}
