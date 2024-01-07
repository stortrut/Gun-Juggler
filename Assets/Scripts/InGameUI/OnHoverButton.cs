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
    public bool isUltButton = false;
    private void Start()
    {
        startScale = transform.localScale.x;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isUltButton)
        {
            theText.color = new Color(144 / 255f, 27 / 255f, 32 / 255f);  //Or however you do your color
            selectionSpotlight.SetActive(true);
            Sound.Instance.SoundSet(Sound.Instance.spotLightOn, 1, 1f, .1f);
        }

        transform.DOScale(scaleAmount, 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isUltButton)
        {
            theText.color = new Color(0.168f, 0.098f, 0.070f); //Or however you do your color
            selectionSpotlight.SetActive(false);
        }
        
        transform.DOScale(startScale, 0.5f);    
    }
    private void OnDisable()
    {
        transform.localScale = Vector3.one;
    }

}

