using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    [SerializeField] private float hitStrength;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != gameObject.tag)
        {
            Vector3 direction = (collision.transform.position - transform.position).normalized;

            if (collision.gameObject.GetComponent<Rigidbody>() != null)
                collision.gameObject.GetComponent<Rigidbody>().AddForce(direction * hitStrength, ForceMode.VelocityChange);
        }
    }
}
