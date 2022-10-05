using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SportsmenMove : MonoBehaviour
{
    public Transform fist;
    public bool run;
    public Vector3 targetPoint;

    [SerializeField] private Transform ribCage, pelvis, sportsman;
    [SerializeField] private Transform leftLeg, leftShin, leftFoot, rightLeg, rightShin, rightFoot;
    [SerializeField] private Transform leftArm, leftForearm, rightArm, rightForearm;
    [SerializeField] private float speed;
    [SerializeField] private float legMoveCoef, armAmplitudeCoef, ribCageAmplitudeCoef, pelvisIncline;
    [SerializeField] private bool rightForward;

    private void Run()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPoint, Time.deltaTime * speed);
    }

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

        rightShin.localRotation = Quaternion.Euler(0, 0, Mathf.Abs(leftLeg.localRotation.z * Mathf.Rad2Deg) * 1.5f - 90f);
        leftShin.localRotation = Quaternion.Euler(0, 0, Mathf.Abs(leftLeg.localRotation.z * Mathf.Rad2Deg) * 1.5f - 90f);

        rightFoot.localRotation = Quaternion.Euler(0, 0, rightLeg.localRotation.z * Mathf.Rad2Deg / 1.5f);
        leftFoot.localRotation = Quaternion.Euler(0, 0, leftLeg.localRotation.z * Mathf.Rad2Deg / 1.5f);

        if (leftLeg.localRotation.z > 0.64f)
            rightForward = true;
        else if (rightLeg.localRotation.z > 0.64f)
            rightForward = false;
    }

    private void BodyMove()
    {
        ribCage.localRotation = Quaternion.Euler(0, rightLeg.localRotation.z * ribCageAmplitudeCoef, 0);
        pelvis.localRotation = Quaternion.Euler(0, 0, pelvisIncline);
        sportsman.localPosition = new Vector3(sportsman.localPosition.x, 0, sportsman.localPosition.z);
    }

    private void ArmsMove()
    {
        rightArm.localRotation = Quaternion.Euler(8f, 0, leftLeg.localRotation.z * Mathf.Rad2Deg * armAmplitudeCoef);
        leftArm.localRotation = Quaternion.Euler(-8f, 0, rightLeg.localRotation.z * Mathf.Rad2Deg * armAmplitudeCoef);

        rightForearm.localRotation = Quaternion.Euler(0, 0, 70f + rightArm.localRotation.z * Mathf.Rad2Deg);
        leftForearm.localRotation = Quaternion.Euler(0, 0, 70f + leftArm.localRotation.z * Mathf.Rad2Deg);
    }

    private void StayPose()
    {
        rightLeg.localRotation = Quaternion.Euler(0, 0, 0);
        leftLeg.localRotation = Quaternion.Euler(0, 0, 0);
        rightShin.localRotation = Quaternion.Euler(0, 0, 0);
        leftShin.localRotation = Quaternion.Euler(0, 0, 0);
        rightFoot.localRotation = Quaternion.Euler(0, 0, 0);
        leftFoot.localRotation = Quaternion.Euler(0, 0, 0);

        ribCage.localRotation = Quaternion.Euler(0, 0, 0);
        pelvis.localRotation = Quaternion.Euler(0, 0, 0);

        rightArm.localRotation = Quaternion.Euler(8f, 0, 0);
        leftArm.localRotation = Quaternion.Euler(-8f, 0, 0);

        rightForearm.localRotation = Quaternion.Euler(0, 0, 0);
        leftForearm.localRotation = Quaternion.Euler(0, 0, 0);

        sportsman.localPosition = new Vector3(sportsman.localPosition.x, 0.2f, sportsman.localPosition.z);
    }

    void Update()
    {
        if (run)
        {
            Run();
            LegsMove();
            BodyMove();
            ArmsMove();
        }
        else
        {
            StayPose();
        }
    }
}
