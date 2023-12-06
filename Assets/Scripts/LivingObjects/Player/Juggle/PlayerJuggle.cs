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
    public WeaponJuggleMovement weaponInHand;
    [HideInInspector] public ArmAnimationHandler armAnimationHandler;
    WeaponQueueElementsVariant weaponQueueElementsScript;

    private void Start()
    {
        armAnimationHandler = GetComponentInChildren<ArmAnimationHandler>();

        WeaponQueueElementsVariant weaponQueueElementsScript = FindObjectOfType<WeaponQueueElementsVariant>();

        WeaponJuggleMovement[] weaponsOnPlayer = GetComponentsInChildren<WeaponJuggleMovement>();
        foreach (WeaponJuggleMovement weapon in weaponsOnPlayer)
        {
            weaponsCurrentlyInJuggleLoop.Add(weapon);
        }

        int lastWeaponID = weaponsCurrentlyInJuggleLoop.Count - 1;

        weaponInHand = weaponsCurrentlyInJuggleLoop[lastWeaponID];

        //weaponsCurrentlyInJuggleLoop[lastWeaponID].weaponBase.EquipWeapon();

        //StartJuggling();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isJuggling)
        {
            Debug.Log("Start Juggling");
            StartJuggling();
        }
    }

    private void StartJuggling()
    {
        isJuggling = true;
        StartCoroutine(nameof(ThrowUpAllWeaponsWithSameInterval), (timeInBetweenEachThrowAtTheStart) / (weaponsCurrentlyInJuggleLoop.Count - 1));
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
        weaponInHand.ThrowUpWeapon();
        weaponInHand.weaponBase.UnEquipWeapon();
        weaponInHand = null;
        weaponQueueElementsScript.ShowNextWeaponInQueueMoving();
    }



    public void RemoveWeaponFromLoop(WeaponJuggleMovement weaponToRemoved)
    {
        weaponsCurrentlyInJuggleLoop.Remove(weaponToRemoved);
        if(weaponsCurrentlyInJuggleLoop.Count < 2)
        {
            StartCoroutine(nameof(PlayerDied));
        }
    }
    IEnumerator PlayerDied()
    {
        Debug.Log("Player Died");
        bodyAnimator.speed = 1;
        bodyAnimator.SetBool("Dead", true);
        yield return new WaitForSeconds(1.65f);
        SceneManager.LoadScene(0);
    }
}