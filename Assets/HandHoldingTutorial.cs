using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandHoldingTutorial : MonoBehaviour
{
    public GameObject firstGun;

    public GameObject aimBox;
    public GameObject shootBox;
    public GameObject explainThrowUp;
    public GameObject explainThrowUpArrow;

    public GameObject criticalCatchStartCheck;
    public GameObject learnToCriticalCatchBoc;


    private void Start()
    {
        FindObjectOfType<PlayerJuggle>().canNotUseWeapons = true;
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
        FindObjectOfType<PlayerMovement>().turnOffMovement = true;

        yield return new WaitForSeconds(4f);

        aimBox.SetActive(false);
        shootBox.SetActive(true);

        FindObjectOfType<PlayerJuggle>().canNotUseWeapons = false;

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        yield return new WaitForSeconds(0.65f);
        shootBox.SetActive(false);
        explainThrowUp.SetActive(true);

        FindObjectOfType<PlayerJuggle>().canNotUseWeapons = true;
        yield return new WaitForSeconds(3f);
        FindObjectOfType<PlayerJuggle>().canNotUseWeapons = false;


        explainThrowUp.GetComponentInChildren<TextMeshPro>().text = "Try using it a few more times!";

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));


        explainThrowUp.GetComponentInChildren<TextMeshPro>().text = "Let's try it against some enemies!";
        explainThrowUpArrow.SetActive(true);

        //explainThrowUp.SetActive(false);
        FindObjectOfType<PlayerMovement>().turnOffMovement = false;


        while (firstGun != null)
        {
            yield return null;
        }

        FindObjectOfType<PlayerMovement>().turnOffMovement = true;
        learnToCriticalCatchBoc.SetActive(true);


        Debug.Log("END");
    }





}
