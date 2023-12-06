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

    [Header("Spin while thrown curve")]
    [SerializeField] AnimationCurve rotationAnimationCurve;

    [HideInInspector] public bool gotCaught;

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

        //Keyframe[] keys1 = gunThrowAnimationCurveY.keys;
        //Keyframe keyFrame = keys1[1];
        //keyFrame.value = Random.Range(4f, 5f);
        //keys1[1] = keyFrame;
        //gunThrowAnimationCurveY.keys = keys1; // This is copying the keys back into the AnimationCurve's array.
    }

    void Update()
    {
        if (beingThrown)
        {
            // Get the current position of the weapon
            Vector3 currentPosition = transform.localPosition;
            // Call evaluate on that time   
            curveDeltaTime += Time.deltaTime;

            currentPosition.y = gunThrowAnimationCurveY.Evaluate(curveDeltaTime);
            currentPosition.x = gunThrowAnimationCurveX.Evaluate(curveDeltaTime);

            // Update the current position of the weapon
            transform.localPosition = currentPosition;

            float rotationValue = rotationAnimationCurve.Evaluate(curveDeltaTime);
            transform.localRotation = Quaternion.Euler(0, 0, rotationValue);
        }

        if (curveDeltaTime >= endOfCurveYTimeValue && curveDeltaTime >= endOfCurveXTimeValue)
        {
            if (beingThrown)
            {
                weaponBase.EquipWeapon();
                playerJuggle.CatchWeapon(thisWeaponJuggleMovement);
            }
        }
        if (curveDeltaTime >= endOfCurveYTimeValue && curveDeltaTime >= endOfCurveXTimeValue / 2)
        {
            if(playerJuggle.weaponInHand != null && beingThrown)
            {
                playerJuggle.ThrowUpWeaponInHand();
            }
        }

        //if (beingDropped)
        //{
        //    Vector3 currentPosition = transform.localPosition;
        //    Vector3 offSet = transform.localPosition;

        //    // Call evaluate on that time   
        //    curveDeltaTime += Time.deltaTime;

        //    currentPosition.y = gunDropAnimationCurveY.Evaluate(curveDeltaTime) + offSet.y;
        //    currentPosition.x = gunDropAnimationCurveX.Evaluate(curveDeltaTime) + offSet.x;

        //    // Update the current position of the sphere
        //    transform.localPosition = currentPosition;
        //}
    }


    public void ThrowUpWeapon()
    {
        if(playerJuggle == null) { Debug.Log("ERROR"); }
        if (playerJuggle.armAnimationHandler == null) { Debug.Log("ERROR"); }

        playerJuggle.armAnimationHandler.StartCoroutine(nameof(playerJuggle.armAnimationHandler.PlayThrowUpWeaponAnimation));

        curveDeltaTime = 0;
        beingThrown = true;
    }

    public void DropWeapon()
    {
        //playerJuggle.RemoveWeaponFromLoop()

        beingDropped = true;
        beingThrown = false;
        curveDeltaTime = 0;

        weaponBase.rb2D.bodyType = RigidbodyType2D.Dynamic;
        weaponBase.rb2D.AddForce(new Vector2(-200, 400));
        weaponBase.weaponCollider.isTrigger = false;
    }
}
