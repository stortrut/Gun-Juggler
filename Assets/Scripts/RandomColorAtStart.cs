using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColorAtStart : MonoBehaviour
{
    SpriteRenderer spriterenderer;

    private void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();

        Color randomWeaponColor = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f));

        spriterenderer.color = randomWeaponColor;
    }
}
