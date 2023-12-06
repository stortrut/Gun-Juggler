using UnityEngine;

public class WeaponQueueElements : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject smallGun;
    [SerializeField] GameObject shotGun;
    [SerializeField] GameObject stunGun;
    [SerializeField] GameObject heart;

    PlayerJuggle playerJuggleScript;
    private float posGapBetweenQueueObjects = 4;

    void Start()
    {
        playerJuggleScript = FindObjectOfType<PlayerJuggle>();
        InstantiateAppropriateQueueElements();
    }

    public void InstantiateAppropriateQueueElements()
    {
        int loopLength = playerJuggleScript.weaponsCurrentlyInJuggleLoop.Count;
        //int posOffsetMultiplier = 0;

        for (int i = 0; i < loopLength; i++)
        {
            WeaponJuggleMovement weaponJuggleMovement = playerJuggleScript.weaponsCurrentlyInJuggleLoop[i];
            WeaponBase.WeaponType weaponEnum = weaponJuggleMovement.weaponBase.weaponType;      

            // Get the RectTransform component for positioning UI elements
            RectTransform rectTransform = GetComponent<RectTransform>();
            int enumIndex = (int)weaponEnum;

            if (weaponEnum == WeaponBase.WeaponType.SmallGun)
            {
                Vector3 indexObjectInQueuePos = new Vector3(posGapBetweenQueueObjects * i, 0);
                GameObject newSmallGun = Instantiate(smallGun, rectTransform);
                RectTransform smallGunTransform = newSmallGun.GetComponent<RectTransform>();
                smallGunTransform.anchoredPosition = indexObjectInQueuePos;
                //posOffsetMultiplier++;
            }
            else if (weaponEnum == WeaponBase.WeaponType.ShotGun)
            {
                Vector3 firstObjectInQueuePos = new Vector3(posGapBetweenQueueObjects * i, 0);
                GameObject newShotGun = Instantiate(shotGun, rectTransform);
                RectTransform shotGunTransform = newShotGun.GetComponent<RectTransform>();
                shotGunTransform.anchoredPosition = firstObjectInQueuePos;
                //posOffsetMultiplier++;
            }
            else if (weaponEnum == WeaponBase.WeaponType.StunGun)
            {
                Vector3 firstObjectInQueuePos = new Vector3(posGapBetweenQueueObjects * i, 0);
                GameObject newStunGun = Instantiate(stunGun, rectTransform);
                RectTransform stunGunTransform = newStunGun.GetComponent<RectTransform>();
                stunGunTransform.anchoredPosition = firstObjectInQueuePos;
                //posOffsetMultiplier++;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            InstantiateAppropriateQueueElements();
        }
    }
}
