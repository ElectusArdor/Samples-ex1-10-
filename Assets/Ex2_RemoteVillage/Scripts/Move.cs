using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private float speed;
    private Vector3 targetPos;
    private Quaternion targetRot;
    
    void Start()
    {
        speed = 4f;
        targetPos = transform.localPosition;
        targetRot = Quaternion.Euler(0, 0, 0);
        transform.localRotation = targetRot;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            speed = 16f;
        else
            speed = 4f;

        if (Input.GetKey(KeyCode.W))
            targetPos += transform.forward * Time.deltaTime * speed;
        if (Input.GetKey(KeyCode.S))
            targetPos -= transform.forward * Time.deltaTime * speed;
        if (Input.GetKey(KeyCode.D))
            targetPos += transform.right * Time.deltaTime * speed;
        if (Input.GetKey(KeyCode.A))
            targetPos -= transform.right * Time.deltaTime * speed;

        targetRot *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * Time.deltaTime * 300f, 0);
    }

    private void LateUpdate()
    {
        transform.localPosition = targetPos;
        transform.localRotation = targetRot;
        if (transform.localPosition.y != 0.8f)
            transform.localPosition = new Vector3(transform.localPosition.x, 0.8f, transform.localPosition.z);
    }
}
