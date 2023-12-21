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
    Vector3 startPosArrow = new Vector3(70, 36);
    float xOffset = 58f;
    Vector3 arrowStartPos;

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

    public void InstantiateTheWeapons()
    {
        for (int i = 0; i < playerJuggleScript.weaponsCurrentlyInJuggleLoop.Count; i++)
        {
            WeaponJuggleMovement weaponJuggleMovement = playerJuggleScript.weaponsCurrentlyInJuggleLoop[i];
            WeaponType weaponEnum = weaponJuggleMovement.weaponBase.weaponType;
            int enumIndex = (int)weaponEnum;

            GameObject instantiatedPrefab = Instantiate(weaponsInQueueEnumsOrder[enumIndex], Vector3.zero, Quaternion.identity);
            instantiatedPrefab.transform.SetParent(transform, true);
            weaponsInQueueDisplayedOrder.Add(instantiatedPrefab);
        }
    }

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
