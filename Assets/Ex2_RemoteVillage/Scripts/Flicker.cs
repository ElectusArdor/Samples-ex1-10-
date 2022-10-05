using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    private Light flameLight;
    private float targetIntensity;

    void Start()
    {
        flameLight = GetComponent<Light>();
        targetIntensity = Random.Range(0.5f, 1.5f);
    }


    void Update()
    {
        flameLight.intensity = Mathf.Lerp(flameLight.intensity, targetIntensity, Time.deltaTime * 15f);
        if (Mathf.Abs(flameLight.intensity - targetIntensity) <= 0.02f)
            targetIntensity = Random.Range(0.5f, 1.25f);
    }
}
