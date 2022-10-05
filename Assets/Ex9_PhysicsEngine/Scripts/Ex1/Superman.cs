using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Superman : MonoBehaviour
{
    [SerializeField] private Transform superman, body;
    [SerializeField] private float movementSpeed;

    private Quaternion rotation;

    void Update()
    {
        SupermanMove();
    }

    private void SupermanMove()
    {
        rotation = transform.rotation;

        if (Input.GetKey(KeyCode.W))
        {
            superman.transform.Translate(0, 0, movementSpeed * Time.deltaTime);
            body.transform.localRotation = Quaternion.Euler(10, rotation.y, rotation.z);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            superman.transform.Translate(0, 0, -movementSpeed * Time.deltaTime);
            body.transform.localRotation = Quaternion.Euler(-10, rotation.y, rotation.z);
        }
        else
            body.transform.localRotation = Quaternion.Euler(0, rotation.y, rotation.z);

        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * Time.deltaTime * 600f, 0);
    }
}
