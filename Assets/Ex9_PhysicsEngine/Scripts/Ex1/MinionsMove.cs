using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionsMove : MonoBehaviour
{
    [SerializeField] private Transform rightLeg, leftLeg, rightArm, leftArm;
    [SerializeField] private float legMoveCoef, armAmplitudeCoef, speed;


    private bool rightForward;

    private void LegsMove()
    {
        float deltaRotation;
        sbyte deltaSign;

        deltaRotation = Time.deltaTime * legMoveCoef * speed;

        if (rightForward)
            deltaSign = 1;
        else
            deltaSign = -1;

        rightLeg.localRotation *= Quaternion.Euler(0, 0, deltaRotation * deltaSign);
        leftLeg.localRotation *= Quaternion.Euler(0, 0, -deltaRotation * deltaSign);

        if (leftLeg.localRotation.z > 0.35f)
            rightForward = true;
        else if (rightLeg.localRotation.z > 0.35f)
            rightForward = false;
    }

    private void ArmsMove()
    {
        rightArm.localRotation = Quaternion.Euler(25, 0, leftLeg.localRotation.z * Mathf.Rad2Deg * armAmplitudeCoef);
        leftArm.localRotation = Quaternion.Euler(-25, 0, rightLeg.localRotation.z * Mathf.Rad2Deg * armAmplitudeCoef);
    }

    void Update()
    {
        LegsMove();
        ArmsMove();
    }
}
