using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] private Light2D left;
    [SerializeField] private Light2D right;
    [SerializeField] private Light2D middle;
    [SerializeField] private List<Light2D> lights;
    private Light2D lastLight;
    float startRadius;
    public float intensity;

    private void Start()
    {
        Debug.Log("CAn the lights flicker");
        Wait(); 
    }

    private void Wait()
    {
        lights.Add(left);
        lights.Add(right);
        lights.Add(middle);
        transform.DOMove(transform.position, 2f).SetLoops(-1).OnStepComplete(Flicker);
    }

    private void Flicker()
    {
        StartCoroutine(FlickerSame());
    }
    public IEnumerator FlickerSame()
    {
        Debug.Log("FLICKER");
        var light = lights[Random.Range(0, lights.Count)];
        startRadius = light.pointLightOuterRadius;
        light.pointLightOuterRadius = startRadius * intensity;
        yield return new WaitForSeconds(0.2f);
        light.pointLightOuterRadius = startRadius * intensity * intensity;
        yield return new WaitForSeconds(0.2f);
        light.pointLightOuterRadius = startRadius * intensity;
        yield return new WaitForSeconds(0.2f);
        light.pointLightOuterRadius = startRadius;

    }

}
