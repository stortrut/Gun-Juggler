using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunZone : MonoBehaviour
{
    [HideInInspector]public List<GameObject> objectsInField;
    public SpriteRenderer soundWave;
    private void OnTriggerEnter2D(Collider2D other)
    {
        objectsInField.Add(other.gameObject);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        objectsInField.Remove(other.gameObject);
    }
    public void SoundWave()
    {       
        soundWave.enabled=true;
        StartCoroutine(nameof(Wait));
        
    }
    private  IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.2f);
        soundWave.enabled = false;

    }
}
