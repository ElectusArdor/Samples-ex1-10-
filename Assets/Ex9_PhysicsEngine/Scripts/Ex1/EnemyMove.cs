using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private bool forward, up;

    private float currentRotationY, currentRotationZ;
    private int sign;

    private void Start()
    {
        sign = Mathf.RoundToInt(transform.localScale.x);

        currentRotationY = transform.localEulerAngles.y;
        currentRotationZ = transform.localEulerAngles.z;
    }

    void Update()
    {
        LegMove();
    }

    private void LegMove()
    {
        var deltaRotation = Time.deltaTime * 200f;

        if (forward)
        {
            currentRotationY += deltaRotation * sign;
            if (Mathf.Abs(currentRotationY) >= 20)
            {
                currentRotationY = 20 * sign;
                currentRotationZ = 0;
                forward = false;
            }

            if (up)
            {
                currentRotationZ -= deltaRotation * 1.5f * sign;
                if (Mathf.Abs(currentRotationZ) >= 30)
                {
                    currentRotationZ = -30 * sign;
                    up = false;
                }
            }
            else
                currentRotationZ += deltaRotation * 1.5f * sign;
        }
        else
        {
            currentRotationY -= deltaRotation * sign;
            if (Mathf.Abs(currentRotationY) >= 20)
            {
                currentRotationY = -20 * sign;
                forward = true;
                up = true;
            }
        }

        transform.localRotation = Quaternion.Euler(0, currentRotationY, currentRotationZ);
    }
}
