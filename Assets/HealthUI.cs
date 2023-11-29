using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
   public Image healthImage;
    void Start()
    {
        healthImage.fillAmount = 1;
    }

    // Update is called once per frame
    public void UpdateHealth(float health, float maxHealth)
    {
        
        healthImage.fillAmount = health / maxHealth;
    }
    public void ColorChange(Color color) 
    {
        healthImage.color = color;
    }

}
