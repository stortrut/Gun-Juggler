using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponQueueElementsVariant : MonoBehaviour
{
    [SerializeField] GameObject smallGun;
    [SerializeField] GameObject shotGun;
    [SerializeField] GameObject stunGun;
    [SerializeField] GameObject heart;
    //SmallGun,
    //    ShotGun,
    //    StunGun

    PlayerJuggle playerJuggleScript;
    private float firstQueueObjectPos;
    private float posGapBetweenQueueObjects = 4;
    public Vector3 firstObjectInQueuePos;


    void Start()
    {
        playerJuggleScript = FindObjectOfType<PlayerJuggle>();
        InstantiateAppropriateQueueElements();
    }

    public void InstantiateAppropriateQueueElements()
    {
        //playerJuggleScript.weaponsCurrentlyInJuggleLoop.weaponBase.WeaponType
        int loopLength = playerJuggleScript.weaponsCurrentlyInJuggleLoop.Count;

        int posOffsetMultiplier = 0;

        for (int i = 0; i < loopLength; i++)
        {
            //playerJuggleScript.weaponsCurrentlyInJuggleLoop[i].weaponType;
            WeaponJuggleMovement weaponJuggleMovement = playerJuggleScript.weaponsCurrentlyInJuggleLoop[i];
            WeaponBase.WeaponType weaponEnum = weaponJuggleMovement.weaponBase.weaponType;
            if (weaponEnum == WeaponBase.WeaponType.SmallGun)
            {
                firstObjectInQueuePos = new Vector2(posGapBetweenQueueObjects * posOffsetMultiplier, 0);
                Instantiate(smallGun, firstObjectInQueuePos, Quaternion.identity, transform);
                posOffsetMultiplier++;
            }
            else if (weaponEnum == WeaponBase.WeaponType.ShotGun)
            {
                firstObjectInQueuePos = new Vector2(posGapBetweenQueueObjects * posOffsetMultiplier, 0);
                Instantiate(shotGun, firstObjectInQueuePos, Quaternion.identity, transform);
                posOffsetMultiplier++;
            }
            else if (weaponEnum == WeaponBase.WeaponType.StunGun)
            {
                firstObjectInQueuePos = new Vector2(posGapBetweenQueueObjects * posOffsetMultiplier, 0);
                Instantiate(stunGun, firstObjectInQueuePos, Quaternion.identity, transform);
                posOffsetMultiplier++;
            }
        }
    }

    void MoveQueueObjects()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            InstantiateAppropriateQueueElements();
        }
    }
}
//    int enumIndex = (int)weaponEnum;
