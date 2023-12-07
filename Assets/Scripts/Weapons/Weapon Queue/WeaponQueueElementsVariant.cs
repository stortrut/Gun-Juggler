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
    private float posGapBetweenQueueObjects = 2.5f;
    [SerializeField] Vector3 firstObjectInQueuePos = new Vector3(-6,-5,0);

    void Start()
    {
        playerJuggleScript = FindObjectOfType<PlayerJuggle>();
        if (playerJuggleScript != null)
        {
            Debug.Log("not null");
        }
        //InstantiateAppropriateQueueElements();
    }

    public void InstantiateAppropriateQueueElements()
    {
        Debug.Log(playerJuggleScript.weaponsCurrentlyInJuggleLoop.Count);
        for (int i = 0; i < playerJuggleScript.weaponsCurrentlyInJuggleLoop.Count; i++)
        {
            WeaponJuggleMovement weaponJuggleMovement = playerJuggleScript.weaponsCurrentlyInJuggleLoop[i];
            WeaponBase.WeaponType weaponEnum = weaponJuggleMovement.weaponBase.weaponType;
            int enumIndex = (int)weaponEnum;

            Vector3 queueObjectSpawnPos = new Vector3(firstObjectInQueuePos.x + posGapBetweenQueueObjects * i, firstObjectInQueuePos.y);
            GameObject instantiatedPrefab = Instantiate(weaponsInQueueEnumsOrder[enumIndex], queueObjectSpawnPos, Quaternion.identity, transform);  //instantiate the right prefab based of the enums index and weapons in queue enums order list
            //Debug.Log("weapons in order "+weaponsInQueueDisplayedOrder[enumIndex]+ queueObjectSpawnPos);
            weaponsInQueueDisplayedOrder.Add(instantiatedPrefab);                    //add the prefab
            Debug.Log(weaponsInQueueDisplayedOrder[enumIndex]);

        }
    }

            //weaponsInQueueDisplayedOrder.Add(newWeaponImage.GetComponent<RectTransform>());

            //Vector3 indexObjectInQueuePos = new Vector3(posGapBetweenQueueObjects * i, 0);
            ////GameObject newSmallGun = Instantiate(smallGun, rectTransform);
            //RectTransform gunTransform = newSmallGun.GetComponent<RectTransform>();
            //gunTransform.anchoredPosition = indexObjectInQueuePos;

            //SetSiblingIndex(int index);

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

    public void ShowNextWeaponInQueueMoving()  //actually moving
    {
        GameObject movingObject = weaponsInQueueDisplayedOrder[0];
        weaponsInQueueDisplayedOrder.RemoveAt(0);
        weaponsInQueueDisplayedOrder.Add(movingObject);

        for (int i = 0; i < weaponsInQueueDisplayedOrder.Count; i++)
        {
            Vector3 position = new Vector3(firstObjectInQueuePos.x + (posGapBetweenQueueObjects * i), firstObjectInQueuePos.y);

            weaponsInQueueDisplayedOrder[i].transform.position = position;
        }
        //WeaponBase.WeaponType weaponEnum = playerJuggleScript.weaponInHand.weaponBase.weaponType;
        //int enumIndex = (int)weaponEnum;        //weapon in hand found by its enum index, the same index as the enumsorder list
        //RectTransform queuedObjectTransform = weaponsInQueueDisplayedOrder[enumIndex].GetComponent<RectTransform>();
        //queuedObjectTransform.SetSiblingIndex(weaponsInQueueDisplayedOrder.Count);
        //queuedObjectTransform.SetAsLastSibling();
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
        if (Input.GetKeyDown(KeyCode.K))
        {
            InstantiateAppropriateQueueElements();
        }
    }
}
//    int enumIndex = (int)weaponEnum;
