using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    public static Lights Instance { get; private set; }

    [SerializeField] GameObject[] battleLights;
    [SerializeField] GameObject[] battleLightSerie;
    [SerializeField] GameObject[] normalLights;
    [SerializeField] GameObject[] menuLights;

    [SerializeField] float delayBetweenLights;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

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
    public void MenuLightsOn()
    {
        StartCoroutine(MenuLightSerie());
    }

    public void FightLightOn(bool on)
    {
        StartCoroutine(FightLightsSerie(on));
    }

    public void NormalLightsOn()
    {
        StartCoroutine(NormalLightSerie());
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

    private IEnumerator MenuLightSerie()
    {
        for (int i = 0; i < menuLights.Length; i++)
        {
            menuLights[i].SetActive(false);
        }

        yield return new WaitForSeconds(.2f);

        Sound.Instance.SoundSet(Sound.Instance.spotLightOn, 0, 1, .25f);
        yield return new WaitForSeconds(.05f);
        menuLights[0].SetActive(true);

        yield return new WaitForSeconds(.1f);

        Sound.Instance.SoundSet(Sound.Instance.spotLightOn, 0, 1, .25f);
        yield return new WaitForSeconds(.05f);
        menuLights[1].SetActive(true);

        yield return new WaitForSeconds(.13f);

        Sound.Instance.SoundSet(Sound.Instance.spotLightOn, 0, 1, .25f);
        yield return new WaitForSeconds(.05f);
        menuLights[2].SetActive(true);
    }

    private IEnumerator NormalLightSerie()
    {
        for (int i = 0; i < normalLights.Length; i++)       
        {
            normalLights[i].SetActive(false);
        }
        yield return new WaitForSeconds(.5f);

        for (int i = 0; i < normalLights.Length; i++)       //stäng av ljus
        {
            Sound.Instance.SoundSet(Sound.Instance.spotLightOn, 0, 1, .4f);
            yield return new WaitForSeconds(.2f);
            normalLights[i].SetActive(true);
            yield return new WaitForSeconds(.5f);
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
            yield return new WaitForSeconds(0.2f);

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
