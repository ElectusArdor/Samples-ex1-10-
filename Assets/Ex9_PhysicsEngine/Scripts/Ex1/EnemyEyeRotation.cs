using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEyeRotation : MonoBehaviour
{
    private Quaternion targetRotation;
    private float freezTmr;

    private void Start()
    {
        RandomRotation();
    }

    private void Rotation()
    {
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, Time.deltaTime * 50f);

        if (transform.localRotation == targetRotation)
        {
            freezTmr -= Time.deltaTime;
            if (freezTmr < 0)
            {
                RandomRotation();
                freezTmr = Random.Range(0.3f, 3f);
            }
        }
    }

    void Update()
    {
        Rotation();
    }

    private void RandomRotation()
    {
        targetRotation = new Quaternion(Random.Range(-0.45f, 0.45f), Random.Range(-0.45f, 0.45f), 0, 1);
    }
}
