using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathCard : MonoBehaviour
{
    public static DeathCard instance;
    [SerializeField] private Image image;
    [SerializeField] private List<Sprite> deathCards;


    private void Awake()
    {
        instance = this;
    }
    public void ActivateDeath()
    {
        image.sprite = deathCards[Random.Range(0,deathCards.Count)];
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        for (float i = 0; i < 200; i++)
        {
            yield return new WaitForSeconds(200 / 20000);
            image.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, (i / 200)));
            
        }
        yield return new WaitForSeconds(2);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


    }

}
