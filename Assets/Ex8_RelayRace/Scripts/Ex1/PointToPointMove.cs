using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToPointMove : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private float speed;
    private int currentPointNumber, totalPoints;
    private bool forward;

    void Start()
    {
        forward = true;

        totalPoints = points.Length - 1;
        currentPointNumber = 0;
    }

    private void Movement()
    {
        transform.Rotate(0, Time.deltaTime * 45f, 0);

        transform.position = Vector3.MoveTowards(transform.position, points[currentPointNumber].position, Time.deltaTime * speed);

        if (transform.position == points[currentPointNumber].position)
        {
            if (forward)
            {
                currentPointNumber++;
                if (currentPointNumber == totalPoints)
                    forward = false;
            }
            else
            {
                currentPointNumber--;
                if (currentPointNumber == 0)
                    forward = true;
            }
        }
    }

    void Update()
    {
        Movement();
    }
}
