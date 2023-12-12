using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerJuggle : MonoBehaviour
{
    [SerializeField] private float timeInBetweenEachThrowAtTheStart;
    [SerializeField] private Animator bodyAnimator;

    public List<WeaponJuggleMovement> weaponsCurrentlyInJuggleLoop = new();

    private bool isJuggling;
   /* [HideInInspector] */public WeaponJuggleMovement weaponInHand;
    [HideInInspector] public ArmAnimationHandler armAnimationHandler;  
    
    WeaponQueueElements weaponQueueElementsScript;

    [HideInInspector] public bool isAlive;

    public void SpeedUpUpcomingWeapon(/*WeaponJuggleMovement oldWeapon*/)
    {
        int weaponPosition = weaponsCurrentlyInJuggleLoop.IndexOf(weaponInHand);

        if (weaponPosition == weaponsCurrentlyInJuggleLoop.Count - 1)
        {
            weaponPosition = 0;
        }
        else
        {
            weaponPosition++;
        }

        weaponsCurrentlyInJuggleLoop[weaponPosition].curveSpeedModifier = 4f;
    }

    private void Start()
    {
        isAlive = true;
        armAnimationHandler = GetComponentInChildren<ArmAnimationHandler>();

        WeaponJuggleMovement[] weaponsOnPlayer = GetComponentsInChildren<WeaponJuggleMovement>();
        foreach (WeaponJuggleMovement weapon in weaponsOnPlayer)
        {
            weaponsCurrentlyInJuggleLoop.Add(weapon);
        }

        int lastWeaponID = weaponsCurrentlyInJuggleLoop.Count - 1;

        weaponInHand = weaponsCurrentlyInJuggleLoop[lastWeaponID];


        StartJuggling();


        //weaponsCurrentlyInJuggleLoop[lastWeaponID].weaponBase.EquipWeapon();

        weaponQueueElementsScript = FindObjectOfType<WeaponQueueElements>();
        if(weaponQueueElementsScript == null) { return; }
        weaponQueueElementsScript.InstantiateAppropriateQueueElements();
    }

    private void Update()
    {
        //if(weaponInHand == null) { return; }
        //if (Input.GetKeyDown(KeyCode.Mouse0) && !isJuggling)
        //{
        //    StartJuggling();
        //}
    }

    private void StartJuggling()
    {
        isJuggling = true;

        //StartCoroutine(nameof(ThrowUpAllWeaponsWithSameInterval), (timeInBetweenEachThrowAtTheStart) / (weaponsCurrentlyInJuggleLoop.Count - 1));


        ThrowUpAllWeapons();
    }


    private void ThrowUpAllWeapons()
    {
        for (int i = 0; i < weaponsCurrentlyInJuggleLoop.Count - 1; i++)
        {
            weaponsCurrentlyInJuggleLoop[i].ThrowUpWeapon();
            StartCoroutine(nameof(DistributeWeaponsInAir));
        }
    }

    IEnumerator DistributeWeaponsInAir()
    {

        //while (CheckDistanceBetweenTwoWeapons)
        //{
        //    
        //}
        yield return new WaitForEndOfFrame();


    }

    private float CheckDistanceBetweenTwoWeapons(int firstWeaponListId, int secondWeaponListId)
    {
        float firstWeaponPos = weaponsCurrentlyInJuggleLoop[firstWeaponListId].curveDeltaTime;
        float secondWeaponPos = weaponsCurrentlyInJuggleLoop[secondWeaponListId].curveDeltaTime;

        float distance = Mathf.Abs(secondWeaponPos - firstWeaponPos);

        return distance;
    }



    //Throws up all weapons except the last one
    IEnumerator ThrowUpAllWeaponsWithSameInterval(float waitTimeBetweenEachThrow)
    {
        for (int i = 0; i < weaponsCurrentlyInJuggleLoop.Count - 1; i++)
        {
            weaponsCurrentlyInJuggleLoop[i].ThrowUpWeapon();
            yield return new WaitForSeconds(waitTimeBetweenEachThrow);
        }
    }

    public void CatchWeapon(WeaponJuggleMovement newWeapon)
    {
        newWeapon.beingThrown = false;
        newWeapon.weaponBase.EquipWeapon();
        
        weaponInHand = newWeapon;
    }

    public void ThrowUpWeaponInHand()
    {
        if(weaponInHand == null) { return; }

        SpeedUpUpcomingWeapon();
        weaponInHand.ThrowUpWeapon();
        weaponInHand.weaponBase.UnEquipWeapon();
        weaponInHand = null;
        if (weaponQueueElementsScript == null) { return; }
        weaponQueueElementsScript.ArrowPositioning();
    }



    public void RemoveWeaponFromLoop(WeaponJuggleMovement weaponToRemoved)
    {
        weaponsCurrentlyInJuggleLoop.Remove(weaponToRemoved);
        if(weaponsCurrentlyInJuggleLoop.Count < 2)
        {
        }
    }


    public void ReplaceRandomWeaponWithHeart()
    {
        for (int i = 0; i < weaponsCurrentlyInJuggleLoop.Count; i++)
        {
            if (!weaponsCurrentlyInJuggleLoop[i].weaponBase.isHeart)
            {
                weaponsCurrentlyInJuggleLoop[i].weaponBase.ReplaceWeaponWithHeart();
                weaponQueueElementsScript.ReplaceQueueElements(i);
                return;
            }
        }
    }


    public void ReplaceRandomHeartWithWeapon()
    {
        for (int i = 0; i < weaponsCurrentlyInJuggleLoop.Count; i++)
        {
            if (weaponsCurrentlyInJuggleLoop[i].weaponBase.isHeart)
            {
                weaponsCurrentlyInJuggleLoop[i].weaponBase.ReplaceHeartWithWeapon();
                weaponQueueElementsScript.ReplaceQueueElements(i);
                return;
            }
        }
    }





    public void DropAllWeaponsOnGround()
    {
        for (int i = 0; i < weaponsCurrentlyInJuggleLoop.Count; i++)
        {
            weaponsCurrentlyInJuggleLoop[i].DropWeapon();
            isAlive = false;
        }
    }

}