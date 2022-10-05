using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelayRaceController : MonoBehaviour
{
    [SerializeField] private GameObject[] sportsmans;
    [SerializeField] private Transform[] points;

    [SerializeField] private GameObject baton, cameraContainer, victoryCanvas;
    [SerializeField] private AudioSource startHorn;
    [SerializeField] private float passDistance, passSpeed;

    private SportsmenMove runingSportsmenMove;
    private float startTmr;
    private int sportsmanNum, totalPoints;
    private bool started, finished, handover;

    void Start()
    {
        totalPoints = points.Length;
        sportsmanNum = -1;
    }

    private void StartHorn()
    {
        startTmr += Time.deltaTime;
        if (startTmr > 1)
        {
            startHorn.Play();
            PassTheBaton();
            started = true;
        }
    }

    private void PassTheBaton()
    {
        sportsmanNum++;

        if (runingSportsmenMove != null)
            runingSportsmenMove.run = false;

        sportsmans[sportsmanNum].transform.LookAt(points[sportsmanNum]);

        runingSportsmenMove = sportsmans[sportsmanNum].GetComponent<SportsmenMove>();

        runingSportsmenMove.targetPoint = points[sportsmanNum].localPosition;
        runingSportsmenMove.run = true;
        baton.transform.SetParent(null);
        handover = true;

        cameraContainer.transform.SetParent(sportsmans[sportsmanNum].transform);
    }

    private void MoveBaton()
    {
        baton.transform.position = Vector3.MoveTowards(baton.transform.position, runingSportsmenMove.fist.position, Time.deltaTime * passSpeed);
        if (baton.transform.position == runingSportsmenMove.fist.position)
        {
            baton.transform.SetParent(runingSportsmenMove.fist);
            baton.transform.localRotation = Quaternion.Euler(0, 0, -80);
            handover = false;
        }
    }

    private void Victory()
    {
        victoryCanvas.SetActive(true);
        finished = true;
        runingSportsmenMove.run = false;
    }

    void Update()
    {
        if (!started)
            StartHorn();

        if (!finished)
        {
            if (handover)
                MoveBaton();

            if (started)
            {
                if (Vector3.Distance(sportsmans[sportsmanNum].transform.localPosition, points[sportsmanNum].localPosition) <= passDistance)
                {
                    if (sportsmanNum < totalPoints - 1)
                        PassTheBaton();
                    else
                        Victory();
                }
            }
        }

        if (cameraContainer.transform.parent != null)
            cameraContainer.transform.localPosition = Vector3.Lerp(cameraContainer.transform.localPosition, Vector3.zero, Time.deltaTime * 2);
    }
}
