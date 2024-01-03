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
        audienceSatisfaction.fillAmount += happiness/100;
        if(happiness < 0 && timer > oldTime + 2)
        {
            Sound.instance.SoundRandomized(Sound.instance.audienceBoo);
            oldTime = timer;
        }
        if(audienceSatisfaction.fillAmount == 1)
        {
           // Sound.instance.SoundRandomized(Sound.instance.)
        }
    }
 
}
