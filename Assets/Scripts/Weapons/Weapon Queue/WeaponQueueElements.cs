using System.Collections.Generic;
using UnityEngine;

public class WeaponQueueElements : MonoBehaviour
{
    [SerializeField] GameObject[] weaponsInQueueEnumsOrder;
    [SerializeField] GameObject arrow;
    List<GameObject> weaponsInQueueDisplayedOrder = new List<GameObject>();

    PlayerJuggle playerJuggleScript;
    private float firstQueueObjectPos;
    private float posGapBetweenQueueObjects = 2.5f;
    [SerializeField] Vector3 firstObjectInQueuePos = new Vector3(-6, -5, 0);

    protected int i = 0;
    void Start()
    {
        playerJuggleScript = FindObjectOfType<PlayerJuggle>();
        //InstantiateAppropriateQueueElements();
        Transform childTransform = GetComponent<Transform>().GetChild(0);
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
        ///
        //ShowNextWeaponInQueueMoving();
        arrow.GetComponent<Transform>().position = weaponsInQueueDisplayedOrder[i].GetComponent<Transform>().GetChild(0).GetComponent<Transform>().position;
        Vector3 pos = new Vector3(0, 1, 0);
        arrow.GetComponent<Transform>().position += pos;
        ///
    }

    public void ShowNextWeaponInQueueMoving()
    {
        Debug.Log(i);
        arrow.GetComponent<Transform>().position = weaponsInQueueDisplayedOrder[i].GetComponent<Transform>().GetChild(0).GetComponent<Transform>().position;
        Vector3 pos = new Vector3(0, 1, 0);
        arrow.GetComponent<Transform>().position += pos;
        i++;
        if (i >= playerJuggleScript.weaponsCurrentlyInJuggleLoop.Count)
        {
            i = 0;
        }
    }

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
