using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    private void Start()
    {
        Transform transform = GetComponent<Transform>();
    }
    public void KnockBackMyself(Vector3 referencePosition)  //referencePosition is not the thing that gets knocked back
    {
        Vector3 distance = new Vector3(referencePosition.x - transform.position.x, 0);
        distance.Normalize();
        float knockbackDirection = distance.x;
        Debug.Log(knockbackDirection);

        transform.position += new Vector3(-knockbackDirection * 1, 0, 0);
    }
}
