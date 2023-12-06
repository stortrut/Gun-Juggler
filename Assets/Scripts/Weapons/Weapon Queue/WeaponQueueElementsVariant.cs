using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponQueueElementsVariant : MonoBehaviour
{
    [SerializeField] GameObject[] weaponsInQueueEnumsOrder;
    List<GameObject> weaponsInQueueDisplayedOrder = new List<GameObject>();

    PlayerJuggle playerJuggleScript;
    private float firstQueueObjectPos;
    private float posGapBetweenQueueObjects = 4;
    public Vector3 firstObjectInQueuePos;

    void Start()
    {
        playerJuggleScript = FindObjectOfType<PlayerJuggle>();
        InstantiateAppropriateQueueElements();
    }

    public void InstantiateAppropriateQueueElements()
    {
        for (int i = 0; i < playerJuggleScript.weaponsCurrentlyInJuggleLoop.Count; i++)
        {
            WeaponJuggleMovement weaponJuggleMovement = playerJuggleScript.weaponsCurrentlyInJuggleLoop[i];
            WeaponBase.WeaponType weaponEnum = weaponJuggleMovement.weaponBase.weaponType;
            int enumIndex = (int)weaponEnum;

            Vector3 queueObjectSpawnPos = new Vector3(firstObjectInQueuePos.x + posGapBetweenQueueObjects * i, firstObjectInQueuePos.y);
            Instantiate(weaponsInQueueEnumsOrder[enumIndex], queueObjectSpawnPos, Quaternion.identity, transform);
            Debug.Log(weaponsInQueueDisplayedOrder[enumIndex]);
            weaponsInQueueDisplayedOrder.Insert(i, weaponsInQueueDisplayedOrder[enumIndex]);

            //weaponsInQueueDisplayedOrder.Add(newWeaponImage.GetComponent<RectTransform>());

            //Vector3 indexObjectInQueuePos = new Vector3(posGapBetweenQueueObjects * i, 0);
            ////GameObject newSmallGun = Instantiate(smallGun, rectTransform);
            //RectTransform gunTransform = newSmallGun.GetComponent<RectTransform>();
            //gunTransform.anchoredPosition = indexObjectInQueuePos;

            //SetSiblingIndex(int index);
        }
    }


    void MoveQueueObjects()
    {
        GameObject movingObject = weaponsInQueueDisplayedOrder[0];
        weaponsInQueueDisplayedOrder.RemoveAt(0);
        weaponsInQueueDisplayedOrder.Add(movingObject);
        for (int i = 0; i < weaponsInQueueDisplayedOrder.Count; i++)
        {
            Vector3 position = new Vector3(firstObjectInQueuePos.x * i, 0);
            weaponsInQueueDisplayedOrder[i].transform.position = position;
        }
    }

    public void ShowNextWeaponInQueueMoving()
    {
        WeaponBase.WeaponType weaponEnum = playerJuggleScript.weaponInHand.weaponBase.weaponType;
        int enumIndex = (int)weaponEnum;        //weapon in hand found by its enum index, the same index as the enumsorder list
        Debug.Log("enum"+weaponEnum);
        Debug.Log("index"+enumIndex);
        RectTransform queuedObjectTransform = weaponsInQueueDisplayedOrder[enumIndex].GetComponent<RectTransform>();
        //queuedObjectTransform.SetSiblingIndex(weaponsInQueueDisplayedOrder.Count);
        queuedObjectTransform.SetAsLastSibling();
        Debug.Log("transform"+queuedObjectTransform);
    }

    public void ShowNextWeaponInQueueArrow()
    {
        
    }

    //gameObject.transform.GetChild(0);

    //void MoveQueueObjects()     //based off their index and transform sibling position
    //{
    //    WeaponBase.WeaponType weaponEnum = playerJuggleScript.weaponInHand.weaponBase.weaponType;
    //    int enumIndex = (int)weaponEnum;
    //    Transform queuedObjectTransform = weaponsInQueueDisplayedOrder[enumIndex].transform;
    //    queuedObjectTransform.SetSiblingIndex(weaponsInQueueDisplayedOrder.Count);


    //    for (int i = 0; i < playerJuggleScript.weaponsCurrentlyInJuggleLoop.Count; i++)
    //    {
    //        SetSiblingIndex(int index);
    //    }
    //    SetSiblingIndex(int index);
    //    GameObject movingObject = weaponsInQueueDisplayedOrder[0];
    //    weaponsInQueueDisplayedOrder.RemoveAt(0);
    //    weaponsInQueueDisplayedOrder.Add(movingObject);
    //    for (int i = 0; i < weaponsInQueueDisplayedOrder.Count; i++)
    //    {
    //        Vector3 position = new Vector3(firstObjectInQueuePos.x * i, 0);
    //        weaponsInQueueDisplayedOrder[i].transform.position = position;
    //    }
    //}



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            ShowNextWeaponInQueueMoving();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            InstantiateAppropriateQueueElements();
        }
    }
}
//    int enumIndex = (int)weaponEnum;
