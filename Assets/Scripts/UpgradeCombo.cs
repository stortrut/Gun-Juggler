using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;


public class UpgradeCombo : MonoBehaviour
{

    public static UpgradeCombo Instance { get; private set; }

    private int _bulletHit;
    private bool hit;
    private bool lastOneHit;
    public bool hitSinceShot;
    public bool onlyOneInWave;
    private float duration;
    private GameObject comboObject;
    public Tween comboTween;
    public List<WeaponJuggleMovement> playerjuggle;
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private GameObject comboStuff;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        comboObject = gameObject;
        _bulletHit = 0;
        comboText = GetComponentInChildren<TextMeshProUGUI>();
        comboStuff.SetActive(false);
    }
    public void OnBulletHit(bool didItHit)
    {
        {
            if (didItHit)
            {
                _bulletHit++;
                comboText.text =_bulletHit.ToString();
                lastOneHit = true;
                comboText.fontStyle = FontStyles.Normal;
                Upgrade();
                comboStuff.SetActive(true);
            }
            else if (lastOneHit)
            {
                comboText.fontStyle = FontStyles.Italic;
                lastOneHit = false;
                comboStuff.SetActive(true);
            }
            else
            {
                comboText.text = "";
                _bulletHit = 0;
                comboStuff.SetActive(false);
            }
        }
    }
    private  void Upgrade() 
    
    {
        
        if( _bulletHit != 0 && _bulletHit % 10 == 0 ) 
        {
            foreach( var weapon in playerjuggle)
            {
                weapon.weaponBase.UpgradeWeapon();
            }
            
        }

    }

    public IEnumerator DestroyCombo(float comboTime)
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

