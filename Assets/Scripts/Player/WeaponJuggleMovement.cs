using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponJuggleMovement : MonoBehaviour
{
    [SerializeField] AnimationCurve gunThrowAnimationCurveY;
    [SerializeField] AnimationCurve gunThrowAnimationCurveX;

    [SerializeField] public WeaponBase weaponBase;

    //[SerializeField] private float speed = 100.0f;

    [HideInInspector] public float curveDeltaTime = 0.0f;
    public bool beingThrown;

    private float endOfCurveYTimeValue;
    private float endOfCurveXTimeValue;

    private WeaponJuggleMovement thisWeaponJuggleMovement;
    private PlayerJuggle playerJuggle;

    private void Start()
    {
        thisWeaponJuggleMovement = GetComponent<WeaponJuggleMovement>();
        playerJuggle = GetComponentInParent<PlayerJuggle>();

        Keyframe[] allCurveYKeys = gunThrowAnimationCurveY.keys;
        Keyframe[] allCurveXKeys = gunThrowAnimationCurveX.keys;

        endOfCurveYTimeValue = allCurveYKeys[allCurveYKeys.Length - 1].time;
        endOfCurveXTimeValue = allCurveXKeys[allCurveXKeys.Length - 1].time;


        //curveDeltaTime += Random.Range(0.1f, 1f);

        Keyframe[] keys1 = gunThrowAnimationCurveY.keys;
        Keyframe keyFrame = keys1[1];
        keyFrame.value = Random.Range(4f, 5f);
        keys1[1] = keyFrame;
        gunThrowAnimationCurveY.keys = keys1; // This is copying the keys back into the AnimationCurve's array.
    }

    void Update()
    {
        if (beingThrown)
        {
            // Get the current position of the sphere
            Vector3 currentPosition = transform.localPosition;
            // Call evaluate on that time   
            curveDeltaTime += Time.deltaTime;

            currentPosition.y = gunThrowAnimationCurveY.Evaluate(curveDeltaTime);
            currentPosition.x = gunThrowAnimationCurveX.Evaluate(curveDeltaTime);


            // Update the current position of the sphere
            transform.localPosition = currentPosition;
        }


        if (curveDeltaTime >= endOfCurveYTimeValue && curveDeltaTime >= endOfCurveXTimeValue)
        {
            weaponBase.EquipWeapon();
            playerJuggle.CatchWeapon(thisWeaponJuggleMovement);
        }

        //Throw up
        //if (Input.GetKeyDown(KeyCode.Mouse1) && weaponBase.weaponEquipped)
        //{
        //    ThrowUpWeapon();
        //}
    }


    public void ThrowUpWeapon()
    {
        curveDeltaTime = 0;
        beingThrown = true;
    }


}
