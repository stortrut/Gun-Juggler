using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{

    float aimDirection;
    /*[HideInInspector] */public Quaternion bulletRotation;
    private Vector3 mousePosition;
    private Vector3 currentAim;
    [SerializeField] private PlayerJuggle playerJuggle;
    private Texture2D cursorImage;
    [SerializeField] private Camera activeCamera;

    private void Start()
    {
        //playerJuggle = FindObjectOfType<PlayerJuggle>();
        //Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.ForceSoftware);
    }
    private void Update()
    {


        if (playerJuggle.weaponInHand != null)
        {
            Vector3 playerjuggle = activeCamera.WorldToScreenPoint(playerJuggle.weaponInHand.weaponBase.gunPoint.transform.position);
            currentAim = Input.mousePosition - playerjuggle;
            currentAim.z = 0;
            if (currentAim.sqrMagnitude < 10000)
            {
                return;
            }
            aimDirection = Mathf.Atan2(currentAim.y, currentAim.x) * Mathf.Rad2Deg;
            bulletRotation = Quaternion.Euler(0, 0, aimDirection);
        }
    }
}


