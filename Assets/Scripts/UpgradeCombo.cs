using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeCombo : MonoBehaviour
{
    
    private static int _bulletHit;
    private bool hit;
    private static bool lastOneHit;
    public static bool hitSinceShot;
    public static bool onlyOneInWave;
    [SerializeField] private static TextMeshProUGUI comboText;

    private void Start()
    {
        _bulletHit = 0;
        comboText = GetComponent<TextMeshProUGUI>();
    }
    public static void OnBulletHit(bool didItHit)
    {
       if(comboText == null) { return; }
        {
            if (didItHit)
            {
                _bulletHit++;
                comboText.text = "Combo "+_bulletHit.ToString();
                lastOneHit = true;
                comboText.fontStyle = FontStyles.Normal;
            }
            else if (lastOneHit)
            {
                comboText.fontStyle = FontStyles.Italic;
                lastOneHit = false;
            }
            else
            {
                comboText.text = "";
                _bulletHit = 0;
            }
        }
    }
 
           
    
    public static IEnumerator DestroyCombo()
    {
        yield return new WaitForSeconds(0.5f);
        if(hitSinceShot == true)
        {
            OnBulletHit(true);
            Debug.Log("Combo number should appear");
        }
        else
        {
            OnBulletHit(false);
        }

       
    }
}

