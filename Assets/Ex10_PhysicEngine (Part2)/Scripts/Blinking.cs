using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinking : MonoBehaviour
{
    private Light lt;
    private float targetIntensity;

    void Start()
    {
        lt = GetComponent<Light>();
        targetIntensity = Random.Range(0.1f, 5f);
    }

    void Update()
    {
        lt.intensity += Time.deltaTime * (targetIntensity - lt.intensity) * 10f;

        if (Mathf.Abs(targetIntensity - lt.intensity) <= Time.deltaTime * 10f)
            targetIntensity = Random.Range(0.1f, 5f);
    }
}
