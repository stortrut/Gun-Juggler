using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class OnHoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public TextMeshProUGUI theText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        theText.color = new Color(144/255f, 27/255f, 32/255f);  //Or however you do your color
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        theText.color = new Color(0.168f, 0.098f, 0.070f); //Or however you do your color
    }
}
    

