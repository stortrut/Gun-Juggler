using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudienceSatisfaction : MonoBehaviour
{
    public static AudienceSatisfaction Instance;
    public Image audienceSatisfaction;
    float timer = 4;
    float oldTime = 0;
    float oldTime2 = 0;
    public bool fightActive;


    // Start is called before the first frame update
    void Start()
    {
        audienceSatisfaction.fillAmount = 0.3f;
        Instance = this;
        // audienceSlider.value = 20;  
    }
    public void AudienceHappiness(float happiness)
    {
        timer = Time.time;
        audienceSatisfaction.fillAmount += happiness / 100;
        audienceSatisfaction.fillAmount = Mathf.Clamp(audienceSatisfaction.fillAmount, 0, 1);
        if (happiness < 0 && timer > oldTime + 2)
        {
            //Sound.Instance.SoundSet(Sound.Instance.audienceBoo,0,.2f);
            //Sound.Instance.SoundSet(Sound.Instance.audienceBoo, 1,.2f);
            oldTime = timer;
        }
        if (happiness > 0 && timer > oldTime2 + 2)
        {
            // Sound.instance.SoundRandomized(Sound.instance.)
        }

    }
    public void ActStarted()
    {
       fightActive = true;
    }
    public void ActDone()
    {
        fightActive = false;
        StartCoroutine(DecreaseSlowly());
    }
    public IEnumerator DecreaseSlowly()
    {
        for (float i = 0; i < 400; i++)
        {
            if (fightActive == true)
                {
                    break;
                }
               
                yield return new WaitForSeconds(0.3f);
                audienceSatisfaction.fillAmount -= 0.002f;
                if (audienceSatisfaction.fillAmount < 0.15f)
                {
                    PlayerHealth.Instance.KillPlayer();
                    PlayerHealth.Instance.isDead = true;
                    break;
                }
            
        }
    }
}
