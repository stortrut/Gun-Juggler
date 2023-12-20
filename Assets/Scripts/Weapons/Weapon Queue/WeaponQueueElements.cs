using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

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

    private int i = 0;

    void Awake()
    {
        playerJuggleScript = FindObjectOfType<PlayerJuggle>();
    }

    public void InstantiateAppropriateQueueElements()
    {
        Vector3 pos = new Vector3(firstObjectInQueuePos.x + xOffset + posGapBetweenQueueObjects, firstObjectInQueuePos.y, firstObjectInQueuePos.z);
        GameObject instantiatedBackground = Instantiate(queueBackground, pos, Quaternion.identity, transform);
        instantiatedBackground.transform.localPosition = pos;
        ArrowPositioning();
        //i--;
        InstantiateTheWeapons();
    }

    public void InstantiateTheWeapons()
    {
        for (int i = 0; i < playerJuggleScript.weaponsCurrentlyInJuggleLoop.Count; i++)
        {
            WeaponJuggleMovement weaponJuggleMovement = playerJuggleScript.weaponsCurrentlyInJuggleLoop[i];
            WeaponType weaponEnum = weaponJuggleMovement.weaponBase.weaponType;
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
            heartPrefab.transform.localPosition = CalculateNewPosition(heartIndex); 
            weaponsInQueueDisplayedOrder.Insert(heartIndex, heartPrefab);
        }

        else if (!replacingThisItem.weaponBase.isHeart)
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
        if (playerJuggleScript == null)
        {
            playerJuggleScript = FindObjectOfType<PlayerJuggle>();
        }
        if (playerJuggleScript.weaponsCurrentlyInJuggleLoop == null)
        {
           Debug.Log("the juggle loop list is not yet created");
        }
        if (playerJuggleScript.weaponsCurrentlyInJuggleLoop != null)
        {
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

    //public Ease ..;
    //Vector3 startPos;
    //start
    //startPos = Transform.pos;
    //upd
    //transform.position = startPos;
    //transform.DOMoveX(7,2).SetEase(currentEase);
    //Transform.DOPunchScale(Vector3.one ...) DOScale: pulsera setease(Ease.InOutSine).setloops() antal loops: -1 = sluta aldrig, 
    //i OnDisable(): transform.DOKill(); stäng av alla tween som körs på detta objektet
    //går efter ordningen de skapades inte hur de ligger i listan, 
    //var buttons = findobjoftypes button
    // i = 0
    //foreach item get component recttransform().DOPunchScale().SetDelay(i);
    //i+= 0.05

    //OnPointerEnter(PointerEventData eventData), (:IPointerEnterHandler, IPointerExitHandler
    //transform.getchild(0).DOScale
    //transform.Getchild.doscale().SetLoops(-1, LoopType.Yoyo);
    //Transform.DOLocalmoveX(14,0.25)
    //GetAdditionalCompilerArguments image.docolor(Color.red,1)
    //Oncomplete() ropas på när den är klar
    //OnPointerExit(--)
    //transform.getchild.dokill
    //transform.DOlocalmove(0,0.25
    //_
    //transform.DOSCale(vector.one,5).OnComplete(Stop) egengjord timer, ändrar inte skalan   .SetDelay(5)
    //using DG.Tweening;
    //(dofade)
    //.Onstepcomplete(step) callas varje frame, justera grejer ex slowmotion? Step = egen void
    //getsetter floats
    //transform.DORotate(new Vector3(0,0,360),2, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.OutBounce);
    //OnUpdate körs varje frame
    //-
}
