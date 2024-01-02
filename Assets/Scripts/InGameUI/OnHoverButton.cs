using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class OnHoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject selectionSpotlight;
    public TextMeshProUGUI theText;
    float startScale;
    [SerializeField] float scaleAmount = 1.2f;
    private void Start()
    {
        startScale = transform.localScale.x;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        theText.color = new Color(144/255f, 27/255f, 32/255f);  //Or however you do your color

       
        //transform.DOScale(new Vector3(scale, scale), .3f).SetEase(Ease.OutBack);
        selectionSpotlight.SetActive(true);
        Sound.instance.SoundSet(Sound.instance.spotLightOn, 0, .9f);
        transform.DOScale(scaleAmount, 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        theText.color = new Color(0.168f, 0.098f, 0.070f); //Or however you do your color

        selectionSpotlight.SetActive(false);
        Sound.instance.SoundSet(Sound.instance.spotLightOn, 1, .4f);
        transform.DOScale(startScale, 0.5f);
        float scale = startScale;
        //transform.DOScale(new Vector3(scale, scale), .3f).SetEase(Ease.OutBack);
    }
   
}
    

