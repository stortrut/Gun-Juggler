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
    public GameObject catchCircle;

    public GameObject entertainmentRatingCheck;
    public GameObject entertainmentRatingBox;
    public GameObject entertainmentRatingArrow;


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

        yield return new WaitForSeconds(5f);

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


        explainThrowUp.GetComponentInChildren<TextMeshPro>().text = "Let's try it against an enemy!";
        explainThrowUpArrow.SetActive(true);

        //explainThrowUp.SetActive(false);
        FindObjectOfType<PlayerMovement>().turnOffMovement = false;






        //              
        //              CRITICAL CATCH
        //
        while (criticalCatchStartCheck != null)
        {
            yield return null;
        }

        FindObjectOfType<PlayerMovement>().turnOffMovement = true;
        learnToCriticalCatchBoc.SetActive(true);
        catchCircle.GetComponent<SpriteRenderer>().enabled = true;

        FindObjectOfType<JuggleCatchCircle>().caughtGun = false;
        while (FindObjectOfType<JuggleCatchCircle>().caughtGun == false)
        {
            yield return null;
        }
        learnToCriticalCatchBoc.GetComponentInChildren<TextMeshPro>().text = "Good job! Make another Critical Catch!";
        
        FindObjectOfType<JuggleCatchCircle>().caughtGun = false;
        while (FindObjectOfType<JuggleCatchCircle>().caughtGun == false)
        {
            yield return null;
        }

        learnToCriticalCatchBoc.GetComponentInChildren<TextMeshPro>().text = "Weapons upgrade each time you make a Critical Catch!";
        FindObjectOfType<PlayerMovement>().turnOffMovement = false;



        //              
        //              ENTERTAINMENT RATING
        //
        while (entertainmentRatingCheck != null)
        {
            yield return null;
        }
        entertainmentRatingBox.SetActive(true);
        entertainmentRatingArrow.SetActive(true);

        FindObjectOfType<PlayerMovement>().turnOffMovement = true;
        yield return new WaitForSeconds(4f);

        entertainmentRatingBox.GetComponentInChildren<TextMeshPro>().text = "Defeat enemies before they leave the stage to keep your Performance Rating up!";
        FindObjectOfType<PlayerMovement>().turnOffMovement = false;
        entertainmentRatingArrow.SetActive(false);




        Debug.Log("END");
    }





}
