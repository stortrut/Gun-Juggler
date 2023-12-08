using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    Image image;
    float alpha = 0f;
    bool fadingToBlack = true;
    void Start()
    {
        image = GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
    }

    // Update is called once per frame
    void Update()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
        if (fadingToBlack)
        {
            PanelFadeToBlack();
        }
        else if (!fadingToBlack)
        {
            PanelFadeBack();
        }
    }

    void PanelFadeToBlack()
    {
        alpha += 0.05f;
        if (alpha > 1f)
        {
            Invoke(nameof(SetFadingToBlackToFalse), .4f);
        }
    }

    void PanelFadeBack()
    {
        alpha -= 0.05f;
    }

    void SetFadingToBlackToFalse()
    {
        fadingToBlack = false;
    }
}
