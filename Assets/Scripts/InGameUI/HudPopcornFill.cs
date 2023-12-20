using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class HudPopcornFill : MonoBehaviour
{
    private Image popcornFillImage;
    private Image popcornFillTop;
    private UpgradeCombo comboScript;

    private Vector2 popcornFillTopPos = new Vector2(0, 0);
    private Vector2 popcornFillImagePos = new Vector2(0, 0);

    void Start()
    {
        comboScript = FindObjectOfType<UpgradeCombo>();
        popcornFillImage = transform.GetChild(0).GetComponent<Image>();
        popcornFillTop = transform.GetChild(1).GetComponent<Image>();

        popcornFillTopPos.y = -12f;
        popcornFillTop.rectTransform.anchoredPosition = popcornFillTopPos;
        popcornFillImage.rectTransform.anchoredPosition = popcornFillImagePos;
    }

    void FixedUpdate()
    {
        if (comboScript != null)
        {
            float clampedYValue = Mathf.Clamp(popcornFillImagePos.y, -12, 12);
            Vector2 popcornTopPos = new Vector2(clampedYValue, 0);
            popcornFillTop.rectTransform.anchoredPosition = popcornFillTopPos;


            //if ((popcornFillTopPos.y >= 12) || (popcornFillTopPos.y <= -12))
            //{
            //    return;
            //}
            //popcornFillTop.rectTransform.anchoredPosition = popcornTopPos;


        }
    }

    public void PopcornAmountUpgrade()
    {
        popcornFillTopPos.y += 2;
    }

    public void ReducePopcornAmount()
    {
        popcornFillTopPos.y = -12;
    }
}