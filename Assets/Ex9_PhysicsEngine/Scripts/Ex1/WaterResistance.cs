using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterResistance : MonoBehaviour
{
    private Rigidbody rb;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
        {
            rb = other.GetComponent<Rigidbody>();
            rb.velocity *= 0.2f;
        }
    }
}
