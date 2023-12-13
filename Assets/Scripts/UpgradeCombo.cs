using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using static DG.Tweening.DOTweenCYInstruction;

public class UpgradeCombo : MonoBehaviour
{
    
    private static int _bulletHit;
    private bool hit;
    private static bool lastOneHit;
    public static bool hitSinceShot;
    public static bool onlyOneInWave;
    private float duration;
    private static GameObject comboObject;
    public static Tween comboTween;
    public static List<WeaponJuggleMovement> playerjuggle;
    [SerializeField] private static TextMeshProUGUI comboText;

    private void Start()
    {
        comboObject = gameObject;
        _bulletHit = 0;
        comboText = GetComponent<TextMeshProUGUI>();
    }
    public static void OnBulletHit(bool didItHit)
    {
        {
            if (didItHit)
            {
                _bulletHit++;
                comboText.text = "Combo "+_bulletHit.ToString();
                lastOneHit = true;
                comboText.fontStyle = FontStyles.Normal;
                Upgrade();
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
    private static void Upgrade() 
    
    {
        
        if( _bulletHit != 0 && _bulletHit % 10 == 0 ) 
        {
            foreach( var weapon in playerjuggle)
            {
                weapon.weaponBase.UpgradeWeapon();
            }
            
        }

    }

    public static IEnumerator DestroyCombo(float comboTime)
    {
        comboTween = comboObject.transform.DOMoveZ(0, comboTime);
        yield return comboTween.WaitForKill();

        if (hitSinceShot == false)
        {
            OnBulletHit(false);
            
        }
        else
        {
          OnBulletHit(true);
            Debug.Log("Combo number should appear");
        }

       
    }
}

