using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeCombo : MonoBehaviour
{
    
    private static int _bulletHit;
    private bool hit;
    [SerializeField] private static TextMeshProUGUI comboText;

    public static void OnBulletHit(bool didItHit)
    {
       
        {
            if (_bulletHit > 0 && didItHit == true)
            {
                comboText.text = _bulletHit.ToString();
            }
        }
            //AgeChanged(this, EventArgs.Empty);
    }

    }

