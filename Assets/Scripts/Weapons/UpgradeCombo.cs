using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;


public class UpgradeCombo : MonoBehaviour
{
    public static UpgradeCombo Instance { get; private set; }

    public bool comboActive = true;
    private int _bulletHit;
    private bool hit;
    private bool lastOneHit;
    public bool hitSinceShot;
    public bool onlyOneInWave;
    public float comboTime;
    public float inactiveTime;
    private GameObject comboObject;
    public Tween comboTween;
    private Tween badCombo;
    private Tween flashCombo;
    public List<WeaponJuggleMovement> playerjuggle;
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private GameObject combo;
    [SerializeField] private SpriteRenderer comboImage;
    [SerializeField] private GameObject comboEffect1;
    [SerializeField] private GameObject comboEffect2;

    private HudPopcornFill hudPopcornFill;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        //if (comboActive == false)
        //{
        //    combo.SetActive(false);
        //    enabled = false;
        //}

        hudPopcornFill = FindObjectOfType<HudPopcornFill>();
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
                flashCombo.Kill();
                _bulletHit++;
                comboText.enabled = true;
                comboText.text = _bulletHit.ToString();
                lastOneHit = true;
                comboText.fontStyle = FontStyles.Normal;
                Upgrade();
                comboEffect1.SetActive(true);
                comboEffect2.SetActive(true);
                comboImage.enabled = true;
                flashCombo = transform.DOMoveZ(0, 1.5f).OnComplete(Flash);
                    
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
        if (hudPopcornFill != null)
        {
            //hudPopcornFill.PopcornAmountUpgrade();
        }
        if( _bulletHit != 0 && _bulletHit % 5 == 0 ) 
        {
            comboEffect1.SetActive(false);
            comboEffect2.SetActive(false);
            comboEffect1.SetActive(true);
            comboEffect2.SetActive(true);
            
            foreach ( var weapon in playerjuggle)
            {
                weapon.weaponBase.UpgradeWeapon();
            }
        }
    }

    private void ResetCombo()
    {
        if (hudPopcornFill != null)
        {
            //hudPopcornFill.ReducePopcornAmount();
        }
        comboText.text = "COMBO LOST";
        comboText.enabled = true;
        Invoke(nameof(Flash), 0.5f);
        _bulletHit = 0;

        foreach (var weapon in playerjuggle)
        {
            weapon.weaponBase.ResetWeaponUpgradeLevel();
        }
    }

    private void DestroyCombo()
    {
         badCombo = comboObject.transform.DOMoveZ(transform.position.z, inactiveTime).OnComplete(ResetCombo);
    }

    public IEnumerator Combo()
    {
        if(comboActive == false )
        {
            yield break;  
        }
        comboTween = comboObject.transform.DOMoveZ(transform.position.z, comboTime);
        yield return comboTween.WaitForKill();

        if (hitSinceShot == false)
        {
            OnBulletHit(false);
        }
        else
        {
            OnBulletHit(true);
            badCombo.Kill();
            DestroyCombo();
            {
                //Debug.Log("Combo number should appear");
            }
        }
    }
}

