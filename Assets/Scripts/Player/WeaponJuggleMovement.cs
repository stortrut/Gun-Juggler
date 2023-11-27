using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponJuggleMovement : MonoBehaviour
{
    [SerializeField] public WeaponBase weaponBase;

    [Header("Juggle Loop Curves")]
    [SerializeField] AnimationCurve gunThrowAnimationCurveY;
    [SerializeField] AnimationCurve gunThrowAnimationCurveX;
    public bool beingThrown;

    [Header("Drop Weapon Curves")]
    [SerializeField] AnimationCurve gunDropAnimationCurveY;
    [SerializeField] AnimationCurve gunDropAnimationCurveX;
    public bool beingDropped;


    //[SerializeField] private float speed = 100.0f;

    [HideInInspector] public float curveDeltaTime = 0.0f;

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
            if (beingThrown)
            {
                weaponBase.EquipWeapon();
                playerJuggle.CatchWeapon(thisWeaponJuggleMovement);
            }
        }


        if (beingDropped)
        {
            Vector3 currentPosition = transform.localPosition;
            Vector3 offSet = transform.localPosition;

            // Call evaluate on that time   
            curveDeltaTime += Time.deltaTime;

            currentPosition.y = gunDropAnimationCurveY.Evaluate(curveDeltaTime) + offSet.y;
            currentPosition.x = gunDropAnimationCurveX.Evaluate(curveDeltaTime) + offSet.x;

            // Update the current position of the sphere
            transform.localPosition = currentPosition;
        }
    }


    public void ThrowUpWeapon()
    {
        curveDeltaTime = 0;
        beingThrown = true;
    }

    public void DropWeapon()
    {
        beingDropped = true;
        beingThrown = false;
        curveDeltaTime = 0;
    }
}
