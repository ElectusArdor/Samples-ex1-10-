using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    [SerializeField] private float boomTmr, boomStrength;
    private float tmr;
    private bool boom,boomFinished;

    private void Start()
    {
        tmr = boomTmr;
    }

    private void OnTriggerStay(Collider other)
    {
        if (boom)
        {
            if (other.GetComponent<Rigidbody>() != null)
            {
                var direction = (other.transform.position - transform.position).normalized * boomStrength;
                var shockWaveCoef = 35f - (other.transform.position - transform.position).magnitude;

                if (shockWaveCoef < 0)
                    shockWaveCoef = 0;

                other.GetComponent<Rigidbody>().AddForce(direction * shockWaveCoef, ForceMode.Impulse);
            }
        }
    }

    private void FixedUpdate()
    {
        if (boomFinished)
        {
            boom = false;
            boomFinished = false;
        }

        if (boom)
            boomFinished = true;
    }

    void Update()
    {
        tmr -= Time.deltaTime;

        if (tmr < 0)
        {
            tmr = boomTmr;
            boom = true;
        }
    }
}
