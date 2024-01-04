using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandHoldingTutorial : MonoBehaviour
{
    public GameObject firstGun;

    public GameObject aimBox;
    public GameObject shootBox;


    private void Start()
    {
        StartCoroutine(nameof(Tutorial));
    }


    IEnumerator Tutorial()
    {
        if(firstGun == null) { Debug.Log("NDAWONDAOWDNAOWPDIHAWDAWD"); }

        while (firstGun != null)
        {
            yield return null;
        }

        aimBox.SetActive(true);

        yield return new WaitForSeconds(4f);

        shootBox.SetActive(false);




        Debug.Log("END");
    }





}
