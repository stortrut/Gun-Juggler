using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class HudPopcornFill : MonoBehaviour
{
    private Image popcornFillImage;
    public Image popcornFillTop;
    private UpgradeCombo comboScript;

    public Vector2 popcornFillTopPos = new Vector2(0, 0);
    private Vector2 popcornFillImagePos = new Vector2(0, 0);

    [SerializeField] private float popcornFillAmountPerUpgrade = 2;

    [SerializeField] float ultDuration;

    private bool ultActive = false;


    void Start()
    {
        comboScript = FindObjectOfType<UpgradeCombo>();
        popcornFillImage = transform.GetChild(0).GetComponent<Image>();
        popcornFillTop = transform.GetChild(1).GetComponent<Image>();

        popcornFillTopPos.y = 0f;
        popcornFillTop.rectTransform.anchoredPosition = popcornFillTopPos;
        popcornFillImage.rectTransform.anchoredPosition = popcornFillImagePos;
        popcornFillImage.fillAmount = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            PopcornAmountUpgrade();
        }
    }

    void FixedUpdate()
    {
        float clampedYValue = Mathf.Clamp(popcornFillTopPos.y, 0, 19);          
        popcornFillTopPos = new Vector2(0, clampedYValue);
        popcornFillTop.rectTransform.anchoredPosition = popcornFillTopPos;          //set filltop to filltopPos

        if (comboScript != null)
        {
            //if ((popcornFillTopPos.y >= 12) || (popcornFillTopPos.y <= -12))
            //{
            //    return;
            //}
            //popcornFillTop.rectTransform.anchoredPosition = popcornTopPos;
        }
    }

    public void PopcornAmountUpgrade()
    {
        popcornFillTopPos.y += popcornFillAmountPerUpgrade;      

        popcornFillImage.fillAmount += 1 / (19 / popcornFillAmountPerUpgrade);

        EffectAnimations.instance.PopcornPopping(popcornFillTopPos);

        if (popcornFillTopPos.y > 19)
        {
            EffectAnimations.instance.PopcornPoppingUltReady(popcornFillTopPos);
            StartCoroutine(nameof(StartUlt));
            popcornFillTopPos.y -= popcornFillAmountPerUpgrade;
            popcornFillImage.fillAmount -= 1 / (19 / popcornFillAmountPerUpgrade);
        }
    }

    public void ReducePopcornAmount()
    {
        popcornFillTopPos.y = 0;
        popcornFillImage.fillAmount = 0;
    }

    public IEnumerator StartUlt()
    {
        if (ultActive)
            yield break;

        ultActive = true;
        float startTime = Time.time;
        FindObjectOfType<PlayerJuggle>().Ultimate();

        Sound.instance.SoundRandomized(Sound.instance.equipNewWeapon);


        while (Time.time < startTime + ultDuration)
        {
            yield return null;
        }

        if (Time.time >= ultDuration)
            ReducePopcornAmount();
    }


    // popcorn fillage is based on combo/streak

    //private Image popcornFillImage;
    //private Image popcornFillTop;
    //private UpgradeCombo comboScript;

    //private Vector2 popcornFillTopPos = new Vector2(0, 0);
    //private Vector2 popcornFillImagePos = new Vector2(0, 0);

    //private Ult ult;

    //[SerializeField] private float popcornFillAmountPerUpgrade = 2;

    //void Start()
    //{
    //    comboScript = FindObjectOfType<UpgradeCombo>();
    //    ult = FindObjectOfType<Ult>();
    //    popcornFillImage = transform.GetChild(0).GetComponent<Image>();
    //    popcornFillTop = transform.GetChild(1).GetComponent<Image>();

    //    popcornFillTopPos.y = -12f;
    //    popcornFillTop.rectTransform.anchoredPosition = popcornFillTopPos;
    //    popcornFillImage.rectTransform.anchoredPosition = popcornFillImagePos;
    //    popcornFillImage.fillAmount = 0;
    //}

    //void FixedUpdate()
    //{
    //    if (comboScript != null)
    //    {
    //        float clampedYValue = Mathf.Clamp(popcornFillImagePos.y, -12, 12);
    //        Vector2 popcornTopPos = new Vector2(clampedYValue, 0);
    //        popcornFillTop.rectTransform.anchoredPosition = popcornFillTopPos;


    //        //if ((popcornFillTopPos.y >= 12) || (popcornFillTopPos.y <= -12))
    //        //{
    //        //    return;
    //        //}
    //        //popcornFillTop.rectTransform.anchoredPosition = popcornTopPos;


    //    }
    //}

    //public void PopcornAmountUpgrade()
    //{
    //    popcornFillTopPos.y += popcornFillAmountPerUpgrade;       // value = 2, fillamount = 1/ 24/value

    //    popcornFillImage.fillAmount += 1 / (24 / popcornFillAmountPerUpgrade);

    //    if (popcornFillTopPos.y > 12)
    //    {
    //        StartCoroutine(nameof(Ult));
    //        popcornFillTopPos.y -= popcornFillAmountPerUpgrade;
    //        popcornFillImage.fillAmount -= 1 / (24 / popcornFillAmountPerUpgrade);
    //    }
    //}

    //public void ReducePopcornAmount()
    //{
    //    popcornFillTopPos.y = -12;
    //    popcornFillImage.fillAmount = 0;
    //}
}
