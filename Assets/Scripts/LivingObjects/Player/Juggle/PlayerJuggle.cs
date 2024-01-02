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
    private Transform originalParent;
    private bool isJuggling;

    [HideInInspector] public ArmAnimationHandler armAnimationHandler;
    [HideInInspector] public bool isAlive;

    [SerializeField] bool startJuggling = true;
   
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }

    public void SpeedUpUpcomingWeapon()
    {

        //weaponsCurrentlyInJuggleLoop = testWeapons;
        int weaponPosition = weaponsCurrentlyInJuggleLoop.IndexOf(weaponInHand);

        if (weaponPosition == (weaponsCurrentlyInJuggleLoop.Count - 1))
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
        for (int i = 0; i < weaponsCurrentlyInJuggleLoop.Count; i++)
            Debug.Log(weaponsCurrentlyInJuggleLoop[i]); 

        isAlive = true;
        armAnimationHandler = GetComponentInChildren<ArmAnimationHandler>();
        //WeaponJuggleMovement[] weaponsOnPlayer = testWeapons.ToArray();
        //var i = 0;
        //foreach (WeaponJuggleMovement weapon in weaponsOnPlayer)
        //{
        //    i++;
        //    weaponsCurrentlyInJuggleLoop.Add(weapon);
        //}
        int lastWeaponID = weaponsCurrentlyInJuggleLoop.Count - 1;
        //weaponInHand = weaponsCurrentlyInJuggleLoop[lastWeaponID];
       
        //weaponsCurrentlyInJuggleLoop[lastWeaponID].weaponBase.EquipWeapon();
        weaponQueueUI = FindObjectOfType<WeaponQueueUI>();
        if (weaponQueueUI == null) { return; }
        weaponQueueUI.InstantiateTheWeapons();
    }

    private bool spreadOutWeaponsInStart;
    [HideInInspector] public float timeUntilNextWeapon;
    [HideInInspector] public string nextWeapon;
    [HideInInspector] public float timeBetweenWeapons;

    private void Update()

    {
        if (Manager.Instance.player != null && startJuggling == true)
        {
            StartJuggling();
        }

        if (!isJuggling) { return; }
        //nextWeapon = GetUpcomingWeapon().gameObject.name;
        //timeUntilNextWeapon = GetUpcomingWeapon().GetTimeUntilWeaponIsInHand();
        timeBetweenWeapons = CheckTimeBetweenTwoWeapons(0, 1,"first");

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

                if (CheckTimeBetweenTwoWeapons(i, idOfWeaponBeforeThisWeapon,"second") < 2)
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
            Sound.instance.SoundRandomized(Sound.instance.throwUpWeapon, .6f);
            ThrowUpWeaponInHand();
        }
    }
    private void StartJuggling()
    {
        startJuggling = false;
        isJuggling = true;
        UpgradeCombo.Instance.playerjuggle = weaponsCurrentlyInJuggleLoop;
        //StartCoroutine(nameof(ThrowUpAllWeaponsWithSameInterval), (timeInBetweenEachThrowAtTheStart) / (weaponsCurrentlyInJuggleLoop.Count - 1));

        ThrowUpAllWeapons();
    }
    private void ThrowUpAllWeapons()
    {
        for (int i = 0; i < weaponsCurrentlyInJuggleLoop.Count; i++)
        {
            Sound.instance.SoundRandomized(Sound.instance.throwUpWeapon, .6f);
            weaponsCurrentlyInJuggleLoop[i].ThrowUpWeapon();
            weaponsCurrentlyInJuggleLoop[i].curveDeltaTime = (weaponsCurrentlyInJuggleLoop.Count - (i * 0.1f)) * 0.1f;
            //StartCoroutine(nameof(DistributeWeaponsInAir));
        }
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

    private float CheckDistanceBetweenTwoWeapons(int firstWeaponListId, int secondWeaponListId)
    {
      
        float firstWeaponPos = weaponsCurrentlyInJuggleLoop[firstWeaponListId].curveDeltaTime;
        float secondWeaponPos = weaponsCurrentlyInJuggleLoop[secondWeaponListId].curveDeltaTime;

        float distance = Mathf.Abs(secondWeaponPos - firstWeaponPos);

        return distance;
    }
    private float CheckTimeBetweenTwoWeapons(int firstWeaponListId, int secondWeaponListId,string whichOne)

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
        originalParent = weaponInHand.transform.parent;
        weaponInHand.gameObject.transform.SetParent(transform, false);
        //ANTI CROTCH PISTOL
        weaponInHand.gameObject.transform.localPosition = new Vector3(1, 1, 0);

        //soundeffect:
        WeaponType weaponEnum = newWeapon.weaponBase.weaponType;
        int enumIndex = (int)weaponEnum;
        Sound.instance.SoundSet(Sound.instance.catchWeaponWeapontypeEnumOrder, enumIndex, 0.6f);
    }

    public void ThrowUpWeaponInHand()
    {
        if (weaponInHand == null) { return; }

        //SpeedUpUpcomingWeapon();
        weaponInHand.ThrowUpWeapon();
        weaponInHand.gameObject.transform.SetParent(originalParent, false);
        weaponInHand = null;

        if (weaponQueueUI == null) { return; }
        weaponQueueUI.ArrowPositioning();
    }
    private WeaponJuggleMovement GetUpcomingWeapon()
    {
        WeaponJuggleMovement weaponThatIsFurthestInLoop = null;
        float furthestLoopTime = 0;

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

        return weaponThatIsFurthestInLoop;
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

    public void DropAllWeaponsOnGround()
    {
        for (int i = 0; i < weaponsCurrentlyInJuggleLoop.Count; i++)
        {
            weaponsCurrentlyInJuggleLoop[i].DropWeapon();
            isAlive = false;
        }
    }


    [Header("The separate WeaponHolder - Drag in")]
    [SerializeField] Transform weaponHolderPoint;

    public void AddWeaponToLoop(GameObject weaponPrefabToAdd)
    {
        UpgradeCombo.Instance.playerjuggle = weaponsCurrentlyInJuggleLoop;
        GameObject newGun = Instantiate(weaponPrefabToAdd, weaponHolderPoint.position, Quaternion.identity, weaponHolderPoint);
        weaponsCurrentlyInJuggleLoop.Add(newGun.GetComponentInChildren<WeaponJuggleMovement>());

        if (isJuggling)
        {
            ThrowUpWeaponInHand();
        }
        
        weaponInHand = newGun.GetComponentInChildren<WeaponJuggleMovement>();
        weaponInHand.GetComponentInChildren<WeaponJuggleMovement>().weaponBase.EquipWeapon();       //lägg till i currently in loop listan också? så queuen funkar
        //weaponQueueUI.InstantiateTheWeapons();
    }

    public void Ultimate()
    {
        //throw up all weapons in new curve
        weaponsCurrentlyInJuggleLoop.Clear();
        AddWeaponToLoop(popcornGun);
        isJuggling = false;
    }
}