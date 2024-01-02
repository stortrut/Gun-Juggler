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
    [SerializeField] float startRotationZ;
    [HideInInspector] public bool gotCaught;

    /*[HideInInspector] */public float curveDeltaTime = 0.0f;
    public float curveSpeedModifier = 1f;

    private float endOfCurveYTimeValue;
    [HideInInspector] public float endOfCurveXTimeValue;

    private PlayerJuggle playerJuggle;

    private void Start()
    {
        playerJuggle = FindObjectOfType<PlayerJuggle>();
        playerJuggle.AddExistingWeaponToLoop(this.transform.parent.gameObject);

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

    public void SetCurveSpeedModifier(float newCurveSpeedModifier)
    {
       // curveSpeedModifier = newCurveSpeedModifier;
    }

    private void ResetCurveSpeedModifier()
    {
       // curveSpeedModifier = 1f;
    }

    void Update()
    {
        if (beingThrown)
        {
            // Get the current position of the weapon
            Vector3 currentPosition = transform.localPosition;
            
            curveDeltaTime += Time.deltaTime * curveSpeedModifier * 2;
                

            currentPosition.y = gunThrowAnimationCurveY.Evaluate(curveDeltaTime);
            currentPosition.x = gunThrowAnimationCurveX.Evaluate(curveDeltaTime);

            // Update the current position of the weapon
            transform.localPosition = currentPosition;


            // Air Rotation
            float rotationValue = rotationAnimationCurve.Evaluate(curveDeltaTime);

            transform.localRotation = Quaternion.Euler(0, 0, rotationValue + startRotationZ);
        }
        else
        {
            startRotationZ = transform.eulerAngles.z;
        }


        //Equip 
        if (curveDeltaTime >= endOfCurveYTimeValue && curveDeltaTime >= endOfCurveXTimeValue)
        {
            if (beingThrown)
            {
                ResetCurveSpeedModifier();
                weaponBase.EquipWeapon();
                playerJuggle.CatchWeapon(this);
            }
        }
        //Throw up old weapon
        if (curveDeltaTime >= endOfCurveYTimeValue && curveDeltaTime >= endOfCurveXTimeValue / 2)
        {
            if(playerJuggle.weaponInHand != null && beingThrown)
            {
                playerJuggle.ThrowUpWeaponInHand();
            }
        }

        //if (curveDeltaTime >= endOfCurveYTimeValue - 0.1)
        //{
        //    if (beingThrown)
        //    {
        //        ResetCurveSpeedModifier();
        //    }
        //}


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

    public float GetTimeUntilWeaponIsInHand()
    {
        return (endOfCurveXTimeValue - curveDeltaTime * curveSpeedModifier) % endOfCurveXTimeValue;
    }

    public void ThrowUpWeapon()
    {
        if (beingDropped) { return; }
        if (playerJuggle == null) { /*Debug.Log("ERROR..");*/ playerJuggle = FindObjectOfType<PlayerJuggle>(); }
        if (playerJuggle == null) { Debug.Log("BIG ERROR!");}

        if (playerJuggle.armAnimationHandler == null) { Debug.Log("ARM ANIMATION ERROR"); }
        playerJuggle.armAnimationHandler.StartCoroutine(nameof(playerJuggle.armAnimationHandler.PlayThrowUpWeaponAnimation));

        curveDeltaTime = 0;
        beingThrown = true;
        weaponBase.UnEquipWeapon();
        Sound.instance.SoundRandomized(Sound.instance.throwUpWeapon,.1f);
    }

    public void DropWeapon()
    {
        playerJuggle.RemoveWeaponFromLoop(gameObject.GetComponent<WeaponJuggleMovement>());

        beingDropped = true;
        beingThrown = false;
        curveDeltaTime = 0;

        transform.parent.SetParent(playerJuggle.transform.parent.parent);
        weaponBase.rb2D.bodyType = RigidbodyType2D.Dynamic;
        weaponBase.rb2D.AddForce(new Vector2(-200, 200));
        weaponBase.weaponCollider.isTrigger = false;

        weaponBase.UnEquipWeapon();

        Destroy(transform.parent.gameObject, 5f);
    }
}
