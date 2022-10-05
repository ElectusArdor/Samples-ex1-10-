using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    [SerializeField] private float pushStrength;

    private Rigidbody rb;

    private void OnCollisionEnter(Collision coll)
    {
        rb = coll.gameObject.GetComponent<Rigidbody>();

        if (rb != null)
            rb.AddForce((coll.gameObject.transform.localPosition - transform.localPosition).normalized * pushStrength, ForceMode.Impulse);
    }
}
