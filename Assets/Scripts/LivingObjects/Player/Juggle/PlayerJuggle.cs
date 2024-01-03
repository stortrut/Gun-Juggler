using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerJuggle : MonoBehaviour
{
    public static PlayerJuggle Instance;
    [SerializeField] private float timeInBetweenEachThrowAtTheStart;
    [SerializeField] private Animator bodyAnimator;
    [SerializeField] private GameObject popcornGun;

    [Header("Juggle Loop - Read Only")]
    [ReadOnly] public List<WeaponJuggleMovement> weaponsCurrentlyInJuggleLoop;
    //[ReadOnly] public List<WeaponJuggleMovement> testWeapons;
    [ReadOnly] public WeaponJuggleMovement weaponInHand;
    
    private WeaponQueueUI weaponQueueUI;
    [ReadOnly] public  bool isJuggling;

    [HideInInspector] public ArmAnimationHandler armAnimationHandler;
    [HideInInspector] public bool isAlive;

    [SerializeField] bool startJuggling = true;

    [SerializeField] bool pauseJuggling = false;

    [Header("The separate WeaponHolder - Drag in")]
    [SerializeField] Transform weaponHolderPoint;


    private bool spreadOutWeaponsInStart;
    [HideInInspector] public float timeUntilNextWeapon;
    [HideInInspector] public string nextWeapon;
    [HideInInspector] public float timeBetweenWeapons;




    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }


    private void Start()
    {
        isAlive = true;
        armAnimationHandler = GetComponentInChildren<ArmAnimationHandler>();

        weaponQueueUI = FindObjectOfType<WeaponQueueUI>();
        if (weaponQueueUI == null) { return; }
        weaponQueueUI.InstantiateTheWeapons();
    }


    private void Update()
    {
        if (Manager.Instance.player != null && startJuggling == true)
        {
            //if(weaponsCurrentlyInJuggleLoop.Count > 0)
                //StartJuggling();
        }

        if(weaponsCurrentlyInJuggleLoop.Count <= 0) { return; }











        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (isJuggling)
            {
                //Sound.instance.SoundRandomized(Sound.instance.throwUpWeapon, .6f);
                //ThrowUpWeaponInHand();
            }
            else
            {
                StartJuggling();
            }
        }
        if (!isJuggling) { return; }

        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            if (!pauseJuggling) { pauseJuggling = true; return; }
            if (pauseJuggling) { pauseJuggling = false; ThrowUpAllWeapons(); }
        }


        //Dynamic Weapon Speeds


        if (GetUpcomingWeapon() == null) { return; }
        nextWeapon = GetUpcomingWeapon().gameObject.name;
        timeUntilNextWeapon = GetUpcomingWeapon().GetTimeUntilWeaponIsInHand();

        if (weaponsCurrentlyInJuggleLoop.Count > 1)
            timeBetweenWeapons = CheckTimeBetweenTwoWeapons(0, 1);


        int lastWeaponId = weaponsCurrentlyInJuggleLoop.Count - 1;
        int weaponBeforeLastId = lastWeaponId - 1;
        float minDistanceBetweenWeapons = 1f;



        if (weaponsCurrentlyInJuggleLoop.Count <= 1) { return; }


        for (int i = 0; i < weaponsCurrentlyInJuggleLoop.Count; i++)
        {
            if(GetWeaponIdOfWeaponInfront(i) != -1)
            {
                if (!weaponsCurrentlyInJuggleLoop[i].weaponBase.weaponEquipped && !(weaponsCurrentlyInJuggleLoop[i] == weaponsCurrentlyInJuggleLoop[GetWeaponIdOfWeaponInfront(i)]))
                {
                    int idOfWeaponBeforeThisWeapon = GetWeaponIdOfWeaponInfront(i);

                    if (idOfWeaponBeforeThisWeapon != -1)
                    {

                        if (CheckTimeBetweenTwoWeapons(i, idOfWeaponBeforeThisWeapon) < 2)
                        {

                            if(weaponsCurrentlyInJuggleLoop[i].curveSpeedModifier > 0)
                                weaponsCurrentlyInJuggleLoop[i].curveSpeedModifier -= 1.2f * Time.deltaTime;
                            else
                            {
                                weaponsCurrentlyInJuggleLoop[i].curveSpeedModifier = 0.05f;
                            }
                        }
                        else
                        {
                            weaponsCurrentlyInJuggleLoop[i].curveSpeedModifier = 1f;
                        }
                    }
                    else
                    {
                        weaponsCurrentlyInJuggleLoop[i].curveSpeedModifier = 1f;
                    }
                }
            }
            else
            {
                weaponsCurrentlyInJuggleLoop[i].curveSpeedModifier = 1f;
            }
            //else if (weaponsCurrentlyInJuggleLoop[i] == GetUpcomingWeapon() && weaponInHand == null)
            //{
            //    //weaponsCurrentlyInJuggleLoop[i].curveSpeedModifier = 5f;
            //}
        }

    }







    private void StartJuggling()
    {
        startJuggling = false;
        isJuggling = true;

        if(UpgradeCombo.Instance != null)
            UpgradeCombo.Instance.playerjuggle = weaponsCurrentlyInJuggleLoop;

        ThrowUpAllWeapons();
    }
    private void ThrowUpAllWeapons()
    {
        isJuggling = true;
        weaponInHand = null;

        for (int i = 0; i < weaponsCurrentlyInJuggleLoop.Count; i++)
        {
            Sound.instance.SoundRandomized(Sound.instance.throwUpWeapon, .6f);
            weaponsCurrentlyInJuggleLoop[i].ThrowUpWeapon();
            weaponsCurrentlyInJuggleLoop[i].curveDeltaTime = (weaponsCurrentlyInJuggleLoop.Count - (i * 0.1f)) * 0.1f;
        }

        if(weaponsCurrentlyInJuggleLoop.Count > 1)
            StartCoroutine(nameof(DistributeWeaponsInAir));
    }


    public void CatchWeapon(WeaponJuggleMovement newWeapon)
    {
        newWeapon.beingThrown = false;
        newWeapon.weaponBase.EquipWeapon();

        weaponInHand = newWeapon;
        //originalParent = weaponInHand.transform.parent;
        //weaponInHand.gameObject.transform.SetParent(transform, false);
        //ANTI CROTCH PISTOL
        //weaponInHand.gameObject.transform.localPosition = new Vector3(1, 1, 0);

        //soundeffect:
        WeaponType weaponEnum = newWeapon.weaponBase.weaponType;
        int enumIndex = (int)weaponEnum;
        Sound.instance.SoundSet(Sound.instance.catchWeaponWeapontypeEnumOrder, enumIndex, 0.6f);
    }

    public void ThrowUpWeaponInHand()
    {
        if (weaponInHand == null) { return; }
        if (pauseJuggling) { return; }
        if (!isJuggling) { ThrowUpAllWeapons(); return; }

        weaponInHand.ThrowUpWeapon();
        //weaponInHand.gameObject.transform.SetParent(originalParent, false);
        weaponInHand = null;

        if (weaponQueueUI == null) { return; }
        weaponQueueUI.ArrowPositioning();
    }




    public void SpeedUpUpcomingWeapon()
    {
        int weaponPosition = weaponsCurrentlyInJuggleLoop.IndexOf(weaponInHand);

        if (weaponPosition == (weaponsCurrentlyInJuggleLoop.Count - 1))
        {
            weaponPosition = 0;
        }
        else {   weaponPosition++; }

        //weaponsCurrentlyInJuggleLoop[weaponPosition].curveSpeedModifier = 4f;
    }

    private WeaponJuggleMovement GetUpcomingWeapon()
    {
        WeaponJuggleMovement weaponThatIsFurthestInLoop = null;
        float furthestLoopTime = 0;

        if(weaponsCurrentlyInJuggleLoop.Count > 1)
        {
            for (int i = 0; i < weaponsCurrentlyInJuggleLoop.Count; i++)
            {
                if (weaponsCurrentlyInJuggleLoop[i].curveDeltaTime > furthestLoopTime)
                {
                    if (!weaponsCurrentlyInJuggleLoop[i].weaponBase.weaponEquipped)
                    {
                        furthestLoopTime = weaponsCurrentlyInJuggleLoop[i].curveDeltaTime;
                        weaponThatIsFurthestInLoop = weaponsCurrentlyInJuggleLoop[i];
                    }
                }
            }
        }
        else
        {
            weaponThatIsFurthestInLoop = weaponsCurrentlyInJuggleLoop[0];
        }

        return weaponThatIsFurthestInLoop;
    }


    private int GetWeaponIdOfWeaponInfront(int weaponId)
    {
        int weaponIdToReturn = -1;
        float leastFarLoopTime = weaponsCurrentlyInJuggleLoop[weaponId].endOfCurveXTimeValue - weaponsCurrentlyInJuggleLoop[weaponId].curveDeltaTime;

        if (weaponsCurrentlyInJuggleLoop.Count > 1)
        {
            for (int i = 0; i < weaponsCurrentlyInJuggleLoop.Count - 1; i++)
            {
                if (weaponsCurrentlyInJuggleLoop[i] != weaponsCurrentlyInJuggleLoop[weaponId])
                {
                    if (weaponsCurrentlyInJuggleLoop[i].curveDeltaTime <= leastFarLoopTime)
                    {
                        if (weaponsCurrentlyInJuggleLoop[i].curveDeltaTime >= weaponsCurrentlyInJuggleLoop[weaponId].curveDeltaTime)
                        {
                            if (!weaponsCurrentlyInJuggleLoop[i].weaponBase.weaponEquipped)
                            {

                                leastFarLoopTime = weaponsCurrentlyInJuggleLoop[i].curveDeltaTime;
                                weaponIdToReturn = i;
                            }
                        }
                    }
                }
            }
        }

        return weaponIdToReturn;
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



    IEnumerator DistributeWeaponsInAir()
    {
        while (CheckDistanceBetweenTwoWeapons(0, 1) < 0.5)
        {
            //Debug.Log(CheckDistanceBetweenTwoWeapons(0, 1));
                
            weaponsCurrentlyInJuggleLoop[0].curveSpeedModifier += 0.001f;
            yield return null;
        }
        while (weaponsCurrentlyInJuggleLoop[0].curveSpeedModifier > 1f)
        {
            weaponsCurrentlyInJuggleLoop[0].curveSpeedModifier -= 0.001f;
            //Debug.Log(CheckDistanceBetweenTwoWeapons(0, 1));
            yield return null;
        }
        //Debug.Log("Now sepeerate");
    }


    IEnumerator ThrowUpAllWeaponsWithSameInterval(float waitTimeBetweenEachThrow)
    {
        for (int i = 0; i < weaponsCurrentlyInJuggleLoop.Count - 1; i++)
        {
            weaponsCurrentlyInJuggleLoop[i].ThrowUpWeapon();
            yield return new WaitForSeconds(waitTimeBetweenEachThrow);
        }
    }





    public void RemoveWeaponFromLoop(WeaponJuggleMovement weaponToRemoved)
    {
        weaponsCurrentlyInJuggleLoop.Remove(weaponToRemoved);
        if (weaponsCurrentlyInJuggleLoop.Count < 2)
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
                weaponQueueUI.ReplaceQueueElements(i);
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
                weaponQueueUI.ReplaceQueueElements(i);
                return;
            }
        }
    }

    [System.Obsolete]
    public void ReplaceAllWeaponsWithAnotherWeapon(GameObject weaponPrefabToAdd)
    {

        List<WeaponJuggleMovement> oldWeapons = new();
        for (int i = 0; i < weaponsCurrentlyInJuggleLoop.Count; i++)
        {
            WeaponJuggleMovement oldWeaponData = weaponsCurrentlyInJuggleLoop[i];
            oldWeapons.Add(oldWeaponData);

            GameObject newGun = Instantiate(weaponPrefabToAdd, weaponHolderPoint.position, Quaternion.identity, weaponHolderPoint);

            newGun.GetComponentInChildren<WeaponJuggleMovement>().curveDeltaTime = oldWeaponData.curveDeltaTime;
            newGun.transform.localRotation = Quaternion.EulerRotation(0, 0, 0);
            newGun.GetComponentInChildren<WeaponJuggleMovement>().beingThrown = true;

            newGun.GetComponentInChildren<WeaponJuggleMovement>().weaponBase.UnEquipWeapon();
        }

        for (int i = 0; i < oldWeapons.Count; i++)
        {
            oldWeapons[i].DropWeapon();
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

    public void AddExistingWeaponToLoop(GameObject weaponToAdd)
    {
        weaponsCurrentlyInJuggleLoop.Add(weaponToAdd.GetComponentInChildren<WeaponJuggleMovement>());

        if (isJuggling)
        {
            ThrowUpWeaponInHand();
        }
        weaponInHand = weaponToAdd.GetComponentInChildren<WeaponJuggleMovement>();
        weaponInHand.GetComponentInChildren<WeaponJuggleMovement>().weaponBase.EquipWeapon();       //lägg till i currently in loop listan också? så queuen funkar
    }

    [System.Obsolete]
    public void CreateAndAddWeaponToLoop(GameObject weaponPrefabToAdd)
    {
        if (!isJuggling)
        {
            isJuggling = true;
            ThrowUpWeaponInHand();
        }

        GameObject newGun = Instantiate(weaponPrefabToAdd, weaponHolderPoint.position, Quaternion.identity, weaponHolderPoint);
        //weaponsCurrentlyInJuggleLoop.Add(newGun.GetComponentInChildren<WeaponJuggleMovement>());
        newGun.transform.localPosition = new Vector3(0, 0, 0);
        newGun.transform.localRotation = Quaternion.EulerRotation(0, 0, 0);

        //weaponsCurrentlyInJuggleLoop.Add(newGun.GetComponentInChildren<WeaponJuggleMovement>());

        if (UpgradeCombo.Instance != null)
            UpgradeCombo.Instance.playerjuggle = weaponsCurrentlyInJuggleLoop;
        else { Debug.Log("ERROR: Did not find the UpgradeCombo Instance"); }

      
        //weaponInHand = newGun.GetComponentInChildren<WeaponJuggleMovement>();
        //weaponInHand.GetComponentInChildren<WeaponJuggleMovement>().weaponBase.EquipWeapon();       //lägg till i currently in loop listan också? så queuen funkar
        //weaponQueueUI.InstantiateTheWeapons();
    }

    public void Ultimate()
    {
        //throw up all weapons in new curve
        weaponsCurrentlyInJuggleLoop.Clear();
        CreateAndAddWeaponToLoop(popcornGun);
        isJuggling = false;
    }
}