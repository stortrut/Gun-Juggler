using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunZone : MonoBehaviour
{
    public List<GameObject> objectsInField;
    private void OnTriggerEnter2D(Collider2D other)
    {
        objectsInField.Add(other.gameObject);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        objectsInField.Remove(other.gameObject);
    }
}
