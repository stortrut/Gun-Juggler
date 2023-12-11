using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class WeaponQueueElements : MonoBehaviour
{
    [SerializeField] GameObject[] weaponsInQueueEnumsOrder;
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject queueBackground;
    List<GameObject> weaponsInQueueDisplayedOrder = new List<GameObject>();

    PlayerJuggle playerJuggleScript;

    private float posGapBetweenQueueObjects = 2.5f;
    [SerializeField] Vector3 firstObjectInQueuePos = new Vector3(-6f, -5f, 34);
    float xOffset = -3.54f;
    float yOffset = 1f;       //1.3

    protected int i = 0;

    void Start()
    {
        playerJuggleScript = FindObjectOfType<PlayerJuggle>();
    }
    private void Update()
    {
        
    }

    public void InstantiateAppropriateQueueElements()
    {
        Vector3 pos = new Vector3(firstObjectInQueuePos.x + xOffset + posGapBetweenQueueObjects, firstObjectInQueuePos.y, firstObjectInQueuePos.z);
        GameObject instantiatedBackground = Instantiate(queueBackground, pos, Quaternion.identity, transform);
        instantiatedBackground.transform.localPosition = pos;
        ArrowPositioning();
        i--;
        InstantiateTheWeapons();
    }

    public void InstantiateTheWeapons()
    {
        for (int i = 0; i < playerJuggleScript.weaponsCurrentlyInJuggleLoop.Count; i++)
        {
            WeaponJuggleMovement weaponJuggleMovement = playerJuggleScript.weaponsCurrentlyInJuggleLoop[i];
            WeaponBase.WeaponType weaponEnum = weaponJuggleMovement.weaponBase.weaponType;
            int enumIndex = (int)weaponEnum;

            GameObject instantiatedPrefab = Instantiate(weaponsInQueueEnumsOrder[enumIndex], Vector3.zero, Quaternion.identity, transform);  
            instantiatedPrefab.transform.localPosition = CalculateNewPosition(i);
            weaponsInQueueDisplayedOrder.Add(instantiatedPrefab);  
        }
    }

    Vector3 CalculateNewPosition(int index)
    {
        Vector3 queueObjectSpawnPos = new Vector3(firstObjectInQueuePos.x + xOffset + posGapBetweenQueueObjects * index, firstObjectInQueuePos.y, firstObjectInQueuePos.z);
        return queueObjectSpawnPos;
    }


    public void ReplaceQueueElements(int heartIndex)
    {
        WeaponJuggleMovement weaponJuggleMovement = playerJuggleScript.weaponsCurrentlyInJuggleLoop[heartIndex];       
        WeaponBase.WeaponType weaponEnum = weaponJuggleMovement.weaponBase.weaponType;
        int enumIndex = (int)weaponEnum;

        if (weaponJuggleMovement.weaponBase.isHeart)
        {
            Destroy(weaponsInQueueDisplayedOrder[heartIndex]); 
            weaponsInQueueDisplayedOrder.RemoveAt(heartIndex);

            GameObject heartPrefab = Instantiate(weaponsInQueueEnumsOrder[3], Vector3.zero, Quaternion.identity, transform); 
            heartPrefab.transform.localPosition = CalculateNewPosition(heartIndex); 
            weaponsInQueueDisplayedOrder.Insert(heartIndex, heartPrefab);
        }
        else if (!weaponJuggleMovement.weaponBase.isHeart)
        {
            Destroy(weaponsInQueueDisplayedOrder[heartIndex]);
            weaponsInQueueDisplayedOrder.RemoveAt(heartIndex);

            GameObject weaponPrefab = Instantiate(weaponsInQueueEnumsOrder[enumIndex], Vector3.zero, Quaternion.identity, transform);
            weaponPrefab.transform.localPosition = CalculateNewPosition(heartIndex);
            weaponsInQueueDisplayedOrder.Insert(heartIndex, weaponPrefab);
        }
    }

    public void ArrowPositioning()
    {
        if (playerJuggleScript.weaponsCurrentlyInJuggleLoop != null)
        {
            Debug.Log("notnull");
        }
        
        if (i < playerJuggleScript.weaponsCurrentlyInJuggleLoop.Count)
        {
            arrow.transform.localPosition = new Vector3(xOffset + firstObjectInQueuePos.x + posGapBetweenQueueObjects * i, firstObjectInQueuePos.y + yOffset, firstObjectInQueuePos.z);
        }
        else
        {
            arrow.transform.localPosition = new Vector3(xOffset + firstObjectInQueuePos.x, firstObjectInQueuePos.y + yOffset, firstObjectInQueuePos.z);
            i = 0;
        }
        i++;
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
}
