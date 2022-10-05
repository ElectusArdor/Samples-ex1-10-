using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotSleep : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.WakeUp();
    }
}
