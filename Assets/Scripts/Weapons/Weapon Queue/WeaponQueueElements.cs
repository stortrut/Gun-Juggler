using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class WeaponQueueElements : MonoBehaviour
{
    [SerializeField] GameObject[] weaponsInQueueEnumsOrder;
    //[SerializeField] GameObject arrow;
    [SerializeField] GameObject queueBackground;
    List<GameObject> weaponsInQueueDisplayedOrder = new List<GameObject>();

    PlayerJuggle playerJuggleScript;
    private float posGapBetweenQueueObjects = 2.5f;
    Vector3 firstObjectInQueuePos = new Vector3(-3.5f, 2.6f, 0);
    private Vector3 zOffset = new Vector3(0, 0, 33);
    Vector3 arrowPos;

    protected int i = 0;
    void Start()
    {
        //transform.localPosition += zOffset;
        playerJuggleScript = FindObjectOfType<PlayerJuggle>();
        //InstantiateAppropriateQueueElements();
        //queueBackground.transform.position += zOffset;

        Invoke(nameof(InstantiateAppropriateQueueElements), .01f);
        //instantiatedPrefab.GetComponent<Transform>().position += zOffset;
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
            //instantiatedPrefab.GetComponent<Transform>().position += zOffset;
            weaponsInQueueDisplayedOrder.Add(instantiatedPrefab);                    //add the prefab
            //Debug.Log(weaponsInQueueDisplayedOrder[enumIndex]);
        }
        Vector3 pos = new Vector3(firstObjectInQueuePos.x + posGapBetweenQueueObjects, firstObjectInQueuePos.y);
        Instantiate(queueBackground, pos, Quaternion.identity, transform);
        //arrow.transform.position = new Vector3(firstObjectInQueuePos.x, firstObjectInQueuePos.y + 1.5f, firstObjectInQueuePos.z);
        ///
        //ShowNextWeaponInQueueMoving();
        //arrow.GetComponent<Transform>().position = weaponsInQueueDisplayedOrder[i].GetComponent<Transform>().GetChild(0).GetComponent<Transform>().position;
        //Vector3 pos = new Vector3(0, 1, 34);
        //arrow.GetComponent<Transform>().position += pos;
        ///
    }

    //public void ShowNextWeaponInQueueMoving()
    //{
    //    i++;
    //    Debug.Log(i);
    //    //arrow.GetComponent<Transform>().position = weaponsInQueueDisplayedOrder[i].GetComponent<Transform>().GetChild(0).GetComponent<Transform>().position;
    //    //arrow.GetComponent<Transform>().position = new Vector3(arrowPos.x + posGapBetweenQueueObjects *i, arrowPos.y, arrowPos.z); 
    //    if (i == 0)
    //    {
    //        Debug.Log("minus");
    //        arrow.transform.position += new Vector3(-playerJuggleScript.weaponsCurrentlyInJuggleLoop.Count * posGapBetweenQueueObjects, 0);
    //    }
    //    else if (i != 0)
    //    {
    //        arrow.transform.position += new Vector3(posGapBetweenQueueObjects * i, 0);
    //    }


    //    if (i >= playerJuggleScript.weaponsCurrentlyInJuggleLoop.Count)
    //    {
    //        i = -1;
    //    }
    //}

    //public void InstantiateAppropriateQueueElements()
    //{
    //    int loopLength = playerJuggleScript.weaponsCurrentlyInJuggleLoop.Count;
    //    //int posOffsetMultiplier = 0;

    //    for (int i = 0; i < loopLength; i++)
    //    {
    //        WeaponJuggleMovement weaponJuggleMovement = playerJuggleScript.weaponsCurrentlyInJuggleLoop[i];
    //        WeaponBase.WeaponType weaponEnum = weaponJuggleMovement.weaponBase.weaponType;      

    //        // Get the RectTransform component for positioning UI elements
    //        RectTransform rectTransform = GetComponent<RectTransform>();
    //        int enumIndex = (int)weaponEnum;

    //        if (weaponEnum == WeaponBase.WeaponType.SmallGun)
    //        {
    //            Vector3 indexObjectInQueuePos = new Vector3(posGapBetweenQueueObjects * i, 0);
    //            GameObject newSmallGun = Instantiate(smallGun, rectTransform);
    //            RectTransform smallGunTransform = newSmallGun.GetComponent<RectTransform>();
    //            smallGunTransform.anchoredPosition = indexObjectInQueuePos;
    //            //posOffsetMultiplier++;
    //        }
    //        else if (weaponEnum == WeaponBase.WeaponType.ShotGun)
    //        {
    //            Vector3 firstObjectInQueuePos = new Vector3(posGapBetweenQueueObjects * i, 0);
    //            GameObject newShotGun = Instantiate(shotGun, rectTransform);
    //            RectTransform shotGunTransform = newShotGun.GetComponent<RectTransform>();
    //            shotGunTransform.anchoredPosition = firstObjectInQueuePos;
    //            //posOffsetMultiplier++;
    //        }
    //        else if (weaponEnum == WeaponBase.WeaponType.StunGun)
    //        {
    //            Vector3 firstObjectInQueuePos = new Vector3(posGapBetweenQueueObjects * i, 0);
    //            GameObject newStunGun = Instantiate(stunGun, rectTransform);
    //            RectTransform stunGunTransform = newStunGun.GetComponent<RectTransform>();
    //            stunGunTransform.anchoredPosition = firstObjectInQueuePos;
    //            //posOffsetMultiplier++;
    //        }
    //    }
    //}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            InstantiateAppropriateQueueElements();
        }
    }
}
