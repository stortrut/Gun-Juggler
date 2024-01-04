using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    public static Lights Instance { get; private set; }

    [SerializeField] GameObject[] battleLights;
    [SerializeField] GameObject[] battleLightSerie;
    [SerializeField] GameObject[] normalLights;

    [SerializeField] float delayBetweenLights;

    private void Awake()
    {
        Instance = this;
    }
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
        if (on)         //fightlights on
        {
            for (int i = 0; i < normalLights.Length; i++)       //stäng av ljus
            {
                normalLights[i].SetActive(false);
            }

            for (int i = 0; i < battleLightSerie.Length; i++)
            {
                
                battleLightSerie[i].SetActive(true);
                Sound.Instance.SoundSet(Sound.Instance.spotLightOn, 0, 1, .4f);
                yield return new WaitForSeconds(delayBetweenLights);
            }

            Sound.Instance.SoundSet(Sound.Instance.spotLightOn, 0, 1.3f, .2f);

            for (int i = 0; i < battleLights.Length; i++)
            {
                battleLights[i].SetActive(true);
            }
        }

        else
        {
            for (int i = 0; i < battleLights.Length; i++)
            {
                battleLights[i].SetActive(false);
            }

            for (int i = 0; i < battleLightSerie.Length; i++)
            {
                battleLightSerie[i].SetActive(false);
            }

            for (int i = 0; i < normalLights.Length; i++)
            {
                //Sound.Instance.SoundSet(Sound.Instance.spotLightOn,0,1,.4f);
                normalLights[i].SetActive(true);
            }
            Sound.Instance.SoundSet(Sound.Instance.spotLightOn, 0, 1, .4f);
        }
    }
}
