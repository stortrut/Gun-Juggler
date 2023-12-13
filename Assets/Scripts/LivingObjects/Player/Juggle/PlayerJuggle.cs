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

    public void SpeedUpUpcomingWeapon()
    {
        int weaponPosition = weaponsCurrentlyInJuggleLoop.IndexOf(weaponInHand);

        if (weaponPosition == (weaponsCurrentlyInJuggleLoop.Count - 1))
        {
            Debug.Log("Bingo ");

            weaponPosition = 0;
        }
        else
        {
            Debug.Log("Bongo ");

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

        //weaponInHand = weaponsCurrentlyInJuggleLoop[lastWeaponID];


        StartJuggling();


        //weaponsCurrentlyInJuggleLoop[lastWeaponID].weaponBase.EquipWeapon();

        weaponQueueElementsScript = FindObjectOfType<WeaponQueueElements>();
        if(weaponQueueElementsScript == null) { return; }
        weaponQueueElementsScript.InstantiateAppropriateQueueElements();
    }


    private bool spreadOutWeaponsInStart;
    public float timeUntilNextWeapon;
    public string nextWeapon;
    public float timeBetweenWeapons;

    private void Update()
    {
        //if(weaponInHand == null) { return; }
        //if (Input.GetKeyDown(KeyCode.Mouse0) && !isJuggling)
        //{
        //    StartJuggling();
        //}

        if (!isJuggling) { return; }
        //nextWeapon = GetUpcomingWeapon().gameObject.name;
        //timeUntilNextWeapon = GetUpcomingWeapon().GetTimeUntilWeaponIsInHand();
        timeBetweenWeapons = CheckTimeBetweenTwoWeapons(0, 1);

        int lastWeaponId = weaponsCurrentlyInJuggleLoop.Count - 1;
        int weaponBeforeLastId = lastWeaponId - 1;
        float minDistanceBetweenWeapons = 1f;



        for (int i = 0; i < weaponsCurrentlyInJuggleLoop.Count; i++)
        {
            if (!weaponsCurrentlyInJuggleLoop[i].weaponBase.weaponEquipped && !(weaponsCurrentlyInJuggleLoop[i] == GetUpcomingWeapon()))
            {
                int idOfWeaponBeforeThisWeapon;

                if (i == 0)
                {
                    idOfWeaponBeforeThisWeapon = weaponsCurrentlyInJuggleLoop.Count - 1;
                }
                else
                {
                    idOfWeaponBeforeThisWeapon = i - 1;
                }

                if (CheckTimeBetweenTwoWeapons(i, idOfWeaponBeforeThisWeapon) < 2)
                {
                    weaponsCurrentlyInJuggleLoop[i].curveSpeedModifier -= 0.3f * Time.deltaTime;
                }
                else
                {
                    weaponsCurrentlyInJuggleLoop[i].curveSpeedModifier = 1f;
                }
            }
            else if (weaponsCurrentlyInJuggleLoop[i] == GetUpcomingWeapon() && weaponInHand == null)
            {
                weaponsCurrentlyInJuggleLoop[i].curveSpeedModifier = 3.85f;
            }
        }



        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ThrowUpWeaponInHand();
        }

        //if(CheckTimeBetweenTwoWeapons(0, 1) < 2)
        //{
        //    weaponsCurrentlyInJuggleLoop[lastWeaponId].curveSpeedModifier -= 0.3f * Time.deltaTime;
        //}
        //else
        //{
        //    weaponsCurrentlyInJuggleLoop[lastWeaponId].curveSpeedModifier = 1f;
        //}



        //if (CheckDistanceBetweenTwoWeapons(lastWeaponId, weaponBeforeLastId) < minDistanceBetweenWeapons)
        //{
        //    weaponsCurrentlyInJuggleLoop[weaponBeforeLastId].curveSpeedModifier += 0.2f * Time.deltaTime;
        //}
        //else
        //{
        //    weaponsCurrentlyInJuggleLoop[weaponBeforeLastId].curveSpeedModifier = 1f;
        //}


    }

    private void StartJuggling()
    {
        isJuggling = true;
        UpgradeCombo.Instance.playerjuggle = weaponsCurrentlyInJuggleLoop;
        //StartCoroutine(nameof(ThrowUpAllWeaponsWithSameInterval), (timeInBetweenEachThrowAtTheStart) / (weaponsCurrentlyInJuggleLoop.Count - 1));

        ThrowUpAllWeapons();
    }


    private void ThrowUpAllWeapons()
    {
        for (int i = 0; i < weaponsCurrentlyInJuggleLoop.Count; i++)
        {
            weaponsCurrentlyInJuggleLoop[i].ThrowUpWeapon();
            weaponsCurrentlyInJuggleLoop[i].curveDeltaTime = (weaponsCurrentlyInJuggleLoop.Count - (i * 0.1f)) * 0.1f;
            //StartCoroutine(nameof(DistributeWeaponsInAir));
        }
    }


    IEnumerator DistributeWeaponsInAir()
    {
        while (CheckDistanceBetweenTwoWeapons(0, 1) < 0.5)
        {
            Debug.Log(CheckDistanceBetweenTwoWeapons(0, 1));

            weaponsCurrentlyInJuggleLoop[0].curveSpeedModifier += 0.001f;
            yield return null;
        }
        while (weaponsCurrentlyInJuggleLoop[0].curveSpeedModifier > 1f)
        {
            weaponsCurrentlyInJuggleLoop[0].curveSpeedModifier -= 0.001f;
            Debug.Log(CheckDistanceBetweenTwoWeapons(0, 1));
            yield return null;
        }

        Debug.Log("Now sepeerate");
    }

    private float CheckDistanceBetweenTwoWeapons(int firstWeaponListId, int secondWeaponListId)
    {
        float firstWeaponPos = weaponsCurrentlyInJuggleLoop[firstWeaponListId].curveDeltaTime;
        float secondWeaponPos = weaponsCurrentlyInJuggleLoop[secondWeaponListId].curveDeltaTime;

        float distance = Mathf.Abs(secondWeaponPos - firstWeaponPos);

        return distance;
    }


    private float CheckTimeBetweenTwoWeapons(int firstWeaponListId, int secondWeaponListId)
    {
        float firstWeaponTime = weaponsCurrentlyInJuggleLoop[firstWeaponListId].GetTimeUntilWeaponIsInHand();
        float secondWeaponTime = weaponsCurrentlyInJuggleLoop[secondWeaponListId].GetTimeUntilWeaponIsInHand();

        float timeBetweenWeapons = Mathf.Abs(secondWeaponTime - firstWeaponTime);

        return timeBetweenWeapons;
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

        //SpeedUpUpcomingWeapon();
        weaponInHand.ThrowUpWeapon();
        weaponInHand.weaponBase.UnEquipWeapon();
        weaponInHand = null;
        if (weaponQueueElementsScript == null) { return; }
        weaponQueueElementsScript.ArrowPositioning();
    }



    private WeaponJuggleMovement GetUpcomingWeapon()
    {
        WeaponJuggleMovement weaponThatIsFurthestInLoop = null;
        float furthestLoopTime = 0;

        for (int i = 0; i < weaponsCurrentlyInJuggleLoop.Count; i++)
        {
            if(weaponsCurrentlyInJuggleLoop[i].curveDeltaTime > furthestLoopTime)
            {
                if (!weaponsCurrentlyInJuggleLoop[i].weaponBase.weaponEquipped)
                {
                    furthestLoopTime = weaponsCurrentlyInJuggleLoop[i].curveDeltaTime;
                    weaponThatIsFurthestInLoop = weaponsCurrentlyInJuggleLoop[i];
                }
            }
        }

        return weaponThatIsFurthestInLoop;
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