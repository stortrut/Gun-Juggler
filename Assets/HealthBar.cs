using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class HealthBar : MonoBehaviour
{
    public static HealthBar Instance { get; private set; }
    [SerializeField] private Image[] hearts;
    private int health = 3;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        Instance = this;
    }
    public void RemoveHeart(int amount)
    {
        health -= amount;
        hearts.ElementAt(health).gameObject.SetActive(false);

    }
    public void AddHeart(int amount)
    {
        health += amount;
        hearts.ElementAt(health).gameObject.SetActive(true);

    }


}
