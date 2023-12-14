using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunZone : MonoBehaviour
{
    [SerializeField] Animator soundWaveAnimator;

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
        soundWave.enabled = true;
        soundWaveAnimator.SetBool("Wave", true);
        StartCoroutine(nameof(Wait));
    }
    private  IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        soundWaveAnimator.SetBool("Wave", false);

        yield return new WaitForSeconds(0.7f);
        soundWave.enabled = false;
    }
}
