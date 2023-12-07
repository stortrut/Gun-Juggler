//using System.Collections.Generic;
//using UnityEngine;
//using static UnityEditor.PlayerSettings;

//public class WeaponQueueElements : MonoBehaviour
//{
//    [SerializeField] GameObject[] weaponsInQueueEnumsOrder;
//    [SerializeField] GameObject arrow;
//    [SerializeField] GameObject queueBackground;
//    List<GameObject> weaponsInQueueDisplayedOrder = new List<GameObject>();

//    PlayerJuggle playerJuggleScript;
//    private float posGapBetweenQueueObjects = 2.5f;
//    [SerializeField] Vector3 firstObjectInQueuePos = new Vector3(-6f, -5f, 34);
//    Vector3 arrowPos;
//    float xOffset = -3.54f;

//    protected int i = 0;
//    void Start()
//    {
//        //transform.localPosition += zOffset;
//        playerJuggleScript = FindObjectOfType<PlayerJuggle>();
//        //InstantiateAppropriateQueueElements();
//        //queueBackground.transform.position += zOffset;

//        Invoke(nameof(InstantiateAppropriateQueueElements), .01f);
//        //instantiatedPrefab.GetComponent<Transform>().position += zOffset;
//    }
//    private void Update()
//    {
//    }

//    public void InstantiateAppropriateQueueElements()
//    {
//        for (int i = 0; i < playerJuggleScript.weaponsCurrentlyInJuggleLoop.Count; i++)
//        {
//            WeaponJuggleMovement weaponJuggleMovement = playerJuggleScript.weaponsCurrentlyInJuggleLoop[i];
//            WeaponBase.WeaponType weaponEnum = weaponJuggleMovement.weaponBase.weaponType;
//            int enumIndex = (int)weaponEnum;

//            Vector3 queueObjectSpawnPos = new Vector3(firstObjectInQueuePos.x + posGapBetweenQueueObjects * i, firstObjectInQueuePos.y);
//            GameObject instantiatedPrefab = Instantiate(weaponsInQueueEnumsOrder[enumIndex], queueObjectSpawnPos, Quaternion.identity, transform);  //instantiate the right prefab based of the enums index and weapons in queue enums order list
//            weaponsInQueueDisplayedOrder.Add(instantiatedPrefab);                    //add the prefab
//        }
//        Vector3 pos = new Vector3(firstObjectInQueuePos.x + posGapBetweenQueueObjects, firstObjectInQueuePos.y);
//        Instantiate(queueBackground, pos, Quaternion.identity, transform);
//        //arrow.transform.SetParent(transform, transform.parent);
//        ArrowPositioning();
//        //arrow.transform.localPosition = new Vector3(firstObjectInQueuePos.x, firstObjectInQueuePos.y + 1.3f, firstObjectInQueuePos.z); 
//        //arrow.transform.localPosition = new Vector3(-10.2f, -4.2f, 0);
//        ///
//        //ShowNextWeaponInQueueMoving();
//        //arrow.GetComponent<Transform>().localPosition = weaponsInQueueDisplayedOrder[0].GetComponent<Transform>().GetChild(0).GetComponent<Transform>().position;
//        //Vector3 pos = new Vector3(0, 1, 34);
//        //arrow.GetComponent<Transform>().position += pos;
//        ///
//    }

//    public void ArrowPositioning()
//    {
//        if (i < playerJuggleScript.weaponsCurrentlyInJuggleLoop.Count)
//        {
//            arrow.transform.localPosition = new Vector3(xOffset +firstObjectInQueuePos.x + posGapBetweenQueueObjects * i, firstObjectInQueuePos.y + 1.3f, firstObjectInQueuePos.z);
//        }
//        else
//        {
//            arrow.transform.localPosition = new Vector3(xOffset+firstObjectInQueuePos.x, firstObjectInQueuePos.y + 1.3f, firstObjectInQueuePos.z);
//            i = 0; 
//        }
//        i++;
//    }


//    //public void ShowNextWeaponInQueueMoving()
//    //{
//    //    i++;
//    //    Debug.Log(i);
//    //    //arrow.GetComponent<Transform>().position = weaponsInQueueDisplayedOrder[i].GetComponent<Transform>().GetChild(0).GetComponent<Transform>().position;
//    //    //arrow.GetComponent<Transform>().position = new Vector3(arrowPos.x + posGapBetweenQueueObjects *i, arrowPos.y, arrowPos.z); 
//    //    if (i == 0)
//    //    {
//    //        Debug.Log("minus");
//    //        arrow.transform.position += new Vector3(-playerJuggleScript.weaponsCurrentlyInJuggleLoop.Count * posGapBetweenQueueObjects, 0);
//    //    }
//    //    else if (i != 0)
//    //    {
//    //        arrow.transform.position += new Vector3(posGapBetweenQueueObjects * i, 0);
//    //    }


//    //    if (i >= playerJuggleScript.weaponsCurrentlyInJuggleLoop.Count)
//    //    {
//    //        i = -1;
//    //    }
//    //}

//    //public void InstantiateAppropriateQueueElements()
//    //{
//    //    int loopLength = playerJuggleScript.weaponsCurrentlyInJuggleLoop.Count;
//    //    //int posOffsetMultiplier = 0;

//    //    for (int i = 0; i < loopLength; i++)
//    //    {
//    //        WeaponJuggleMovement weaponJuggleMovement = playerJuggleScript.weaponsCurrentlyInJuggleLoop[i];
//    //        WeaponBase.WeaponType weaponEnum = weaponJuggleMovement.weaponBase.weaponType;      

//    //        // Get the RectTransform component for positioning UI elements
//    //        RectTransform rectTransform = GetComponent<RectTransform>();
//    //        int enumIndex = (int)weaponEnum;

//    //        if (weaponEnum == WeaponBase.WeaponType.SmallGun)
//    //        {
//    //            Vector3 indexObjectInQueuePos = new Vector3(posGapBetweenQueueObjects * i, 0);
//    //            GameObject newSmallGun = Instantiate(smallGun, rectTransform);
//    //            RectTransform smallGunTransform = newSmallGun.GetComponent<RectTransform>();
//    //            smallGunTransform.anchoredPosition = indexObjectInQueuePos;
//    //            //posOffsetMultiplier++;
//    //        }
//    //        else if (weaponEnum == WeaponBase.WeaponType.ShotGun)
//    //        {
//    //            Vector3 firstObjectInQueuePos = new Vector3(posGapBetweenQueueObjects * i, 0);
//    //            GameObject newShotGun = Instantiate(shotGun, rectTransform);
//    //            RectTransform shotGunTransform = newShotGun.GetComponent<RectTransform>();
//    //            shotGunTransform.anchoredPosition = firstObjectInQueuePos;
//    //            //posOffsetMultiplier++;
//    //        }
//    //        else if (weaponEnum == WeaponBase.WeaponType.StunGun)
//    //        {
//    //            Vector3 firstObjectInQueuePos = new Vector3(posGapBetweenQueueObjects * i, 0);
//    //            GameObject newStunGun = Instantiate(stunGun, rectTransform);
//    //            RectTransform stunGunTransform = newStunGun.GetComponent<RectTransform>();
//    //            stunGunTransform.anchoredPosition = firstObjectInQueuePos;
//    //            //posOffsetMultiplier++;
//    //        }
//    //    }
//    //}

//    //void Update()
//    //{
//    //    if (Input.GetKeyDown(KeyCode.O))
//    //    {
//    //        InstantiateAppropriateQueueElements();
//    //    }
//    //}
//}
