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
    //private Transform originalParent;
    private bool isJuggling;

    [HideInInspector] public ArmAnimationHandler armAnimationHandler;
    [HideInInspector] public bool isAlive;

    [SerializeField] bool startJuggling = true;

    [SerializeField] bool pauseJuggling = false;

    [Header("The separate WeaponHolder - Drag in")]
    [SerializeField] Transform weaponHolderPoint;

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
                Sound.instance.SoundRandomized(Sound.instance.throwUpWeapon, .6f);
                ThrowUpWeaponInHand();
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

        GameObject newGun = Instantiate(weaponPrefabToAdd, weaponHolderPoint.position, Quaternion.identity, weaponHolderPoint);
        //weaponsCurrentlyInJuggleLoop.Add(newGun.GetComponentInChildren<WeaponJuggleMovement>());
        newGun.transform.localPosition = new Vector3(0, 0, 0);
        newGun.transform.localRotation = Quaternion.EulerRotation(0, 0, 0);

        if (UpgradeCombo.Instance != null)
            UpgradeCombo.Instance.playerjuggle = weaponsCurrentlyInJuggleLoop;
        else { Debug.Log("ERROR: Did not find the UpgradeCombo Instance"); }

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
        CreateAndAddWeaponToLoop(popcornGun);
        isJuggling = false;
    }
}