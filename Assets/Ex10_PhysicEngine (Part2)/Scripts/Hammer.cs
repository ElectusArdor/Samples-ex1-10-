using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    [SerializeField] private float launchTime, radius;
    [SerializeField] private int totalBalls;
    [SerializeField] private LayerMask layer;

    private Vector3 startPosition, endPosition;
    private float tmr;
    private int ballsCount;
    private bool launch, readyToTighten;

    private void Start()
    {
        startPosition = transform.localPosition;
        endPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 7.67f);
    }

    private void BallsCounter()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layer);

        foreach (Collider collider in colliders)
        {
            if (collider.name.Contains("Ball"))
                ballsCount++;
        }
    }

    private void Launch()
    {
        if (ballsCount >= totalBalls)
            launch = true;

        ballsCount = 0;
    }

    private void Stretching()
    {
        if (ballsCount >= totalBalls & readyToTighten)
        {
            transform.localPosition = startPosition;
            tmr = 0;
        }

        ballsCount = 0;
    }

    void FixedUpdate()
    {
        BallsCounter();

        if (!launch)
            Launch();
        else
            Stretching();
    }

    void Update()
    {
        if (launch)
        {
            tmr += Time.deltaTime;

            if (tmr >= launchTime)
                transform.localPosition = endPosition;

            if (tmr >= launchTime + 3f)
                readyToTighten = true;
        }

        if (readyToTighten & transform.localPosition == startPosition)
        {
            readyToTighten = false;
            launch = false;
        }
    }
}
