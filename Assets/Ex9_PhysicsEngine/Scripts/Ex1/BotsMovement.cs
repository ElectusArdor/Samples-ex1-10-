using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotsMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        AddForce();
    }

    private void OnTriggerEnter(Collider other)
    {
        transform.LookAt(new Vector3(Random.Range(-20f, 20f), transform.position.y, Random.Range(-20f, 20f)));
    }

    private void AddForce()
    {
        if (rb.velocity.magnitude < speed)
            rb.AddForce(transform.forward * speed, ForceMode.Acceleration);
    }
}
