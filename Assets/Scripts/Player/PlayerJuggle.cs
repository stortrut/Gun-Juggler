using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJuggle : MonoBehaviour
{
    [SerializeField] private float timeInBetweenEachThrowAtTheStart;


    List<WeaponJuggleMovement> weaponsCurrentlyInJuggleLoop = new();

    private bool isJuggling;
    private WeaponJuggleMovement weaponInHand;
    [HideInInspector] public ArmAnimationHandler armAnimationHandler;

    private void Start()
    {
        armAnimationHandler = GetComponentInChildren<ArmAnimationHandler>();

        WeaponJuggleMovement[] weaponsOnPlayer = GetComponentsInChildren<WeaponJuggleMovement>();
        foreach (WeaponJuggleMovement weapon in weaponsOnPlayer)
        {
            weaponsCurrentlyInJuggleLoop.Add(weapon);
        }

        int lastWeaponID = weaponsCurrentlyInJuggleLoop.Count - 1;

        weaponInHand = weaponsCurrentlyInJuggleLoop[lastWeaponID];

        weaponsCurrentlyInJuggleLoop[lastWeaponID].weaponBase.EquipWeapon();

        isJuggling = true;
        StartCoroutine(nameof(ThrowUpAllWeaponsWithSameInterval), (timeInBetweenEachThrowAtTheStart) / (weaponsCurrentlyInJuggleLoop.Count - 1));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isJuggling)
        {
            isJuggling = true;
            StartCoroutine(nameof(ThrowUpAllWeaponsWithSameInterval), (timeInBetweenEachThrowAtTheStart) / (weaponsCurrentlyInJuggleLoop.Count - 1));
        }
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
        weaponInHand.ThrowUpWeapon();
        weaponInHand.weaponBase.UnEquipWeapon();

        newWeapon.beingThrown = false;
        newWeapon.weaponBase.EquipWeapon();
        weaponInHand = newWeapon;
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
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

}