using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponQueueUI : MonoBehaviour
{
    [SerializeField] GameObject[] weaponsInQueueEnumsOrder;
    [SerializeField] Image arrow;
    List<GameObject> weaponsInQueueDisplayedOrder = new List<GameObject>();

    PlayerJuggle playerJuggleScript;
    Vector3 startPosArrow = new Vector3(60, 31);
    float xOffset = 50f;

    private int i = 0;

    void Awake()
    {
        playerJuggleScript = FindObjectOfType<PlayerJuggle>();

        if (playerJuggleScript == null)
        {
            playerJuggleScript = FindObjectOfType<PlayerJuggle>();
        }
    }
    
    void Start()
    {
        //var rect = anchor.transform as RectTransform;
        //Debug.Log("anchor"+arrow.rectTransform.anchoredPosition); 
        //arrowStartPos = arrow.transform.position;
        if (playerJuggleScript.weaponsCurrentlyInJuggleLoop == null)
        {
            Debug.Log("the juggle loop list is not yet created");
        }


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SortInTheRightOrder();
        }
    }

    public void SortInTheRightOrder()
    {
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
        weaponsInQueueDisplayedOrder.Clear();

        for (int i = 0; i < playerJuggleScript.GetCorrectWeaponOrder().Count; i++)
        {
            WeaponJuggleMovement weaponJuggleMovement = playerJuggleScript.GetCorrectWeaponOrder()[i];
            WeaponType weaponEnum = weaponJuggleMovement.weaponBase.weaponType;


            int enumIndex = (int)weaponEnum;

            GameObject instantiatedPrefab = Instantiate(weaponsInQueueEnumsOrder[enumIndex], Vector3.zero, Quaternion.identity, transform);
            weaponsInQueueDisplayedOrder.Add(instantiatedPrefab);
        }

        i = -1;
        arrow.rectTransform.anchoredPosition = startPosArrow;

    }

    public void InstantiateTheWeapons()
    {
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }

        weaponsInQueueDisplayedOrder.Clear();

        for (int i = 0; i < playerJuggleScript.weaponsCurrentlyInJuggleLoop.Count; i++)
        {
            WeaponJuggleMovement weaponJuggleMovement = playerJuggleScript.weaponsCurrentlyInJuggleLoop[i];
            WeaponType weaponEnum = weaponJuggleMovement.weaponBase.weaponType;
            int enumIndex = (int)weaponEnum;

            GameObject instantiatedPrefab = Instantiate(weaponsInQueueEnumsOrder[enumIndex], Vector3.zero, Quaternion.identity, transform);
            //SetAndStretchToParentSize(instantiatedPrefab.GetComponent<RectTransform>(), gameObject.GetComponent<RectTransform>());
            //instantiatedPrefab.transform.SetParent(transform, true);
            weaponsInQueueDisplayedOrder.Add(instantiatedPrefab);
            //instantiatedPrefab.GetComponent<Image>().rectTransform.localScale = new Vector3(.2f,.2f);
        }


        SortInTheRightOrder();
    }

    //public void SetAndStretchToParentSize(RectTransform rectTransform, RectTransform parentRectTransform)
    //{
    //    rectTransform.anchoredPosition = parentRectTransform.position;
    //    rectTransform.anchorMin = new Vector2(1, 0);
    //    rectTransform.anchorMax = new Vector2(0, 1);
    //    rectTransform.pivot = new Vector2(0.5f, 0.5f);
    //    rectTransform.sizeDelta = parentRectTransform.rect.size;
    //    rectTransform.transform.SetParent(parentRectTransform);
    //}

    public void ReplaceQueueElements(int heartIndex)
    {
        if (!playerJuggleScript.isAlive)
        {
            return;
        }

        WeaponJuggleMovement replacingThisItem = playerJuggleScript.weaponsCurrentlyInJuggleLoop[heartIndex];
        WeaponType weaponEnum = replacingThisItem.weaponBase.weaponType;
        int enumIndex = (int)weaponEnum;

        if (replacingThisItem.weaponBase.isHeart)
        {
            Destroy(weaponsInQueueDisplayedOrder[heartIndex]);
            weaponsInQueueDisplayedOrder.RemoveAt(heartIndex);

            GameObject heartPrefab = Instantiate(weaponsInQueueEnumsOrder[3], Vector3.zero, Quaternion.identity, transform);
            //heartPrefab.transform.localPosition = CalculateNewPosition(heartIndex);
            weaponsInQueueDisplayedOrder.Insert(heartIndex, heartPrefab);
        }

        else if (!replacingThisItem.weaponBase.isHeart)
        {
            Destroy(weaponsInQueueDisplayedOrder[heartIndex]);
            weaponsInQueueDisplayedOrder.RemoveAt(heartIndex);

            GameObject weaponPrefab = Instantiate(weaponsInQueueEnumsOrder[enumIndex], Vector3.zero, Quaternion.identity, transform);
            //weaponPrefab.transform.localPosition = CalculateNewPosition(heartIndex);
            weaponsInQueueDisplayedOrder.Insert(heartIndex, weaponPrefab);
        }
    }

    public void ArrowPositioning()
    {
        if (playerJuggleScript.weaponsCurrentlyInJuggleLoop != null)
        {
            i++;
            if (i < playerJuggleScript.weaponsCurrentlyInJuggleLoop.Count)
            {
                arrow.rectTransform.anchoredPosition = new Vector3(startPosArrow.x + i * xOffset, startPosArrow.y);
                //arrow.rectTransform.localPosition = new Vector3(startPosArrow.x + i * xOffset, startPosArrow.y);
            }
            else
            {
                arrow.rectTransform.anchoredPosition = startPosArrow;
                i = 0;
            }
        }
    }
}
