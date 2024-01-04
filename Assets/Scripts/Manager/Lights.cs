using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    public static Lights Instance { get; private set; }

    [SerializeField] GameObject[] battleLights;
    [SerializeField] GameObject[] battleLightSerie;
    [SerializeField] GameObject[] normalLights;

    [SerializeField] float delayBetweenLights = 0.5f;


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.H))
        {
            FightLightOn(true);
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            FightLightOn(true);
        }
    }
    public void FightLightOn(bool on)
    {
        StartCoroutine(FightLightsSerie(on));
    }

    public void TurnOffBattleLights()
    {
        for (int i = 0; i < battleLightSerie.Length; i++)
        {
            battleLightSerie[i].SetActive(false);
            battleLights[i].SetActive(false);
            //yield return new WaitForSeconds(delayBetweenLights);
        }

    }

    private IEnumerator FightLightsSerie(bool on)
    {
        if (on)
        {
            for (int i = 0; i < battleLightSerie.Length; i++)
            {
                Sound.Instance.SoundSet(Sound.Instance.spotLightOn,0,1,.4f);
                battleLightSerie[i].SetActive(true);
                yield return new WaitForSeconds(delayBetweenLights);
            }
        }

        else
        {
            for (int i = 0; i < battleLightSerie.Length; i++)
            {
                battleLightSerie[i].SetActive(false);
                //yield return new WaitForSeconds(delayBetweenLights);
            }
        }

        for (int i = 0; i < normalLights.Length; i++)
        {
            normalLights[i].SetActive(on);
            if (!on)
            {
                Sound.Instance.SoundSet(Sound.Instance.spotLightOn, 0, 1, .4f);
            }
            
            //yield return new WaitForSeconds(delayBetweenLights);
        }

        for (int i = 0; i < normalLights.Length; i++)
        {
            battleLights[i].SetActive(on);
            if (on)
            {
                Sound.Instance.SoundSet(Sound.Instance.spotLightOn, 0, 1, .4f);
            }
            //yield return new WaitForSeconds(delayBetweenLights);
        }
    }


}
