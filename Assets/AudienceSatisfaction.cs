using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudienceSatisfaction : MonoBehaviour
{
    public static AudienceSatisfaction Instance;
    public Image audienceSatisfaction;
    
    
    // Start is called before the first frame update
    void Start()
    {
        audienceSatisfaction.fillAmount = 0.3f; 
        Instance = this;
       // audienceSlider.value = 20;  
    }
    public void AudienceHappiness(float happiness)
    {
        audienceSatisfaction.fillAmount += happiness/100;
    }
 
}
