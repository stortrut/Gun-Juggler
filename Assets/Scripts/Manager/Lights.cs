using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    public static Lights Instance { get; private set; }

    [SerializeField] GameObject bigFightLight;
    [SerializeField] GameObject[] fightLightSerie;
    [SerializeField] GameObject[] normalLights;

    [SerializeField] float delayBetweenLights = 0.5f;

    public void FightLightOn(bool on)
    {
        StartCoroutine(FightLightsSerie(on));
    }

    private IEnumerator FightLightsSerie(bool on)
    {
        if (on)
        {
            for (int i = 0; i < fightLightSerie.Length; i++)
            {
                Sound.Instance.SoundSet(Sound.Instance.spotLightOn,0,1,.4f);
                fightLightSerie[i].SetActive(true);
                yield return new WaitForSeconds(delayBetweenLights);
            }
        }

        else
        {
            for (int i = 0; i < fightLightSerie.Length; i++)
            {
                fightLightSerie[i].SetActive(false);
                //yield return new WaitForSeconds(delayBetweenLights);
            }
        }

        for (int i = 0; i < normalLights.Length; i++)
        {
            normalLights[i].SetActive(true);
            //yield return new WaitForSeconds(delayBetweenLights);
        }

        bigFightLight.SetActive(on);
    }


}
