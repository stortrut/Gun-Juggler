using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiBullet : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private List<Sprite> confetti;
    void Awake()
    {
       spriteRenderer = GetComponent<SpriteRenderer>();
       spriteRenderer.sprite = confetti[Random.Range(0,confetti.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
