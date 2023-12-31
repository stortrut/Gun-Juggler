using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProtection : Health
{
    [SerializeField] private GameObject Protection;
    public int numberOfProtection = 1;
    public int numberOfProtection2 = 1;
    public HealthUI healthImage;
    public List<GameObject> protectingItems;
    private GameObject currentProtection;
    [ReadOnly] public int totalProtection;
    [SerializeField] private bool hamster;
    [SerializeField] private GameObject hamter;
    
    public void Start()
    {
        totalProtection = numberOfProtection + numberOfProtection2;
           
            
        for (int i = 100; i < 100 + numberOfProtection; i++) 
        { 
          currentProtection = Instantiate(Protection,transform.position+new Vector3(0,0,i),Quaternion.identity,gameObject.transform); 
          protectingItems.Add(currentProtection);           
        }
        for (int i = 0; i < numberOfProtection2; i++)
        {
            currentProtection = Instantiate(Protection, transform.position + new Vector3(0, 0, i), Quaternion.identity, gameObject.transform);
            protectingItems.Add(currentProtection);
        }
        if (numberOfProtection > 0) 
        { 
            // health = GetComponent<Health>();
             //healthImage.ColorChange(Color.blue);    
             base.hasProtection = true;

        }
        else
        {
            if(health != null)
            { 
                
            }
        }
    }
    public void RemoveProtection(int amount)
    {
        totalProtection -= amount;
        //int randomIndex = Random.Range(0, protectingItems.Count);
        //protectingItems.RemoveAt(randomIndex);
        if (totalProtection == 0)//protectingItems.Count==0)
        {
            base.Death();
            EffectAnimations.Instance.EnemyPoof(transform.position);
            if (hamster == true)
            {
                Instantiate(hamter, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
            //health.oneShot = true;
            //health.hasProtection = false;
            //healthImage.ColorChange(Color.yellow);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("HoolaHoop"))
        {
            AudienceSatisfaction.Instance.AudienceHappiness(-health * 2);
            base.Death();
            EffectAnimations.Instance.EnemyPoof(transform.position);
            if (hamster == true)
            {
                Instantiate(hamter, transform.position, Quaternion.identity);
            }

            Sound.Instance.SoundRandomized(Sound.Instance.dissapointment, .6f, .1f);
            Destroy(gameObject);
            // CUSTOM FAIL DEATH;
        }
    }
}
