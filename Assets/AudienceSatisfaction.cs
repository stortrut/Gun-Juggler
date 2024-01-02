using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudienceSatisfaction : MonoBehaviour
{
    public static AudienceSatisfaction Instance;
    public Slider audienceSlider;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
       // audienceSlider.value = 20;  
    }
    public void AudienceHappiness(float happiness)
    {
        //audienceSlider.value += happiness;
    }
 
}
