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
    private Tween badCombo;
    public List<WeaponJuggleMovement> playerjuggle;
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private SpriteRenderer comboImage;
    [SerializeField] private GameObject comboEffect1;
    [SerializeField] private GameObject comboEffect2;


    private void Start()
    {
        if (Instance == null)
            Instance = this;
        comboObject = gameObject;
        _bulletHit = 0;
        comboText = GetComponentInChildren<TextMeshProUGUI>();
        comboEffect1.SetActive(false);
        comboEffect2.SetActive(false);
        comboImage.enabled = false;
    }
    public void OnBulletHit(bool didItHit)
    {
        {
            if (didItHit)
            {
                _bulletHit++;
                comboText.enabled = true;
                comboText.text =_bulletHit.ToString();
                lastOneHit = true;
                comboText.fontStyle = FontStyles.Normal;
                Upgrade();
                comboEffect1.SetActive(true);
                comboEffect2.SetActive(true);
                comboImage.enabled = true;
                transform.DOMoveZ(0, 0.7f).OnComplete(Flash);
                    
            }
            else if (lastOneHit)
            {
                comboText.fontStyle = FontStyles.Italic;
                lastOneHit = false;
                comboEffect1.SetActive(true);
                comboEffect2.SetActive(true);
                comboImage.enabled = true;
            }
            else
            {
                comboText.text = "";
                _bulletHit = 0;
                comboEffect1.SetActive(false);
                comboEffect2.SetActive(false);
                comboImage.enabled = false;
            }
        }
    }
    private void Flash()
    {
        comboEffect1.SetActive(false);
        comboEffect2.SetActive(false);
        comboText.enabled = false;
        comboImage.enabled = false;
    }
    private  void Upgrade() 
    
    {
        
        if( _bulletHit != 0 && _bulletHit % 10 == 0 ) 
        {
            comboEffect1.SetActive(false);
            comboEffect2.SetActive(false);
            comboEffect1.SetActive(true);
            comboEffect2.SetActive(true);
            comboImage.color = UnityEngine.Random.ColorHSV(); 
            foreach ( var weapon in playerjuggle)
            {
                weapon.weaponBase.UpgradeWeapon();
            }
            
        }

    }
    private void ResetCombo()
    {
        comboText.enabled = true;
        comboText.text = "COMBO LOST";
        _bulletHit = 0;
    }
    private void DestroyCombo(float comboTime)
    {
         badCombo = comboObject.transform.DOMoveZ(0, comboTime).OnComplete(ResetCombo);
            

    }

    public IEnumerator Combo(float comboTime)
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
            badCombo.Kill();
            DestroyCombo(2);
            {
                Debug.Log("Combo number should appear");
            }


        }
    }
}
